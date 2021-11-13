using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("department")]
    public class Department
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("floor")]
        public int Floor { get; set; }
    }
}