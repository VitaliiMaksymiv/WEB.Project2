namespace Librairie.Repositories
{
    using Librairie.Models;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IAuthorBookRepository : IRepository<AuthorBook>
    {
        EntityEntry<AuthorBook> Add(int authorId, int bookId);
    }
}
