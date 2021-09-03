using ASP.NET_MVC_Exercise.Models;
using System;
using System.Web.Mvc;
using System.Configuration;
using System.Security;
using Microsoft.SharePoint.Client;

namespace ASP.NET_MVC_Exercise.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        private static readonly ClientContext context = new ClientContext(ConfigurationManager.AppSettings["SPOSite"]);

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
                Name = "Shuvodip Ray",
                Address = "Kolkata",
                Qualification = "B.Tech",
                Age = 23
            };
            return View();
        }

        /// <summary>
        ///         Action method to return the view containing the form where values are to be entered.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Customer()
        {
            return View();
        }

        /// <summary>
        ///         Action method to return the view to display the submitted values.
        /// </summary>
        /// <param name="customerData">
        ///         Instance of class CustomerData containing the customer properties.
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Customer(CustomerData customerData)
        {
            return View("CustomerDetails", customerData);
        }

        /// <summary>
        ///         Action method to prform the retrieval operation on a SharePoint List 
        ///         and return the view containing the SharePoint Online List view 
        ///         and the form to create new items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Student()
        {
            List list = InitiateAuthentication(context);
            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            ListItemCollection items = list.GetItems(query);
            ViewBag.listItemCollection = items;
            context.Load(items);
            context.ExecuteQuery();
            return View();
        }

        /// <summary>
        ///         Action method to perfom create operation on a SharePoint Online List 
        ///         and return the view containing success message.
        /// </summary>
        /// <param name="studentData"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Student(Student studentData)
        {
            List list = InitiateAuthentication(context);
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem newItem = list.AddItem(itemCreateInfo);
            newItem["Title"] = studentData.Name;
            newItem["Address"] = studentData.Address;
            newItem["Qualification"] = studentData.Qualification;
            newItem["Age"] = studentData.Age;
            newItem["Gender"] = studentData.Gender;
            newItem["CustomID"] = generateUniqueID();
            newItem.Update();
            context.ExecuteQuery();
            return View("StudentDetails", studentData);
        }

        /// <summary>
        ///         Feteches value of SharePoint Online site username.
        ///         Returns a string.
        /// </summary>
        /// <returns>
        ///         Returns SharePoint Online site username as string.
        /// </returns>
        private static string GetSPOUserName()
        {
            try
            {
                return ConfigurationManager.AppSettings["SPOAccount"];
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///         Feteches value of SharePoint Online site password from web config.
        ///         Returns a SecureString object.
        /// </summary>
        /// <returns>
        ///         System.Security.SecureString class instance.
        /// </returns>
        private static SecureString GetSPOSecureStringPassword()
        {
            try
            {
                SecureString secureString = new SecureString();
                foreach (char c in ConfigurationManager.AppSettings["SPOPassword"])
                {
                    secureString.AppendChar(c);
                }
                return secureString;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///         Takes SharePoint Online Site ClientContext as parameter.
        ///         Fetches SharePoint Online credentials from app config and initiates authentication.
        ///         Returns an object of Microsoft.Client.List.
        /// </summary>
        /// <param name="ctx">
        ///         SharePoint Online Site ClientContext.
        /// </param>
        /// <returns>
        ///         Microsoft.SharePoint.Client.List class instance.
        /// </returns>
        private static List InitiateAuthentication(ClientContext ctx)
        {
            ClientContext context = ctx;
            context.AuthenticationMode = ClientAuthenticationMode.Default;
            context.Credentials = new SharePointOnlineCredentials(GetSPOUserName(), GetSPOSecureStringPassword());
            List list = context.Web.Lists.GetByTitle(ConfigurationManager.AppSettings["SPOList"]);
            context.Load(list);
            context.ExecuteQuery();
            return list;
        }

        /// <summary>
        ///         Method to generate a unique ID for the records.
        /// </summary>
        /// <returns></returns>
        private static string generateUniqueID()
        {
            string[] uniqueRandomKey = Guid.NewGuid().ToString().Split('-');
            return uniqueRandomKey[0];
        }
    }
}