using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StuffAppTestTask.Models
{
    public class EmployeeAddEditModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public string GenderId { get; set; }
        public string DepartmentId { get; set; }
        public IEnumerable<SelectListItem> AllProgramLanguages { get; set; }
        public List<string> ProgramLanguages { get; set; }
    }
    
}