using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask
{
    [Table("programlanguage")]
    public class ProgramLanguage
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
    }
}