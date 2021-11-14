using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuffAppTestTask.DB
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("login")] public string Login { get; set; }

        [Column("password")] public string Password { get; set; }

        [Column("lastactiondate")] public DateTime? LastActionDate { get; set; }
    }
}