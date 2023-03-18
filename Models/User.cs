using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Library.Models
{
    public class User
    {
        [Required]
        public int userId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string? firstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string? lastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        [Display(Name = "Password")]
        public string? password { get; set; }
        public List<Book> books { get; set; } = new List<Book>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        [NotMapped]
        [Compare("password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string confirm { get; set; }
    }
}

public class LoginUser
{
    // No other fields!
    [Required]
    [Display(Name = "Email")]
    public string email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string password { get; set; }
}
