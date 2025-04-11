using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using TestREA.Models;
using System.Text;
using TestREA.Utilities;

namespace TestREA.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to initialize _context
        public AccountController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.ReaUtilisateurs
                .FirstOrDefaultAsync(u => u.EmailUtilisateur == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("","Nom d'utilisateur ou mot de passe incorrect.");
                return View(model);
            }

            string decodedPassword = ByteArrayDecoder.DecodeByteArrayToString(user.MotPasse);

            Console.WriteLine($"User Email: {user.EmailUtilisateur}"); // Debugging line
            Console.WriteLine($"Model Email: {model.Email}"); // Debugging line
            Console.WriteLine($"Decoded Password: {decodedPassword}"); // Debugging line
            Console.WriteLine($"Model Password: {model.Password}"); // Debugging line

            if (decodedPassword != model.Password)
            {
                ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe incorrect.");
                return View(model);
            }

            if (user.EmailUtilisateur == model.Email && decodedPassword == model.Password)
            {
                Console.WriteLine("youhouuuuuuu"); // Debugging line
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NomUtilisateur ?? string.Empty),
                new Claim(ClaimTypes.Email, user.EmailUtilisateur ?? string.Empty)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
