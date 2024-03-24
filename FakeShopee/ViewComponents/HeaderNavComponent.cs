using Microsoft.AspNetCore.Mvc;
using FakeShopee.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FakeShopee.ViewComponents{
    [ViewComponent(Name = "HeaderNav")]
    public class HeaderNavComponent : ViewComponent{
        private MyDbContext _context;

        public HeaderNavComponent(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync (){
            var jwt = Request.Cookies["token"];
            if (jwt == null)
            {
                ViewData["IsLoggedIn"] = false;
                return View();
            }
            var handler = new JwtSecurityTokenHandler();
            try{
                var token = handler.ReadJwtToken(jwt);
                var claims = token.Claims.ToArray();
                var userId = claims.FirstOrDefault(claim=>claim.Type == "nameid").Value;
                var users = await _context.Users.ToListAsync();
                Users user = users.FirstOrDefault(user1=>user1.Id.ToString() == userId);
                ViewData["IsLoggedIn"] = true;
                ViewData["User"] = user;
                return View();
            }catch{
                ViewData["IsLoggedIn"] = false;
                return View();
            }
        }

    }
}
