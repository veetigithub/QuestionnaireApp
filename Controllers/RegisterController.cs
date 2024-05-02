using Microsoft.AspNetCore.Mvc;
using QuestionnaireApp.Models;

namespace QuestionnaireApp.Controllers
{
    public class RegisterController : Controller
    {
        // Instance of the Register class to handle user registration
        private readonly Register _register;

        // Constructor to inject an instance of Register into the controller
        public RegisterController(Register register)
        {
            _register = register;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };
                Console.WriteLine("sheesh");
                _register.RegisterUser(user);
                return View(model);
            }
            Console.WriteLine("sheeshssssssssssdsds");
            // If model state is not valid, return to the registration form with validation errors
            return View(model);
        }
    }
}
