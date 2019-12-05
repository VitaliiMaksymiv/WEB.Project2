namespace Librairie.Repositories
{
    using Librairie.Data;
    using Librairie.Models;
    using Microsoft.AspNetCore.Identity;

    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ApplicationDbContext context,
            IAuthorBookRepository authorBookRepository,
            IRepository<Book> bookRepository,
            IRepository<Author> authorRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            DbContext = context;
            AuthorBookRepository = authorBookRepository;
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationDbContext DbContext { get; set; }

        public IAuthorBookRepository AuthorBookRepository { get; set; }

        public IRepository<Book> BookRepository { get; set; }

        public IRepository<Author> AuthorRepository { get; set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        public RoleManager<IdentityRole<int>> RoleManager { get; set; }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}