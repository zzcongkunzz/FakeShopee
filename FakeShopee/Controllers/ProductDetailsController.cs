using FakeShopee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FakeShopee.Controllers;

public class ProductDetailsController : Controller
{
    private MyDbContext MyDbContext{ get;}
    private readonly ILogger<ProductDetailsController> _logger;

    public ProductDetailsController(MyDbContext context ,ILogger<ProductDetailsController> logger)
    {
        _logger = logger;
        MyDbContext = context;
    }
    
    public IActionResult Index(Guid id)
    {
        Product product = MyDbContext.Products.Find(id);
        return View(product);
    }

    [HttpGet]
    public IActionResult AddCart(Guid productId, int quantity)
    {
        Product product = MyDbContext.Products.Find(productId);
        
        var serializedUser = HttpContext.Session.GetString("User");
        if (serializedUser != null)
        {
            Users tmpUser = JsonConvert.DeserializeObject<Users>(serializedUser);

            Users user = MyDbContext.Users.Find(tmpUser.Id);
            
            Cart cart = MyDbContext.Carts
                .Include(cart => cart.Customer)
                .Include(cart => cart.Product)
                .FirstOrDefault(cart => cart.Customer.Id == user.Id && cart.Product.Id == productId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    Customer = user,
                    Product = product,
                    Quantity = quantity
                };
                MyDbContext.Carts.Add(cart);
            }
            else
            {
                cart.Quantity += quantity;
            }

            MyDbContext.SaveChanges();
            
            return Ok(cart);
        }
        else
        {
            return Ok(null);
        }
    }
    
}