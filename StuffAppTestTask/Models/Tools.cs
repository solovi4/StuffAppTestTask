using Microsoft.AspNetCore.Mvc.Rendering;
using StuffAppTestTask.DB;

namespace StuffAppTestTask.Models
{
    public static class Tools
    {
        public static SelectListItem ToListItem(this Department department)
        {
            return new SelectListItem
            {
                Text = $"{department.Title} этаж {department.Floor}",
                Value = department.Id.ToString()
            };
        }

        public static SelectListItem ToListItem(this ProgramLanguage programLanguage)
        {
            return new SelectListItem
            {
                Text = programLanguage.Title,
                Value = programLanguage.Id.ToString()
            };
        }

        public static SelectListItem ToListItem(this Gender gender)
        {
            return new SelectListItem
            {
                Text = gender.Title,
                Value = gender.Id.ToString()
            };
        }
    }
}