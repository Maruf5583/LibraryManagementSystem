using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public string? SubscriptionName { get; set; }

    public decimal? YearlyFee { get; set; }

    public int? BookingCount { get; set; }

    public int? BookingTime { get; set; }

    public virtual ICollection<MemberSubscription> MemberSubscriptions { get; set; } = new List<MemberSubscription>();
}
