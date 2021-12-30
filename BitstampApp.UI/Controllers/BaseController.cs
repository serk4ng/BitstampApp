using BitstampApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BitstampApp.UI.Controllers
{
    public class BaseController : Controller
    {
        public User _users;

        public User CurrentSession
        {
            get
            {
                var value = HttpContext.Session.GetString("user");
                return value == null ? default(User)
                                     : JsonConvert.DeserializeObject<User>(value);
            }
            set
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(value));
            }
        }

        public void ClearSession()
        {
            CurrentSession = null;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var value = HttpContext.Session.GetString("user");
            if (value == null)
            {
                filterContext.Result = RedirectToLoginPage();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        protected IActionResult RedirectToLoginPage(string redirectAction = "Index")
        {
            return RedirectToAction("Index", "Login");
        }

    }
}
