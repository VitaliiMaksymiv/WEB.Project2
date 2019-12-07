namespace Librairie.Controllers
{
    using AutoMapper;
    using Infrastructure;
    using Repositories;
    using ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly CartVM _cart;

        private readonly IMapper _mapper;

        public CartController(IUnitOfWork unitOfWork, CartVM cartService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cart = cartService;
            _mapper = mapper;
        }

        public IActionResult AddToCart(int bookId)
        {
            var book = _unitOfWork.BookRepository.Get(p => p.Id == bookId).FirstOrDefault();
            if (book != null)
            {
                _cart.AddItem(_mapper.Map<BookVM>(book), 1);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public RedirectToActionResult RemoveFromCart(int bookId)
        {
            _cart.RemoveLine(bookId);
            return RedirectToAction("Index");
        }

        public ViewResult Index()
        {
            return View(GetCart());
        }

        private CartVM GetCart()
        {
            CartVM cart = HttpContext.Session.GetJson<CartVM>("Cart") ?? new CartVM();
            return cart;
        }
    }
}