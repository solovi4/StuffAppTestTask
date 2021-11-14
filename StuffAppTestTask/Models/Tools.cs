using System;
using System.Collections.Generic;
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

    public class ExperienceComparer : IEqualityComparer<Experience>
    {
        public bool Equals(Experience x, Experience y)
        {
            if (x == null || y == null)
                return false;
            return x.EmployeeId == y.EmployeeId && x.ProgramLanguageId == y.ProgramLanguageId;
        }

        public int GetHashCode(Experience obj)
        {
            return HashCode.Combine(obj.EmployeeId, obj.ProgramLanguageId);
        }
    }
}