using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Claims;
using QuestionnaireApp.Models;

namespace QuestionnaireApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseConnection _connection;

        public LoginController(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User model)
        {
            // Check if the username + password combination exists in the database
            if (IsValidUser(model.Username, model.Password))
            {
                // Call the Login method to sign in the user
                Login(model.Username);
                Console.WriteLine("kirjautui sisään");
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("ei kirjautunut");
            }
            // Invalid username or password, return to login page with error
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        private bool IsValidUser(string username, string password)
        {

            // Query the database for a document with the provided username
            var usersCollection = _connection.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var user = usersCollection.Find(filter).FirstOrDefault();

            // If no user found with the provided username, return false
            if (user == null)
            {
                Console.WriteLine(username);
                Console.WriteLine("nulli user");
                return false;
            }

            // Check if the provided password matches the password of the found document
            if (user.Password == password)
            {
                return true;
            }

            return false;
        }

        private void Login(string username)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username)
            };
            //if (name == "admin")
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "admin"));
            //}
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = true
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
