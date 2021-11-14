using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffAppTestTask.DB;
using StuffAppTestTask.Models;

namespace StuffAppTestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var genders = await _context.Genders.ToArrayAsync();
            var departments = await _context.Departments.ToArrayAsync();
            var employees = await _context.Employees
                .Where(e => !e.Deleted)
                .OrderBy(e => e.Id)
                .ToArrayAsync();
            var employeeModels = employees.Select(e => new EmployeeModel
            {
                Id = e.Id,
                Age = e.Age,
                Name = e.Name,
                Surname = e.Surname,
                GenderTitle = genders.Single(g => g.Id == e.Gender).Title,
                DepartmentTitle = departments.Single(d => d.Id == e.DepartmentId).Title
            });
            var model = new EmployeeListModel
            {
                EmployeeList = employeeModels
            };
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _context.Departments.ToArrayAsync();
            var genders = await _context.Genders.ToArrayAsync();
            var programLanguages = await _context.ProgramLanguages.ToArrayAsync();
            
            var result = from empl in _context.Employees 
                where empl.Id == id
                join exp in _context.Experiences on empl.Id equals exp.EmployeeId
                select new
                {
                    Id = empl.Id,
                    Name = empl.Name,
                    Surname = empl.Surname,
                    Age = empl.Age,
                    GenderId = empl.Gender,
                    DepartmentId = empl.DepartmentId,
                    ProgramLanguageId = exp.ProgramLanguageId
                };

            var rows = await result.ToArrayAsync();

            var model = new EmployeeEditModel
            {
                Id = id,
                Name = rows.First().Name,
                Surname = rows.First().Surname,
                Age = rows.First().Age,
                Departments = departments.Select(d => d.ToListItem()),
                DepartmentId = rows.First().DepartmentId.ToString(),
                GenderId = rows.First().GenderId.ToString(),
                Genders = genders.Select(g => g.ToListItem()),
                AllProgramLanguages = programLanguages.Select(p => p.ToListItem()),
                ProgramLanguages = new List<string>()
            };

            foreach (var row in rows)
            {
                model.ProgramLanguages.Add(row.ProgramLanguageId.ToString());
            }

            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            var languages = (await _context.ProgramLanguages.ToArrayAsync()).Select(p => p.ToListItem());
            var genders = (await _context.Genders.ToArrayAsync()).Select(g => g.ToListItem());
            var departments = (await _context.Departments.ToArrayAsync()).Select(d => d.ToListItem());

            var model = new EmployeeAddModel
            {
                AllProgramLanguages = languages,
                Genders = genders,
                Departments = departments,
            };
            return View(model);
        }

        public async Task<IActionResult> AddEmployee(EmployeeAddModel employeeAddModel)
        {
            var employee = new Employee
            {
                Age = employeeAddModel.Age,
                Gender = int.Parse(employeeAddModel.GenderId),
                Name = employeeAddModel.Name,
                Surname = employeeAddModel.Surname,
                DepartmentId = int.Parse(employeeAddModel.DepartmentId)
            };
            
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var employeeAdded = await _context.AddAsync(employee);
                    await employeeAdded.Context.SaveChangesAsync();
                    var experience = employeeAddModel.ProgramLanguages.Select(l => new Experience
                    {
                        EmployeeId = employeeAdded.Entity.Id,
                        ProgramLanguageId = int.Parse(l)
                    }).ToArray();
                    await _context.AddRangeAsync(experience);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditEmployee(EmployeeEditModel employeeModel)
        {
            var employee = new Employee
            {
                Age = employeeModel.Age,
                Gender = int.Parse(employeeModel.GenderId),
                Name = employeeModel.Name,
                Surname = employeeModel.Surname,
                DepartmentId = int.Parse(employeeModel.DepartmentId),
                Id = employeeModel.Id
            };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var storedExperience = await _context.Experiences.Where(e => e.EmployeeId == employeeModel.Id)
                        .ToArrayAsync();

                    var edited = _context.Update(employee);
                    var newExperience = employeeModel.ProgramLanguages.Select(l => new Experience
                    {
                        EmployeeId = employeeModel.Id,
                        ProgramLanguageId = int.Parse(l)
                    }).ToArray();

                    var toDeleteExperiences = storedExperience.Except(newExperience, new ExperienceComparer()).ToArray();
                    var toAddExperience = newExperience.Except(storedExperience, new ExperienceComparer()).ToArray();

                    _context.RemoveRange(toDeleteExperiences);
                    await _context.AddRangeAsync(toAddExperience);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var remove = _context.Employees.Single(e => e.Id == id);
            remove.Deleted = true;
            _context.Update(remove);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employeeQueryable = from empl in _context.Employees
                where empl.Id == id
                join gender in _context.Genders on empl.Gender equals gender.Id
                join department in _context.Departments on empl.DepartmentId equals department.Id
                select new EmployeeModel
                {
                    Age = empl.Age,
                    Id = empl.Id,
                    Name = empl.Name,
                    Surname = empl.Surname,
                    DepartmentTitle = department.Title,
                    GenderTitle = gender.Title
                };
            var model = await employeeQueryable.FirstAsync();
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}