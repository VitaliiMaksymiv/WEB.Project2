namespace Librairie.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Models;
    using Repositories;
    using ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Books");
        }

        public async Task<IActionResult> Users()
        {
            List<UserVM> userVMs = new List<UserVM>();
            foreach (var user in _unitOfWork.UserManager.Users)
            {
                var role = (await _unitOfWork.UserManager.GetRolesAsync(user)).FirstOrDefault();
                if (role == null)
                {
                    role = string.Empty;
                }

                var userVM = _mapper.Map<UserVM>(user);
                userVM.Role = role;
                userVMs.Add(userVM);
            }

            return View(userVMs);
        }

        public IActionResult Books()
        {
            var books = _unitOfWork.BookRepository.Get().ProjectTo<BookVM>(_mapper.ConfigurationProvider).ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            var bookVM = new BookVM() { Authors = new List<AuthorVM>() };
            return View("EditBook", bookVM);
        }

        [HttpPost]
        public IActionResult AddBook(BookVM bookVM)
        {
            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var test = _mapper.Map<Book>(bookVM);
                    var bookEntry = _unitOfWork.BookRepository.Add(test);
                    _unitOfWork.SaveChanges();
                    bookEntry.Reload();
                    bookVM.Id = _mapper.Map<BookVM>(bookEntry.Entity).Id;
                    foreach (var authorVM in bookVM.Authors)
                    {
                        var newAuthorVM = _unitOfWork.AuthorRepository.Get(p => p.FirstName == authorVM.FirstName && p.LastName == authorVM.LastName).ProjectTo<AuthorVM>(_mapper.ConfigurationProvider).FirstOrDefault();
                        if (newAuthorVM == null)
                        {
                            var entityEntry = _unitOfWork.AuthorRepository.Add(_mapper.Map<Author>(authorVM));
                            _unitOfWork.SaveChanges();
                            entityEntry.Reload();
                            newAuthorVM = _mapper.Map<AuthorVM>(entityEntry.Entity);
                        }

                        _unitOfWork.AuthorBookRepository.Add(newAuthorVM.Id, bookVM.Id);
                        _unitOfWork.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return RedirectToAction("Books");
        }

        public IActionResult DeleteBook(int bookId)
        {
            _unitOfWork.BookRepository.Delete(bookId);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Books");
        }

        [HttpGet]
        public IActionResult EditBook(int bookId)
        {
            var book = _unitOfWork.BookRepository.Get(p => p.Id == bookId).ProjectTo<BookVM>(_mapper.ConfigurationProvider).FirstOrDefault();
            return View(book);
        }

        [HttpPost]
        public IActionResult EditBook(BookVM bookVM)
        {
            var oldBook = _unitOfWork.BookRepository.Get(bookVM.Id);
            var oldBookVM = _mapper.Map<BookVM>(oldBook);
            _unitOfWork.DbContext.Entry(oldBook).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            var deletedAuthors = oldBookVM.Authors.ToList();
            foreach (var item in oldBookVM.Authors)
            {
                var v = bookVM.Authors.Where(p => p.FirstName == item.FirstName && p.LastName == item.LastName).SingleOrDefault();
                if (bookVM.Authors.Remove(v))
                {
                    v = deletedAuthors.Where(p => p.FirstName == item.FirstName && p.LastName == item.LastName).SingleOrDefault();
                    deletedAuthors.Remove(v);
                }
            }

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var authorVM in bookVM.Authors)
                    {
                        var newAuthorVM = _unitOfWork.AuthorRepository.Get(p => p.FirstName == authorVM.FirstName && p.LastName == authorVM.LastName).ProjectTo<AuthorVM>(_mapper.ConfigurationProvider).FirstOrDefault();
                        if (newAuthorVM == null)
                        {
                            var entityEntry = _unitOfWork.AuthorRepository.Add(_mapper.Map<Author>(authorVM));
                            _unitOfWork.SaveChanges();
                            entityEntry.Reload();
                            newAuthorVM = _mapper.Map<AuthorVM>(entityEntry.Entity);
                        }

                        _unitOfWork.AuthorBookRepository.Add(newAuthorVM.Id, bookVM.Id);
                        _unitOfWork.SaveChanges();
                    }

                    foreach (var authorVM in deletedAuthors)
                    {
                        _unitOfWork.AuthorBookRepository.Delete(authorVM.Id, bookVM.Id);
                        _unitOfWork.SaveChanges();
                    }

                    _unitOfWork.BookRepository.Update(_mapper.Map<Book>(bookVM));
                    _unitOfWork.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return RedirectToAction("Books");
        }

        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _unitOfWork.UserManager.DeleteAsync(user);
                _unitOfWork.SaveChanges();
            }

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> SetUserRole(int userId, string roleName)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());
            if (user != null && await _unitOfWork.RoleManager.RoleExistsAsync(roleName))
            {
                await _unitOfWork.UserManager.RemoveFromRolesAsync(user, new string[] { "Admin", "User" });
                await _unitOfWork.UserManager.AddToRoleAsync(user, roleName);
                _unitOfWork.SaveChanges();
            }

            return RedirectToAction("Users");
        }
    }
}