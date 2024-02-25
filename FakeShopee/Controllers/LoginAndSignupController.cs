using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FakeShopee.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeShopee.Controllers;

public class LoginAndSignupController : Controller
{
    private MyDbContext _context;

    public LoginAndSignupController(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Login()
    {
        var users = await _context.UserGroups.ToListAsync();
        // User user = 
        Console.WriteLine(users.Count);
        // users.ForEach(user=>{
        //     Console.WriteLine(user.FullName);
        // });
        // return View();
        return View();
    }

    public IActionResult Register(){
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}