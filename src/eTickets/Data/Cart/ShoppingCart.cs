using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart;

public class ShoppingCart
{
    public AppDbContext _context { get; set; }
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ShoppingCart(AppDbContext context)
    {
        _context = context;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??
                (ShoppingCartItems = _context.ShoppingCartItems
                                    .Where(s => s.ShoppingCartId == ShoppingCartId)
                                    .Include(s => s.Movie).ToList());
    }

    public double GetShoppingCartTotal()
        => _context.ShoppingCartItems.
                    Where(s => s.ShoppingCartId == ShoppingCartId)
                    .Select(s => s.Movie.Price * s.Amount)
                    .Sum();
}