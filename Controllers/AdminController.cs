using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireApp.Models;

namespace QuestionnaireApp.Controllers
{
    [Authorize(Policy = "OnlyAdminAccess")]
    public class AdminController : Controller
    {
        // Instance of the SurveyManipulator class
        private readonly SurveyManipulator _surveyManipulator;

        // Constructor to inject an instance of SurveyManipulator into the controller
        public AdminController(SurveyManipulator surveyManipulator)
        {
            _surveyManipulator = surveyManipulator;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult NewSurvey()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewSurvey(Survey model)
        {
            var survey = new Survey()
            {
                Title = model.Title,
                Description = model.Description,
                Questions = model.Questions
            };
            // Here you can save the survey data to the database or perform other operations
            // For now, let's just return a view to display the submitted data
            Console.WriteLine("sheesh");
            _surveyManipulator.CreateSurvey(survey);
            return View(model);
        }

        public IActionResult AnsweredSurveys()
        {
            if (User.Identity.IsAuthenticated)
            {
                var answeredSurveys = _surveyManipulator.GetAnsweredSurveys();
                return View(answeredSurveys);
            }
            return RedirectToAction("Index", "Login"); // Redirect to login page if not authenticated
        }
    }
}
