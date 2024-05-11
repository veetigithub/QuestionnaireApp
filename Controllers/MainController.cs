using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using QuestionnaireApp.Models;

namespace QuestionnaireApp.Controllers
{
    public class MainController : Controller
    {
        // Instance of the SurveyManipulator class
        private readonly SurveyManipulator _surveyManipulator;

        // Constructor to inject an instance of SurveyManipulator into the controller
        public MainController(SurveyManipulator surveyManipulator)
        {
            _surveyManipulator = surveyManipulator;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var allSurveys = _surveyManipulator.GetAllSurveys<Survey>("Survey");
                Console.WriteLine(allSurveys);
                return View(allSurveys);
            }
            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Survey(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(id) && ObjectId.TryParse(id, out ObjectId objectId))
                {
                    var surveyById = _surveyManipulator.GetByObjectId(objectId);
                    if (surveyById != null)
                    {
                        return View(surveyById);
                    }
                }
                // If the id is not valid or the survey is not found, handle accordingly
                return RedirectToAction("Index", "Main"); // Redirect to a default action
            }
            return RedirectToAction("Index", "Login"); // Redirect to login page if not authenticated
        }
    }
}
