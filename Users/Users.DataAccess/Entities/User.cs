using Microsoft.AspNetCore.Identity;

namespace Users.DataAccess.Entities;

public class User: IdentityUser<Guid>
{
    public DateTime DateCreated { get; set; }

    public User()
    {
        DateCreated = DateTime.UtcNow;
    }
}