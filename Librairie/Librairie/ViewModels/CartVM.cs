namespace Librairie.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class CartVM
    {
        public virtual List<CartItemVM> ShopItems { get; } = new List<CartItemVM>();

        public virtual void AddItem(BookVM book, int quantity)
        {
            var cartItem = ShopItems.Where(p => p.Book.Id == book.Id).FirstOrDefault();
            if (cartItem == null)
            {
                ShopItems.Add(new CartItemVM() { Book = book, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(int bookId)
        {
            ShopItems.RemoveAll(p => p.Book.Id == bookId);
        }

        public virtual decimal ComputeTotalValue()
        {
            return ShopItems.Sum(e => e.Book.Price * e.Quantity);
        }

        public virtual void Clear()
        {
            ShopItems.Clear();
        }
    }
}