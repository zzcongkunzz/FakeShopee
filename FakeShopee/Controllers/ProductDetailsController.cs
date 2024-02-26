using Microsoft.AspNetCore.Mvc;

namespace FakeShopee.Controllers;

public class ProductDetailsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}