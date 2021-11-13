using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("gender")]
    public class Gender
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }
    }
}