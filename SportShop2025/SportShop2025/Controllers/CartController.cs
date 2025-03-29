using Microsoft.AspNetCore.Mvc;
using SportShop2025.ViewModel;
using Newtonsoft.Json;
using SportShop2025.Helpers;
using System.Drawing;
using SportShop2025.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly IHttpContextAccessor httpContextAccessor;

    private readonly SportShop2025Context db;

    public CartController(IHttpContextAccessor _httpContextAccessor, SportShop2025Context _db)
    {
        httpContextAccessor = _httpContextAccessor;

        db = _db;
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

    public IActionResult Checkout()
    {
        var cart = GetCart();
        if (cart.Items.Count == 0)
        {
            TempData["ErrorMessage"] = "Giỏ hàng trống, không thể thanh toán!";
            return RedirectToAction("Index", "Cart");
        }

        var checkoutViewModel = new CheckoutViewModel
        {
            Cart = cart
        };

        return View(checkoutViewModel);
    }



    [HttpPost]
    public IActionResult Checkout(CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var cart = GetCart();
        if (cart.Items.Count == 0)
        {
            TempData["ErrorMessage"] = "Giỏ hàng trống, không thể thanh toán!";
            return RedirectToAction("Index", "Cart");
        }

        var existingCustomer = db.Customers.FirstOrDefault(c => c.Email == model.Email);
        if (existingCustomer == null)
        {
            existingCustomer = new Customer
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                RegistrationDate = DateTime.Now
            };
            db.Customers.Add(existingCustomer);
            db.SaveChanges();
        }

        var order = new Order
        {
            CustomerId = existingCustomer.CustomerId,
            OrderDate = DateTime.Now,
            TotalAmount = cart.Total,
            Status = "Pending"
        };

        db.Orders.Add(order);
        db.SaveChanges();

        foreach (var item in cart.Items)
        {
            var orderDetails = new OrderDetail
            {
                OrderId = order.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                SubTotal = item.TotalPrice
            };
            db.OrderDetails.Add(orderDetails);
        }

        db.SaveChanges();

        Session.Remove("Cart");

        return RedirectToAction("OrderSuccess", new { orderId = order.OrderId });
    }


    public IActionResult OrderSuccess(int orderId)
    {
        var order = db.Orders.Find(orderId);
        return View(order);
    }



}
