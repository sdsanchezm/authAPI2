using System;
using System.ComponentModel.DataAnnotations;


namespace AuthenticationAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        public string Username { get; set; }

        //[Required]
        //[StringLength(100)]
        public string PasswordHash { get; set; }
    }
}
