using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Library.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int reviewValue { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        
        public User? user { get; set;}
        public Book? book { get; set; }
    }
}