namespace Librairie.Repositories
{
    using Librairie.Data;
    using Librairie.Models;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class AuthorBookRepository : Repository<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public EntityEntry<AuthorBook> Add(int authorId, int bookId)
        {
            return _dbSet.Add(new AuthorBook() { AuthorId = authorId, BookId = bookId });
        }
    }
}
