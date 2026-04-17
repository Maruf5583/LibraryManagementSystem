using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<MemberRole> MemberRoles { get; set; } = new List<MemberRole>();
}
