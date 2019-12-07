namespace Librairie.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Repositories;
    using ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ShopController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(ShopVM shopVM)
        {
            var bookVMs = _unitOfWork.BookRepository.Get().ProjectTo<BookVM>(_mapper.ConfigurationProvider);
            if (!string.IsNullOrWhiteSpace(shopVM.ToFind))
            {
                bookVMs = bookVMs.Where(p =>
                    p.Description.Contains(shopVM.ToFind) ||
                    p.Title.Contains(shopVM.ToFind) ||
                    p.Category.Contains(shopVM.ToFind));
            }

            shopVM.PageSize = shopVM.PageSize == 0 ? 6 : shopVM.PageSize;
            shopVM.PageCount = (int)decimal.Ceiling((decimal)bookVMs.Count() / shopVM.PageSize);
            shopVM.PageNumber = shopVM.PageNumber > shopVM.PageCount ? shopVM.PageCount : shopVM.PageNumber;
            shopVM.PageNumber = shopVM.PageNumber < shopVM.PageCount ? 1 : shopVM.PageNumber;
            if (shopVM.PageCount > 0)
            {
                switch (shopVM.OrderBy)
                {
                    case OrderByType.PriceUp:
                        {
                            bookVMs = bookVMs.OrderBy(p => p.Price);
                            break;
                        }

                    case OrderByType.PriceDown:
                        {
                            bookVMs = bookVMs.OrderByDescending(p => p.Price);
                            break;
                        }

                    case OrderByType.TitleUp:
                        {
                            bookVMs = bookVMs.OrderBy(p => p.Title);
                            break;
                        }

                    case OrderByType.TitleDown:
                        {
                            bookVMs = bookVMs.OrderByDescending(p => p.Title);
                            break;
                        }

                    case OrderByType.AuthorUp:
                        {
                            bookVMs = bookVMs.OrderBy(p => p.Authors.Min(q => q.LastName));
                            break;
                        }

                    case OrderByType.AuthorDown:
                        {
                            bookVMs = bookVMs.OrderByDescending(p => p.Authors.Max(q => q.LastName));
                            break;
                        }

                    default:
                        break;
                }

                shopVM.ShopItems = bookVMs.Skip((shopVM.PageNumber - 1) * shopVM.PageSize).Take(shopVM.PageSize).ToList();
            }

            return View(shopVM);
        }
    }
}