using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ServerOne.Data;

public class BaseDbContext : IdentityDbContext<ApplicationUser>
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<IdentityUserRole<string>> UserRoles { get; set; }
    
}