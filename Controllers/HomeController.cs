using ASP.NET_MVC_Exercise.Models;
using System.Web.Mvc;

namespace ASP.NET_MVC_Exercise.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        /// <summary>
        ///         An action method to return a string instead of an action result to the View. 
        /// </summary>
        /// <returns></returns>

        //public string Index()
        //{
        //    return "Hello from Index!";
        //}

        /// <summary>
        ///         A method same as the index action. 
        ///         Since, the action name is different from the one mentioned in the default rule, 
        ///         this action won't be binded on pageload. 
        ///         The action name needs to written explicitly.
        /// </summary>
        /// <returns></returns>
        public string IndexVariant()
        {
            return "Hello from Index variant!";
        }

        /// <summary>
        ///         Default Action to be called on pageload. 
        ///         The name Index if mentioned as the default action name in the RouteConfig rule. 
        ///         ViewBag Property is used to send dynamic Student class properties to the view.
        /// </summary>
        /// <returns>
        ///         An object of System.Web.Mvc.ActionResult. 
        ///         Represents the result of an action method.
        /// </returns>
        public ActionResult Index()
        {
            ViewBag.Gender = new string[] { "Male", "Female", " Transgender", "Other" };
            ViewBag.Student = new Student
            {
                ID = 5452,
                Name = "Shuvodip Ray",
                Address = "Kolkata",
                Qualification = "B.Tech",
                Age = 23
            };
            return View();
        }
    }
}