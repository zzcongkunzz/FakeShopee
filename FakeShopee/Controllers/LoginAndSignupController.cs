using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FakeShopee.constant;
using FakeShopee.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    
    [HttpGet]
    [Route("/LoginAndSignup/Login/{id:guid}")]
    public async Task<IActionResult> Login(Guid id)
    {
        Users users = MyDbContext.Users.Find(id);
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Login (String username, String password)
    {
        Users user = MyDbContext.Users
            .FirstOrDefault(u => (u.Email.Equals(username) || u.PhoneNumber.Equals(username)) && u.Password.Equals(password));
        
        HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
        
        return Ok(user);

    }

    [HttpGet]
    public async Task<IActionResult> Register(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] Users user){
        Users userExist = MyDbContext.Users
            .FirstOrDefault(u => u.Email.Equals(user.Email) || u.PhoneNumber.Equals(user.PhoneNumber));
        
        if (userExist != null)
        {
            return Ok(Empty);
        }
        else
        {
            List<UserGroup> userGroup = MyDbContext.UserGroups.ToList();
            user.UserGroup = userGroup.Find(item => item.Name.Equals(RoleType.CUSTOMER.GetDescription()));
            MyDbContext.Users.Add(user);
            MyDbContext.SaveChanges();
            return Ok(user);
        }
    }
    
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}