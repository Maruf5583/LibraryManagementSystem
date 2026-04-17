using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class BookCopy
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public string? Barcode { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Condition { get; set; }

    public virtual Book? Book { get; set; }

    public virtual ICollection<BookReservation> BookReservations { get; set; } = new List<BookReservation>();
}
