using BitstampApp.Core.Models;
using BitstampApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        //protected IUserService _userService => HttpContext.RequestServices.GetService<IUserService>();
        //private readonly IUserService _userService;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public LoginController(IUserService userService, IUnitOfWork unitOfWork, IHttpContextAccessor  httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _unitOfWork = unitOfWork;
        //    _userService = userService;

        //}

        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }
        public IActionResult Index()
        {
            var user = _userService.GetAll();
            return View(user);
        }
        public IActionResult Register()
        {
            return View();
        }

      

        [HttpPost]
        public async Task<IActionResult> LoginAsync(User user)
        {
            try
            {

                User u = new User();
                var myContent = JsonConvert.SerializeObject(user);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json"); 

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("http://localhost:63691/api/login/authorize", stringContent))
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            u = JsonConvert.DeserializeObject<User>(apiResponse);

                            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));
                            return RedirectToAction("Index","Bitstamp");
                        }
                        else
                        {
                            TempData["Message"] = "Username or password incorrect !";
                            return View("Index");

                        }
                    }

                }
            }
            catch (System.Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(User user)
        {
   

            User u = new User();
            var myContent = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json"); 

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("http://localhost:63691/api/login/register", stringContent))
                {
                    
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        u = JsonConvert.DeserializeObject<User>(apiResponse);
                        return View("Index");
                    }
                    else
                    {
                        return View("Register");

                    }
                }
 
            }
          
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Login");
        }
    }
}
