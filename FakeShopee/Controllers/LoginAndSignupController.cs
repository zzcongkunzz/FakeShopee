using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FakeShopee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FakeShopee.Controllers;

public class LoginAndSignupController : Controller
{
    private readonly IConfiguration _configuration;

    private MyDbContext  MyDbContext{ get;}

    public LoginAndSignupController(MyDbContext context, IConfiguration configuration)
    {
        MyDbContext = context;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login (String username, String password)
    {
        Users users = MyDbContext.Users
            .FirstOrDefault(u => (u.Email.Equals(username) || u.PhoneNumber.Equals(username)) && u.Password.Equals(password));
        return Ok(users);

    }

    [HttpGet]
    public async Task<IActionResult> Register(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] Users user){
        
        return Ok(user);
    }
    
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}