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
    public class StudentDashboardController : Controller
    {
        // GET: StudentDashboard
        readonly string url = "https://localhost:44315/api/";


        // View the available books in the library
        public async Task<ActionResult> Index()
        {
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

        public async Task<ActionResult> IndexRequest()
        {
            
            int id = Convert.ToInt32(Session["UserId"]);
            IList<RequestBook> requests = new List<RequestBook>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Students/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    requests = JsonConvert.DeserializeObject<List<RequestBook>>(studentResponse);

                }
            }
            return View(requests);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            RequestBook students = new RequestBook();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("RequestBooks/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<RequestBook>(studentResponse);

                }
            }
            return View(students);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.DeleteAsync("RequestBooks/" + id);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexRequest");

                }
            }
            return RedirectToAction("Delete");
        }
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

    }
}