using FakeShopee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FakeShopee.Controllers;

public class UserController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private MyDbContext  MyDbContext{ get;}

    public UserController(MyDbContext context ,ILogger<HomeController> logger)
    {
        MyDbContext = context;
    }

    [HttpGet]
    public IActionResult GetUserLogin()
    {
        var serializedUser = HttpContext.Session.GetString("User");
        if (serializedUser != null)
        {
            Users user = JsonConvert.DeserializeObject<Users>(serializedUser);
            return Ok(user);
        }
        else
        {
            return Ok(null);
        }
    }

    [HttpGet]
    public IActionResult GetCartUser()
    {
        var serializedUser = HttpContext.Session.GetString("User");
        if (serializedUser != null)
        {
            Users user = JsonConvert.DeserializeObject<Users>(serializedUser);
            List<Cart> carts = MyDbContext.Carts
                .Include(cart => cart.Customer)
                .Include(cart => cart.Product)
                .Where(cart => cart.Customer.Id == user.Id)
                .ToList();
            
            return Ok(carts);
        }
        else
        {
            return Ok(null);
        }
        
    }

    [HttpGet]
    public IActionResult ViewCart()
    {
        var serializedUser = HttpContext.Session.GetString("User");
        if (serializedUser != null)
        {
            Users user = JsonConvert.DeserializeObject<Users>(serializedUser);
            List<Cart> carts = MyDbContext.Carts
                .Include(cart => cart.Customer)
                .Include(cart => cart.Product)
                .Where(cart => cart.Customer.Id == user.Id)
                .ToList();
            
            return View("Cart", carts);
        }
        else
        {
            return RedirectToAction("Login", "LoginAndSignup");
        }
        return View("Cart");
    }

    [HttpGet]
    public IActionResult DeleteCart(Guid cartID)
    {
        Cart cart = MyDbContext.Carts.Find(cartID);
        MyDbContext.Carts.Remove(cart);
        MyDbContext.SaveChanges();
        return Ok();
    }
    
    [HttpPost]
    public IActionResult ChangeCart(Guid cartID, int quantity)
    {
        Cart cart = MyDbContext.Carts
            .Include(cart1 => cart1.Product)
            .FirstOrDefault(cart1 => cart1.Id == cartID);
        if (quantity > cart.Product.WarehouseQuantity)
        {
            cart.Quantity = cart.Product.WarehouseQuantity;
        }
        else
        {
            cart.Quantity = quantity;
        }
        MyDbContext.SaveChanges();
        return Ok(cart);
    }

    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("User");
        return Ok();
    }
}