using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.ViewModels
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }
        public string Publication { get; set; }
        public int? MaxPages { get; set; }
        public int? PublishedYear { get; set; }

        // ✅ These should be List<SelectListItem>, not SelectList
        public List<SelectListItem> Authors { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> PublicationsList { get; set; }

        public List<BookSearchResultDto> Results { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class BookSearchResultDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Authors { get; set; }
        public string Publication { get; set; }
        public string Genre { get; set; }
        public int? Pages { get; set; }
        public string ShortDesc { get; set; }
        public int? PublishedYear { get; set; }
        public string Isbn { get; set; }
        public double? AverageRating { get; set; }
        public bool IsAvailable { get; set; }
    }
}