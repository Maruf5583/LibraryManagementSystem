using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly LmsContext _context;

        public HomeController(LmsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var next10Days = DateOnly.FromDateTime(DateTime.Today.AddDays(10));

            var viewModel = new VmHomePage
            {
                // ========== Statistics ==========
                TotalBooks = await _context.Books.CountAsync(),
                TotalCopies = await _context.BookCopies.CountAsync(),
                TotalMembers = await _context.Members.CountAsync(m => m.IsActive == true),
                CurrentlyIssued = await _context.BookReservations.CountAsync(r => r.IsComplete == false),
                TotalReviews = await _context.BookReviews.CountAsync(r => r.IsApproved == true),

                // ========== Top 5 Rated Books ==========
                TopRatedBooks = await _context.BookReviews
                    .Where(r => r.IsApproved == true && r.Book != null)
                    .GroupBy(r => r.BookId)
                    .Select(g => new TopBookDto
                    {
                        Id = g.Key ?? 0,
                        Title = g.FirstOrDefault().Book.Title ?? "Unknown",
                        AverageRating = g.Average(r => r.Ratings) ?? 0,
                        ReviewCount = g.Count(),
                        CoverImage = g.FirstOrDefault().Book.Image ?? "/images/default-book.jpg"
                    })
                    .OrderByDescending(b => b.AverageRating)
                    .Take(5)
                    .ToListAsync(),

                // ========== Top 5 Most Recommended Books ==========
                MostRecommendedBooks = await _context.BookReviews
                    .Where(r => r.IsRecommended == true && r.IsApproved == true && r.Book != null)
                    .GroupBy(r => r.BookId)
                    .Select(g => new TopBookDto
                    {
                        Id = g.Key ?? 0,
                        Title = g.FirstOrDefault().Book.Title ?? "Unknown",
                        Count = g.Count(),
                        CoverImage = g.FirstOrDefault().Book.Image ?? "/images/default-book.jpg"
                    })
                    .OrderByDescending(b => b.Count)
                    .Take(5)
                    .ToListAsync(),

                // ========== Top 5 Most Reserved Books ==========
                MostReservedBooks = await _context.BookReservations
                    .Where(r => r.BookCopy != null && r.BookCopy.Book != null)
                    .GroupBy(r => r.BookCopy.BookId)
                    .Select(g => new TopBookDto
                    {
                        Id = g.Key ,
                        Title = g.FirstOrDefault().BookCopy.Book.Title ?? "Unknown",
                        Count = g.Count(),
                        CoverImage = g.FirstOrDefault().BookCopy.Book.Image ?? "/images/default-book.jpg"
                    })
                    .OrderByDescending(b => b.Count)
                    .Take(5)
                    .ToListAsync(),

                // ========== New Arrivals (Top 6) ==========
                NewArrivals = await _context.Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.CreatedAt != null)
                    .OrderByDescending(b => b.CreatedAt)
                    .Take(6)
                    .Select(b => new NewArrivalDto
                    {
                        Id = b.Id,
                        Title = b.Title ?? "Unknown",
                        Authors = string.Join(", ", b.BookAuthors.Select(ba => ba.Author.AuthorName)),
                        Genre = b.Genre != null ? b.Genre.GenreName : "Unknown",
                        Publication = b.Publication ?? "N/A",
                        CoverImage = b.Image ?? "/images/default-book.jpg",
                        AddedDate = b.CreatedAt
                    })
                    .ToListAsync(),

                // ========== Genre-wise Statistics ==========
                GenreWiseStats = await _context.Genres
                    .Select(g => new GenreStatsDto
                    {
                        GenreName = g.GenreName ?? "Unknown",
                        BookCount = g.Books.Count(),
                        Percentage = _context.Books.Count() > 0 ?
                            (decimal)g.Books.Count() * 100 / _context.Books.Count() : 0
                    })
                    .OrderByDescending(g => g.BookCount)
                    .Take(8)
                    .ToListAsync(),

                // ========== Books Returning in Next 10 Days ==========
                ReturningBooks = await _context.BookReservations
                    .Where(r => r.IsComplete == false &&
                                r.ToDate >= today &&
                                r.ToDate     <= next10Days)
                    .Include(r => r.BookCopy)
                        .ThenInclude(bc => bc.Book)
                    .Include(r => r.Member)
                    .Select(r => new ReturningBookDto
                    {
                        Id = r.Id,
                        BookTitle = r.BookCopy.Book.Title ?? "Unknown",
                        MemberName = r.Member.Name ?? "Unknown",
                        ReturnDate =r.ToDate,
                        CoverImage = r.BookCopy.Book.Image ?? "/images/default-book.jpg"
                    })
                    .Take(5)
                    .ToListAsync(),

                // ========== Upcoming Books (Coming Soon) ==========
                UpcomingBooks = new List<UpcomingBookDto>
                {
                    new UpcomingBookDto {
                        Id = 1,
                        Title = "The Future of AI",
                        Authors = "Dr. Alan Turing",
                        CoverImage = "/images/upcoming/ai-future.jpg",
                        ExpectedDate = DateTime.Now.AddMonths(1),
                        Description = "Exploring artificial intelligence and its impact on society."
                    },
                    new UpcomingBookDto {
                        Id = 2,
                        Title = "Cloud Architecture Patterns",
                        Authors = "Martin Fowler",
                        CoverImage = "/images/upcoming/cloud-patterns.jpg",
                        ExpectedDate = DateTime.Now.AddMonths(2),
                        Description = "Design patterns for cloud-native applications."
                    },
                    new UpcomingBookDto {
                        Id = 3,
                        Title = "Sustainable Development Goals",
                        Authors = "UN Publications",
                        CoverImage = "/images/upcoming/sdg.jpg",
                        ExpectedDate = DateTime.Now.AddMonths(1),
                        Description = "A guide to achieving sustainable development goals."
                    }
                }
            };

            return View(viewModel);
        }
    }
}