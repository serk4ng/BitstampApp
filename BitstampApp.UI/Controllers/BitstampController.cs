using BitstampApp.Api.Helpers;
using BitstampApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BitstampApp.UI.Controllers
{
    public class BitstampController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HacimHesapla(BitstampData bsd)
        {
            try
            {
               
                var userSession = HttpContext.Session.GetString("user");
                User mainuser;

                mainuser = JsonConvert.DeserializeObject<User>(userSession);
                double hacim = 0;

                var myContent = JsonConvert.SerializeObject(bsd);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization =
new AuthenticationHeaderValue("Bearer", mainuser.AccessToken);
                    using (var response = await httpClient.PostAsync("http://localhost:63691/api/bitstamp/hacim", stringContent))
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            hacim = double.Parse(apiResponse);
                            TempData["Hacim"] = "Hacim miktarı: " + hacim;
                            return View("Index");
                        }
                        else
                        {
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
    }
}
