namespace Librairie.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Librairie.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataSeeder
    {
        public static async Task CreateRoles(IApplicationBuilder app)
        {
            RoleManager<IdentityRole<int>> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole<int>>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("User"));
            }
        }

        public static async Task CreateAdmin(IApplicationBuilder app, IConfiguration configuration)
        {
            UserManager<User> userManager = app.ApplicationServices.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole<int>> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole<int>>>();
            string username = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string role = configuration["Data:AdminUser:Role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }

                User user = new User
                {
                    UserName = username,
                    Email = email,
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Books.Any() || (!context.Authors.Any() && !context.AuthorBooks.Any()))
            {
                context.Books.AddRange(
                    new Book()
                    {
                        Id = 1,
                        Title = "To Kill a Mockingbird",
                        Price = 64.99M,
                        Category = "Classics",
                    },
                    new Book()
                    {
                        Id = 2,
                        Title = "Harry Potter and the Sorcerer's Stone",
                        Price = 57.0M,
                        Category = "Fantasy",
                    },
                    new Book()
                    {
                        Id = 3,
                        Title = "Pride and Prejudice",
                        Price = 89.99M,
                        Category = "Classics",
                    },
                    new Book()
                    {
                        Id = 4,
                        Title = "The Diary of a Young Girl",
                        Price = 80.00M,
                        Category = "Autobiography",
                    },
                    new Book()
                    {
                        Id = 5,
                        Title = "1984",
                        Price = 49.99M,
                        Category = "Fiction",
                    },
                    new Book()
                    {
                        Id = 6,
                        Title = "The Little Prince",
                        Price = 94.99M,
                        Category = "Fantasy",
                    },
                    new Book()
                    {
                        Id = 7,
                        Title = "The Great Gatsby",
                        Price = 44.99M,
                        Category = "Novel",
                    },
                    new Book()
                    {
                        Id = 8,
                        Title = "The Catcher in the Rye",
                        Price = 90.00M,
                        Category = "Fiction",
                    },
                    new Book()
                    {
                        Id = 9,
                        Title = "Animal Farm",
                        Price = 85.99M,
                        Category = "Other",
                    },
                    new Book()
                    {
                        Id = 10,
                        Title = "The Lord of the Rings",
                        Price = 59.99M,
                        Category = "Fantasy",
                    });

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Books ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Books OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }

                context.Authors.AddRange(
                    new Author()
                    {
                        Id = 1,
                        FirstName = "Lee",
                        LastName = "Harper",
                    },
                    new Author()
                    {
                        Id = 2,
                        FirstName = "Joanne",
                        LastName = "Rowling",
                    },
                    new Author()
                    {
                        Id = 3,
                        FirstName = "Jane",
                        LastName = "Austen",
                    },
                    new Author()
                    {
                        Id = 4,
                        FirstName = "Anne",
                        LastName = "Frank",
                    },
                    new Author()
                    {
                        Id = 5,
                        FirstName = "George",
                        LastName = "Orwell",
                    },
                    new Author()
                    {
                        Id = 6,
                        FirstName = "Antoine",
                        LastName = "Saint-Exupéry",
                    },
                    new Author()
                    {
                        Id = 7,
                        FirstName = "Francis",
                        LastName = "Fitzgerald",
                    },
                    new Author()
                    {
                        Id = 8,
                        FirstName = "Jerome",
                        LastName = "Salinger",
                    },
                    new Author()
                    {
                        Id = 9,
                        FirstName = "John",
                        LastName = "Tolkien",
                    });

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Authors ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Authors OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }

                context.AuthorBooks.AddRange(
                    new AuthorBook()
                    {
                        AuthorId = 1,
                        BookId = 1,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 2,
                        BookId = 2,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 3,
                        BookId = 3,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 4,
                        BookId = 4,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 5,
                        BookId = 5,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 6,
                        BookId = 6,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 7,
                        BookId = 7,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 8,
                        BookId = 8,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 5,
                        BookId = 9,
                    },
                    new AuthorBook()
                    {
                        AuthorId = 9,
                        BookId = 10,
                    });

                try
                {
                    context.SaveChanges();
                }
                finally
                {
                }
            }
        }
    }
}