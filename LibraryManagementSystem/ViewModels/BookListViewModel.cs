// BookListViewModel.cs
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class BookListViewModel
    {
        public List<BookListDto> Books { get; set; }
        public string SearchText { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class BookListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Authors { get; set; }
        public string Genre { get; set; }
        public string Publication { get; set; }
        public int? PublishedYear { get; set; }
        public int? Pages { get; set; }
        public bool IsAvailable { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }

    // BookDetailViewModel.cs
    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public int? GenreId { get; set; }
        public string Publication { get; set; }
        public int? PublishedYear { get; set; }
        public int? Pages { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string Image { get; set; }
        public List<AuthorDto> Authors { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public bool IsAvailable { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<RelatedBookDto> RelatedBooks { get; set; }
    }

    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ReviewDto
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewedOn { get; set; }
        public bool IsRecommended { get; set; }
    }

    public class RelatedBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Authors { get; set; }
        public string Genre { get; set; }
        public int? PublishedYear { get; set; }
        public string RelationType { get; set; }
    }

    // BookCreateViewModel.cs
    public class BookCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        public string ShortDesc { get; set; }

        public string Isbn { get; set; }

        [Required]
        public int? GenreId { get; set; }

        public string Publication { get; set; }

        public int? PublishedYear { get; set; }

        public int? Pages { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        public string Image { get; set; }

        [Required]
        public List<int> AuthorIds { get; set; }
        public List<SelectListItem> GenreList { get; internal set; }
    }
}