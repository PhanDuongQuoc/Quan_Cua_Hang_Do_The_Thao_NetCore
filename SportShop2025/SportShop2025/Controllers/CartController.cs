using Microsoft.AspNetCore.Mvc;
using SportShop2025.ViewModel;
using Newtonsoft.Json;
using SportShop2025.Helpers;
using SportShop2025.ViewModel;
using System.Drawing;

public class CartController : Controller
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CartController(IHttpContextAccessor _httpContextAccessor)
    {
        httpContextAccessor = _httpContextAccessor;
    }

    private ISession Session => httpContextAccessor.HttpContext.Session;

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    public IActionResult AddToCart(int productId, string productName,string image, decimal price)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            item.Quantity++;
        }
        else
        {
            cart.Items.Add(new CartItemViewModel
            {

                ProductId = productId,
                ProductName = productName,
                Image = image,
                Price = price,
                Quantity = 1
            });
        }

        SaveCart(cart);
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int productId)
    {
        var cart = GetCart();
        cart.Items.RemoveAll(x => x.ProductId == productId);
        SaveCart(cart);
        return RedirectToAction("Index");
    }

    public IActionResult ClearCart()
    {
        Session.Remove("Cart");
        return RedirectToAction("Index");
    }

    private CartViewModel GetCart()
    {
        return Session.GetObject<CartViewModel>("Cart") ?? new CartViewModel();
    }

    private void SaveCart(CartViewModel cart)
    {
        Session.SetObject("Cart", cart);
    }
}
