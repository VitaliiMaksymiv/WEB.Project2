namespace Librairie.Repositories
{
    using Models;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IAuthorBookRepository : IRepository<AuthorBook>
    {
        EntityEntry<AuthorBook> Add(int authorId, int bookId);
    }
}
