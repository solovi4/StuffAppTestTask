using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("Experience")]
    public class Experience
    {
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }
        [Column("ProgramLanguageId")]
        public int ProgramLanguageId { get; set; }
    }
}