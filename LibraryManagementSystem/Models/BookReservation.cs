using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class BookReservation
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int BookCopyId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public bool IsComplete { get; set; }

    public bool IsViolation { get; set; }

    public string ViolationReason { get; set; }

    public string ViolationRemarks { get; set; }

    public int LastUpdatedBy { get; set; }

    public DateTime LastUpdatedOn { get; set; }

    public virtual BookCopy BookCopy { get; set; }

    public virtual Member Member { get; set; }
}
