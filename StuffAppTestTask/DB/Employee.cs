using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("employee")]
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        
        [Column("surname")]
        public string Surname { get; set; }

        [Column("age")] public int Age { get; set; }

        [Column("gender")] public int Gender { get; set; }

        [Column("departmentid")] public int DepartmentId { get; set; }
        [Column("deleted")] public bool Deleted { get; set; }
    }
}