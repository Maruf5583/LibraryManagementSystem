using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;  // ✅ Add this
using System.IO;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly LmsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(LmsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Book/Index - List all books
        public async Task<IActionResult> Index(
            string searchText = null,
            int? authorId = null,
            int? genreId = null,
            string publication = null,
            int? maxPages = null,
            int? publishedYear = null,
            int page = 1)
        {
            // Load Dropdowns as List<SelectListItem>
            var authors = await _context.Authors
                .OrderBy(a => a.AuthorName)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.AuthorName
                })
                .ToListAsync();
            authors.Insert(0, new SelectListItem { Value = "", Text = "-- All Authors --" });

            var genres = await _context.Genres
                .OrderBy(g => g.GenreName)
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.GenreName
                })
                .ToListAsync();
            genres.Insert(0, new SelectListItem { Value = "", Text = "-- All Genres --" });

            var publicationList = await _context.Books
                .Where(b => b.Publication != null && b.Publication != "")
                .Select(b => b.Publication)
                .Distinct()
                .OrderBy(p => p)
                .Select(p => new SelectListItem
                {
                    Value = p,
                    Text = p
                })
                .ToListAsync();
            publicationList.Insert(0, new SelectListItem { Value = "", Text = "-- All Publications --" });

            // Build Query
            var query = _context.Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookCopies)
                .Include(b => b.BookReviews)
                .AsQueryable();

            // Apply Filters
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(b => b.Title.Contains(searchText) ||
                                         (b.Isbn != null && b.Isbn.Contains(searchText)));
            }

            if (authorId.HasValue && authorId.Value > 0)
            {
                query = query.Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId.Value));
            }

            if (genreId.HasValue && genreId.Value > 0)
            {
                query = query.Where(b => b.GenreId == genreId.Value);
            }

            if (!string.IsNullOrEmpty(publication))
            {
                query = query.Where(b => b.Publication != null && b.Publication == publication);
            }

            if (maxPages.HasValue && maxPages.Value > 0)
            {
                query = query.Where(b => b.Pages <= maxPages.Value);
            }

            if (publishedYear.HasValue && publishedYear.Value > 0)
            {
                query = query.Where(b => b.PublishedYear == publishedYear.Value);
            }

            // Pagination
            int pageSize = 10;
            var totalCount = await query.CountAsync();
            var books = await query
                .OrderByDescending(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var results = books.Select(b => new BookSearchResultDto
            {
                Id = b.Id,
                Title = b.Title ?? "Unknown",
                Image = b.Image ?? "/images/default-book.jpg",
                Authors = b.BookAuthors.Any() ? string.Join(" | ", b.BookAuthors.Select(ba => ba.Author.AuthorName)) : "No Author",
                Publication = b.Publication ?? "N/A",
                Genre = b.Genre?.GenreName ?? "Unknown",
                Pages = b.Pages,
                Isbn = b.Isbn ?? "N/A",
                IsAvailable = b.BookCopies.Any(c => c.IsAvailable == true)
            }).ToList();

            var viewModel = new SearchViewModel
            {
                SearchText = searchText,
                AuthorId = authorId,
                GenreId = genreId,
                Publication = publication,
                MaxPages = maxPages,
                PublishedYear = publishedYear,
                Results = results,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize,
                Authors = authors,
                Genres = genres,
                PublicationsList = publicationList
            };

            return View(viewModel);
        }

        // GET: Book/Details/5 - Book details with related books
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookCopies)
                .Include(b => b.BookReviews)
                    .ThenInclude(r => r.Member)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            // Calculate average rating
            var approvedReviews = book.BookReviews.Where(r => r.IsApproved == true).ToList();
            var averageRating = approvedReviews.Any() ? approvedReviews.Average(r => r.Ratings) ?? 0 : 0;

            // Get available copies count
            var availableCopies = book.BookCopies.Count(c => c.IsAvailable == true);
            var totalCopies = book.BookCopies.Count();

            // Create view model
            var viewModel = new BookDetailViewModel
            {
                Id = book.Id,
                Title = book.Title ?? "Unknown",
                ShortDesc = book.ShortDesc ?? "No description available.",
                Isbn = book.Isbn ?? "N/A",
                Genre = book.Genre?.GenreName ?? "Unknown",
                GenreId = book.GenreId  ,
                Publication = book.Publication ?? "N/A",
                PublishedYear = book.PublishedYear,
                Pages = book.Pages,
                Length = book.Length,
                Width = book.Width,
                Height = book.Height,
                Image = book.Image ?? "/images/default-book.jpg",
                Authors = book.BookAuthors.Select(ba => new AuthorDto
                {
                    Id = ba.Author.Id,
                    Name = ba.Author.AuthorName
                }).ToList(),
                AverageRating = averageRating,
                TotalReviews = approvedReviews.Count,
                AvailableCopies = availableCopies,
                TotalCopies = totalCopies,
                IsAvailable = availableCopies > 0,
                Reviews = approvedReviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    MemberName = r.Member?.Name ?? "Anonymous",
                    Rating = r.Ratings ?? 0,
                    Comment = r.Comments ?? "",
                    ReviewedOn = r.ReviewedOn ?? DateTime.Now,
                    IsRecommended = r.IsRecommended ?? false
                }).OrderByDescending(r => r.ReviewedOn).ToList()
            };

            // Get related books (same genre or same author)
            viewModel.RelatedBooks = await GetRelatedBooks(book);

            return View(viewModel);
        }

        private async Task<List<RelatedBookDto>> GetRelatedBooks(Book currentBook)
        {
            var relatedBooks = new List<RelatedBookDto>();

            // Get books from same genre (excluding current book)
            if (currentBook.GenreId != 0)
            {
                var sameGenreBooks = await _context.Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.GenreId == currentBook.GenreId && b.Id != currentBook.Id)
                    .Take(4)
                    .Select(b => new RelatedBookDto
                    {
                        Id = b.Id,
                        Title = b.Title ?? "Unknown",
                        Image = b.Image ?? "/images/default-book.jpg",
                        Authors = string.Join(" | ", b.BookAuthors.Select(ba => ba.Author.AuthorName)),
                        Genre = b.Genre != null ? b.Genre.GenreName : "Unknown",
                        PublishedYear = b.PublishedYear,
                        RelationType = "Same Genre"
                    })
                    .ToListAsync();

                relatedBooks.AddRange(sameGenreBooks);
            }

            // If less than 4 books, get books from same authors
            if (relatedBooks.Count < 4 && currentBook.BookAuthors.Any())
            {
                var authorIds = currentBook.BookAuthors.Select(ba => ba.AuthorId).ToList();
                var sameAuthorBooks = await _context.Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.BookAuthors.Any(ba => authorIds.Contains(ba.AuthorId)) && b.Id != currentBook.Id)
                    .Take(4 - relatedBooks.Count)
                    .Select(b => new RelatedBookDto
                    {
                        Id = b.Id,
                        Title = b.Title ?? "Unknown",
                        Image = b.Image ?? "/images/default-book.jpg",
                        Authors = string.Join(" | ", b.BookAuthors.Select(ba => ba.Author.AuthorName)),
                        Genre = b.Genre != null ? b.Genre.GenreName : "Unknown",
                        PublishedYear = b.PublishedYear,
                        RelationType = "Same Author"
                    })
                    .ToListAsync();

                relatedBooks.AddRange(sameAuthorBooks);
            }

            // If still less than 4, get recent books
            if (relatedBooks.Count < 4)
            {
                var recentBooks = await _context.Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.Id != currentBook.Id && !relatedBooks.Select(r => r.Id).Contains(b.Id))
                    .OrderByDescending(b => b.Id)
                    .Take(4 - relatedBooks.Count)
                    .Select(b => new RelatedBookDto
                    {
                        Id = b.Id,
                        Title = b.Title ?? "Unknown",
                        Image = b.Image ?? "/images/default-book.jpg",
                        Authors = string.Join(" | ", b.BookAuthors.Select(ba => ba.Author.AuthorName)),
                        Genre = b.Genre != null ? b.Genre.GenreName : "Unknown",
                        PublishedYear = b.PublishedYear,
                        RelationType = "New Arrival"
                    })
                    .ToListAsync();

                relatedBooks.AddRange(recentBooks);
            }

            return relatedBooks;
        }

        // ==================== CREATE BOOK ====================

        // GET: Book/Create
        public IActionResult Create()
        {
            var viewModel = new BookCreateViewModels
            {
                GenreList = _context.Genres
                    .OrderBy(g => g.GenreName)
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.GenreName
                    }).ToList(),

                AuthorList = _context.Authors
                    .OrderBy(a => a.AuthorName)
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.AuthorName
                    }).ToList(),

                AuthorIds = new List<int>()
            };

            viewModel.GenreList.Insert(0, new SelectListItem { Value = "", Text = "-- Select Genre --" });

            return View(viewModel);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateViewModels model)
        {
            // Remove validation errors for dropdown lists
            ModelState.Remove("GenreList");
            ModelState.Remove("AuthorList");
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                string imagePath = "/images/default-book.jpg";

                // Handle image upload to images/books folder
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    try
                    {
                        // Create folder path: wwwroot/images/books
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");

                        // Create directory if it doesn't exist
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Generate unique filename
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(fileStream);
                        }

                        imagePath = "/images/books/" + uniqueFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Image upload failed: {ex.Message}");
                        return View(model);
                    }
                }

                // Create new book
                var book = new Book
                {
                    Title = model.Title,
                    ShortDesc = model.ShortDesc,
                    Isbn = model.Isbn,
                    GenreId = model.GenreId,
                    Publication = model.Publication,
                    PublishedYear = model.PublishedYear,
                    Pages = model.Pages,
                    Length = model.Length,
                    Width = model.Width,
                    Height = model.Height,
                    Image = imagePath,
                    CreatedAt = DateTime.Now
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                // Add authors
                if (model.AuthorIds != null && model.AuthorIds.Any())
                {
                    foreach (var authorId in model.AuthorIds)
                    {
                        _context.BookAuthors.Add(new BookAuthor
                        {
                            BookId = book.Id,
                            AuthorId = authorId
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = "Book created successfully!";
                return RedirectToAction(nameof(Details), new { id = book.Id });
            }

            // Reload dropdowns if validation fails
            model.GenreList = _context.Genres
                .OrderBy(g => g.GenreName)
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.GenreName,
                    Selected = g.Id == model.GenreId
                }).ToList();
            model.GenreList.Insert(0, new SelectListItem { Value = "", Text = "-- Select Genre --" });

            model.AuthorList = _context.Authors
                .OrderBy(a => a.AuthorName)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.AuthorName,
                    Selected = model.AuthorIds != null && model.AuthorIds.Contains(a.Id)
                }).ToList();

            return View(model);
        }
    }
}