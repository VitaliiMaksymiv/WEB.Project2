namespace Librairie.Components
{
    using ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Represents Cart Summary View Components.
    /// </summary>
    public class CartSummaryVC : ViewComponent
    {
        private readonly CartVM _cart;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartSummaryVC"/> class.
        /// </summary>
        /// <param name="cart"><see cref="CartVM"/>.</param>
        public CartSummaryVC(CartVM cart)
        {
            _cart = cart;
        }

        /// <summary>
        /// Invokes returning of the <see cref="CartVM"/> to View.
        /// </summary>
        /// <returns>Returns <see cref="CartVM"/> to View.</returns>
        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}