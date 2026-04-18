// VmHomePage.cs
namespace LibraryManagementSystem.ViewModels
{
    public class VmHomePage
    {
        // Existing properties
        public List<NewArrivalDto> NewArrivals { get; set; }
        public List<GenreStatsDto> GenreWiseStats { get; set; }
        public List<ReturningBookDto> ReturningBooks { get; set; }

        // New properties for enhanced UI
        public List<TopBookDto> TopRatedBooks { get; set; }
        public List<TopBookDto> MostRecommendedBooks { get; set; }
        public List<TopBookDto> MostReservedBooks { get; set; }
        public List<UpcomingBookDto> UpcomingBooks { get; set; }

        // Statistics
        public int TotalBooks { get; set; }
        public int TotalMembers { get; set; }
        public int TotalReviews { get; set; }
        public int CurrentlyIssued { get; set; }
        public int TotalCopies { get; set; }
    }

    public class NewArrivalDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Genre { get; set; }
        public string Publication { get; set; }
        public string CoverImage { get; set; }
        public DateTime? AddedDate { get; set; }
    }

    public class GenreStatsDto
    {
        public string GenreName { get; set; }
        public int BookCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class ReturningBookDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
        public DateOnly ReturnDate { get; set; }
        public string CoverImage { get; set; }
    }

    public class TopBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public int Count { get; set; }  // For reservations/recommendations count
        public string CoverImage { get; set; }
        public string Genre { get; set; }
    }

    public class UpcomingBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string CoverImage { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string Description { get; set; }
    }
}