using Microsoft.AspNetCore.Authentication;
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
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("ERROR", "ko cóa tk này");
                }
                else
                {
                    if (!(user.IsActive ?? false))
                    {
                        ModelState.AddModelError("ERRO", "tk bị ban");
                    }
                    else
                    {
                        if (user.Password != model.Password.ToMD5Hash())
                        {
                            ModelState.AddModelError("ERROR", "sai tk or mk");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, model.Email),

                                // clian đọng phan quyen 
                                new Claim(ClaimTypes.Role,"User")
                            };
                            var claimIdentity = new ClaimsIdentity(claims, "login");
                            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                            await HttpContext.SignInAsync(claimPrincipal);
                            // Thêm thông báo thành công vào TempData
                            TempData["SuccessMessage"] = "Đăng nhập thành công!";
                            TempData.Keep("SuccessMessage");

                            if (Url.IsLocalUrl(ReturnUrl))
                                    {
                                        return  Redirect(ReturnUrl);
                                    }
                                        else
                                        {
                                          return Redirect("/");
                                        }


                        }

                       
                    }
                } 
               
                
            }
            return RedirectToAction("Index");
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
    }
}
