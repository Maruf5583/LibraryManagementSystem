using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class BookReview
{
    public int Id { get; set; }

    public int? MemberId { get; set; }

    public int? BookId { get; set; }

    public int? Ratings { get; set; }

    public bool? IsRecommended { get; set; }

    public string? Comments { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime? ReviewedOn { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Member? Member { get; set; }
}
