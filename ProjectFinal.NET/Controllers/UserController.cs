using Microsoft.AspNetCore.Mvc;
using ProjectFinal.NET.Data;
using ProjectFinal.NET.Helper;
using ProjectFinal.NET.Models;


namespace ProjectFinal.NET.Controllers
{
    public class UserController : Controller
    {
        private readonly DotnetContext db;
        public UserController(DotnetContext context)
        {
            db = context;
        }
        public IActionResult Login()
        { 
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string randomKey = Util.GenerateRamdomkey();
                    // Kết hợp mật khẩu với chuỗi ngẫu nhiên
                    string combinedPassword = model.Password + randomKey;

                    // Hash mật khẩu kết hợp bằng MD5
                    string hashedPassword = combinedPassword.ToMD5Hash();

                    var user = new User
                    {
                        Email = model.Email,

                        Password = hashedPassword,
                        CreatedAt = DateTime.Now,
                        IsAdmin = true,
                        IsActive = true,

                    };
                    
                    
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }

            }
            return View(model);
        }
    }
}
