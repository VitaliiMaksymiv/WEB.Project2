namespace Librairie.Repositories
{
    using System;
    using Librairie.Data;
    using Librairie.Models;
    using Microsoft.AspNetCore.Identity;

    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext DbContext { get; set; }

        IAuthorBookRepository AuthorBookRepository { get; set; }

        IRepository<Book> BookRepository { get; set; }

        IRepository<Author> AuthorRepository { get; set; }

        UserManager<User> UserManager { get; set; }

        SignInManager<User> SignInManager { get; set; }

        RoleManager<IdentityRole<int>> RoleManager { get; set; }

        void SaveChanges();
    }
}