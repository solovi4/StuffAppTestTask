using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("experience")]
    public class Experience
    {
        [Column("employeeid")]
        public int EmployeeId { get; set; }
        [Column("programlanguageid")]
        public int ProgramLanguageId { get; set; }
    }
}