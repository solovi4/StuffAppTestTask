using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index()
        {
            var employees = _context.Employees.ToArray();
            var employeeModels = employees.Select(e => new EmployeeModel
            {
                Age = e.Age,
                Name = e.Name,
                Surname = e.Surname
            });
            var model = new EmployeeListModel
            {
                EmployeeList = employeeModels
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Add()
        {
            var languages = _context.ProgramLanguages.ToArray()
                .Select(p => new SelectListItem(p.Title, p.Id.ToString()));

            var genders = _context.Genders.ToArray().Select(g => new SelectListItem(g.Title, g.Id.ToString()));
            var departments = _context.Departments.ToArray()
                .Select(d => new SelectListItem($"{d.Title} этаж {d.Floor}", d.Id.ToString()));

            var model = new EmployeeModel
            {
                AllProgramLanguages = languages,
                Genders = genders,
                Departments = departments,
            };
            return View(model);
        }

        public async Task<IActionResult> AddEmployee(EmployeeModel employeeModel)
        {
            var employee = new Employee
            {
                Age = employeeModel.Age,
                Gender = int.Parse(employeeModel.GenderId),
                Name = employeeModel.Name,
                Surname = employeeModel.Surname,
                DepartmentId = int.Parse(employeeModel.DepartmentId)
            };
            
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var employeeAdded = await _context.AddAsync(employee);
                    await employeeAdded.Context.SaveChangesAsync();
                    var experience = employeeModel.ProgramLanguages.Select(l => new Experience
                    {
                        EmployeeId = employeeAdded.Entity.Id,
                        ProgramLanguageId = int.Parse(l)
                    });
                    await _context.AddRangeAsync(experience);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            
            return RedirectToAction("Index");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}