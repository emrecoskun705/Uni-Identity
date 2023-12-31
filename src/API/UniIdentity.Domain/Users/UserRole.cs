﻿using UniIdentity.Domain.Roles;

namespace UniIdentity.Domain.Users;

public class UserRole
{
    public UserId UserId { get; set; }
    public RoleId RoleId { get; set; }
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}