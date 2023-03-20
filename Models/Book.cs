using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Library.Models
{
    public class Book
    {

        public int bookId { get; set; }
        public int userId { get; set; }
        public int categoryId { get; set; }
        [Display(Name = "Book Title")]
        public string? bookTitle { get; set; }
        [Display(Name = "Book Author")]
        public string? bookAuthor { get; set; }
        [Display(Name = "Your opinion about this book")]
        public string? bookDescription { get; set; }
        [Display(Name = "Select the category of the book")]
        public string? bookCategory { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        [Display(Name = "Upload an cover for the Book")]
        public IFormFile file { get; set; }
        public User? bookCreator { get; set; }
        public List<Like> booklikes { get; set; } = new List<Like>();
        public List<Review> bookReviews { get; set; } = new List<Review>();


    }
}