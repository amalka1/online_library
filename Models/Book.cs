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
        public int userId { get; set;}
        public int categoryId {get; set; }
        public string? bookTitle { get; set;}
        public string? bookAuthor {get; set;}
        public string? bookDescription { get; set; } 
        public string? bookCategory { get; set; }
        public string? ImagePath { get; set; }  
        [NotMapped]
        public IFormFile file { get; set; }
        public User? bookCreator {get; set;}
        public List <Like>booklikes { get; set; } = new List<Like>();
        public List <Review> bookReviews {get; set; } = new List<Review> ();
        

    }
}