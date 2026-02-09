using System;
using System.Collections.Generic;

namespace LoginSystem.Models.Database;

public partial class UserDetail
{
    public int UserDetailsId { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Age { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public string Gender { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
