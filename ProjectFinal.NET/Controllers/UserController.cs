using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal.NET.Data;
using ProjectFinal.NET.Helper;
using ProjectFinal.NET.Models;
using System.Security.Claims;


namespace ProjectFinal.NET.Controllers
{
    public class UserController : Controller
    {
        private readonly DotnetContext db;
        public UserController(DotnetContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        { 
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
               
                var user = db.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user != null)
                {

                    if (!(user.IsActive??false))
                    {
                        // Người dùng không được kích hoạt, handle accordingly
                        ModelState.AddModelError("", "Tài khoản của bạn chưa được kích hoạt. Liên hệ với quản trị viên để biết thêm thông tin.");
                        return View();
                    }

                    // Hash the combined password using the same method used during registration
                    string hashedPassword = model.Password.ToMD5Hash();

                    // Compare the hashed password with the stored hashed password
                    if (hashedPassword == user.Password)
                    {
                        bool isAdmin = user.IsAdmin;
                        // Passwords match, user is authenticated
                        var claims = new List<Claim>
                {

                     new Claim(ClaimTypes.Name, user.Email),

                    new Claim(ClaimTypes.Name, user.Email),

                     new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User"),
                    // Thêm các claim khác nếu cần, ví dụ: user ID, vv.
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            // Tùy chỉnh các thuộc tính nếu cần
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                            );
                        return Redirect(ReturnUrl ?? Url.Action("Index", "Home"));
                    }
                    else
                    {
                        // Passwords do not match
                        // Handle accordingly, perhaps show an error message
                        ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                    }
                }

                // Invalid credentials, add a model error
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
            }
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
                    // Hash mật khẩu kết hợp bằng MD5
                    string hashedPassword = model.Password.ToMD5Hash();

                    var user = new User
                    {
                        Email = model.Email,

                        Password = hashedPassword,
                        CreatedAt = DateTime.Now,
                        IsAdmin = false,
                        IsActive = true,

                    };
                    
                    
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }

            }
            return RedirectToAction("Login");
        }


        

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to home page 
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
