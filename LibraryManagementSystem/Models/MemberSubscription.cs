using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class MemberSubscription
{
    public int Id { get; set; }

    public int? MemberId { get; set; }

    public int? SubscriptionId { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public decimal? AmountPaid { get; set; }

    public string? PaymentMode { get; set; }

    public DateOnly? PayDate { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Subscription? Subscription { get; set; }
}
