using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ShortDesc { get; set; }

    public string Isbn { get; set; }

    public int GenreId { get; set; }

    public string Publication { get; set; }

    public int PublishedYear { get; set; }

    public int Pages { get; set; }

    public decimal Length { get; set; }

    public decimal Width { get; set; }

    public decimal Height { get; set; }

    public string Image { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    public virtual ICollection<BookReview> BookReviews { get; set; } = new List<BookReview>();

    public virtual Genre Genre { get; set; }
   
}
