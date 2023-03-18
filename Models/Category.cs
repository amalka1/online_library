using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Library.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string? categoryName { get; set; }
        public List<Book> categoryBooks { get; set; }

    }
    }