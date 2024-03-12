using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FakeShopee.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace FakeShopee.Controllers;

public class LoginAndSignupController : Controller
{
    private readonly IConfiguration _configuration;

    private MyDbContext _context;

    public LoginAndSignupController(MyDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public static Users getUserByToken (String jwt, List<Users> users){
        var handler = new JwtSecurityTokenHandler();
        try{
            var token = handler.ReadJwtToken(jwt);
            var claims = token.Claims.ToArray();
            var userId = claims.FirstOrDefault(claim=>claim.Type == "nameid").Value;
            Users user = null;
            user = users.FirstOrDefault(user1=>user1.Id.ToString() == userId);
            if(user == null) throw new Exception();
            return user;
        }catch{
            throw new Exception();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        var jwt = Request.Cookies["token"];
        var users = await _context.Users.ToListAsync();
        if (jwt != null)
        {
            try{
                Users user = null;
                user = getUserByToken(jwt, users);
                if(user!=null) return RedirectToAction("Index", "Home");
            }catch{
                return View();
            }
        }
        return View();
        
    }

    [HttpPost]
    public async Task<IActionResult> Login (String username, String password){
        
        Console.WriteLine("uuuu{0} {1}", username, Request.Cookies["token"]);
        var users = await _context.Users.ToListAsync();
        Users user = null;
        users.ForEach(user1=>{
            if((username == user1.Email || username == user1.PhoneNumber)&&BCrypt.Net.BCrypt.Verify(password, user1.Password)){
                user = user1;
            }
        });
        if(user == null){
            ViewData["Error"] = "Tên tài khoản của bạn hoặc Mật khẩu không đúng, vui lòng thử lại";
            // return View(username, password);
            return BadRequest("Tên tài khoản của bạn hoặc Mật khẩu không đúng, vui lòng thử lại");
        }

        var jwtIssuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = _configuration.GetSection("Jwt:Key").Get<string>();
        var key = Encoding.ASCII.GetBytes(jwtKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName.ToString()),
            new Claim(ClaimTypes.Role, user.UserGroupId.ToString()),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(3),
            Issuer = jwtIssuer,
            Audience = jwtIssuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(tokenString);

    }

    [HttpGet]
    public async Task<IActionResult> Register(){
        var jwt = Request.Cookies["token"];
        var users = await _context.Users.ToListAsync();
        if (jwt != null)
        { 
            try{
                Users user = null;
                user = getUserByToken(jwt, users);
                if(user!=null) return RedirectToAction("Index", "Home");
            }catch{
                return View();
            }
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Users user){
        var users = await _context.Users.ToListAsync();
        if (ModelState["FullName"].Errors.Count > 0 || ModelState["Email"].Errors.Count > 0 || ModelState["PhoneNumber"].Errors.Count > 0 || ModelState["Password"].Errors.Count > 0){
            return View(user);
        }
        if (users.Any(i => i.PhoneNumber == user.PhoneNumber)){
            ModelState.AddModelError("PhoneNumber", "Số điện thoại đã được sử dụng");
            return View(user);
        }
        if (users.Any(i => i.Email == user.Email)){
            ModelState.AddModelError("Email", "Email đã được sử dụng");
            return View(user);
        }
        var usersGroup = await _context.UserGroups.ToListAsync();
        UserGroup groupId = null;
        groupId = usersGroup.FirstOrDefault(group => group.Name == "user");
        if(groupId == null){
            groupId = new UserGroup{
                Id = Guid.NewGuid(),
                Name = "user"
            };
            _context.UserGroups.Add(groupId);
            await _context.SaveChangesAsync();
        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        user.UserGroupId = groupId.Id;
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public async Task<IActionResult> User () {
        var jwt = Request.Cookies["token"];
        var users = await _context.Users.ToListAsync();
        if (jwt != null)
        { 
            try{
                Users user = null;
                user = getUserByToken(jwt, users);
                if(user!=null) return View(user);
            }catch{
                return RedirectToAction("Login");
            }
        }
        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> User (Users user){
        var users = await _context.Users.ToListAsync();
        var jwt = Request.Cookies["token"];
        Users user1 = null;
        if (jwt != null)
        { 
            try{
                user1 = getUserByToken(jwt, users);
            }catch{
                return RedirectToAction("Login");
            }
        }
        ViewBag.Type = 0;
        if (ModelState["FullName"].Errors.Count > 0 || ModelState["Email"].Errors.Count > 0 || ModelState["PhoneNumber"].Errors.Count > 0 ){
            return View(user);
        }
        if (user1.PhoneNumber!=user.PhoneNumber && users.Any(i => i.PhoneNumber == user.PhoneNumber)){
            ModelState.AddModelError("PhoneNumber", "Số điện thoại đã được sử dụng");
            return View(user);
        }
        if (user1.Email!=user.Email && users.Any(i => i.Email == user.Email)){
            ModelState.AddModelError("Email", "Email đã được sử dụng");
            return View(user);
        }
        user1.FullName = user.FullName;
        user1.Email = user.Email;
        user1.PhoneNumber = user.PhoneNumber;
        user1.Gender = user.Gender;
        user1.Address = user.Address;
        await _context.SaveChangesAsync();
        ViewBag.Type = 1;
        return View(user1);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}