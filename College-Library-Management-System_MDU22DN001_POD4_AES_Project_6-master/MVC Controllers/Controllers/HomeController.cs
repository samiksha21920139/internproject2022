using College_library_management_system.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace College_library_management_system.Controllers
{
    public class HomeController : Controller
    {
        
        readonly string url = "https://localhost:44315/api/";


        // View the available books in the library
        public async Task<ActionResult> Index()
        {
            // TempData["SuccessMessage"] = "Book added successfully";
            IList<AvailableBook> books = new List<AvailableBook>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("AvailableBooks");
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<AvailableBook>>(studentResponse);

                }
            }
            return View(books);
        }
    }
}
