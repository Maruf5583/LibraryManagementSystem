using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class MemberRole
{
    public int Id { get; set; }

    public int? MemberId { get; set; }

    public int? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Role? Role { get; set; }
}
