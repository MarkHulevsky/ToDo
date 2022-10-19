using Document.DataAccess.Entities;
using Document.DataAccess.Repositories.Interfaces;

namespace Document.DataAccess.Repositories;

public class BlobFileRepository: BaseRepository<BlobFile>, IBlobFileRepository
{
    public BlobFileRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}