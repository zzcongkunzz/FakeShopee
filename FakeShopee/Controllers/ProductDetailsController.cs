using FakeShopee.Models;
using Microsoft.AspNetCore.Mvc;

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
}