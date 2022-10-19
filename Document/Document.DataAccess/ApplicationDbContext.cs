using Document.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.DataAccess;

public class ApplicationDbContext: DbContext
{
    public DbSet<BlobFile> BlobFiles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}