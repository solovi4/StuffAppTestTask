using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("Employee")]
    public class Employee
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Surname")]
        public string Surname { get; set; }

        [Column("Age")] public int Age { get; set; }

        [Column("Gender")] public int Gender { get; set; }

        [Column("DepartmentId")] public int DepartmentId { get; set; }
    }
}