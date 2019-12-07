namespace Librairie.Controllers
{
    using AutoMapper;
    using Models;
    using Repositories;
    using ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInVM details)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.UserManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await _unitOfWork.SignInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                    await _unitOfWork.SignInManager.PasswordSignInAsync(
                    user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect("/");
                    }
                }

                ModelState.AddModelError(
                    string.Empty,
                    "Invalid Email or Password");
            }

            return View(details);
        }

        public async Task<IActionResult> SignOut()
        {
            if (_unitOfWork.SignInManager.IsSignedIn(User))
            {
                await _unitOfWork.SignInManager.SignOutAsync();
            }

            return Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpVM details)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(details);

                var res = await _unitOfWork.UserManager.CreateAsync(user, details.Password);
                if (res.Succeeded)
                {
                    if (await _unitOfWork.RoleManager.RoleExistsAsync("User"))
                    {
                        await _unitOfWork.UserManager.AddToRoleAsync(user, "User");
                    }

                    return Redirect("/");
                }

                foreach (var v in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, v.Description);
                }
            }

            return View(details);
        }
    }
}