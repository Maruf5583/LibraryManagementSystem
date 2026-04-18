using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class BookCreateViewModels
    {
        [Required(ErrorMessage = "Book title is required")]
        [Display(Name = "Book Title")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Display(Name = "Short Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string ShortDesc { get; set; }

        [Display(Name = "ISBN")]
        [StringLength(50, ErrorMessage = "ISBN cannot exceed 50 characters")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Please select a genre")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Display(Name = "Publication")]
        [StringLength(200, ErrorMessage = "Publication name cannot exceed 200 characters")]
        public string Publication { get; set; }

        [Display(Name = "Published Year")]
        [Range(1000, 2025, ErrorMessage = "Please enter a valid year between 1000 and 2025")]
        public int PublishedYear { get; set; }

        [Display(Name = "Pages")]
        [Range(1, 5000, ErrorMessage = "Pages must be between 1 and 5000")]
        public int Pages { get; set; }

        [Display(Name = "Length (inches)")]
        [Range(0, 20, ErrorMessage = "Length must be between 0 and 20 inches")]
        public decimal Length { get; set; }

        [Display(Name = "Width (inches)")]
        [Range(0, 20, ErrorMessage = "Width must be between 0 and 20 inches")]
        public decimal Width { get; set; }

        [Display(Name = "Height (inches)")]
        [Range(0, 20, ErrorMessage = "Height must be between 0 and 20 inches")]
        public decimal Height { get; set; }

        [Display(Name = "Book Cover Image")]
        public string Image { get; set; }

        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Please select at least one author")]
        [Display(Name = "Authors")]
        public List<int> AuthorIds { get; set; }

        // For dropdown lists
        public List<SelectListItem> GenreList { get; set; }
        public List<SelectListItem> AuthorList { get; set; }
    }

    public class BookEditViewModel : BookCreateViewModel
    {
        public int Id { get; set; }
    }
}