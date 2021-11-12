using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask
{
    [Table("ProgramLanguage")]
    public class ProgramLanguage
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string Title { get; set; }
    }
}