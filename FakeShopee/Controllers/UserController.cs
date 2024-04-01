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

    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("User");
        return Ok();
    }
}