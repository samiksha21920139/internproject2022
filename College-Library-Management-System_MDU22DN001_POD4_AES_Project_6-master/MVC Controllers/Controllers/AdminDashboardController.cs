using College_library_management_system.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace College_library_management_system.Controllers
{
    
    public class AdminDashboardController : Controller
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
                    books= JsonConvert.DeserializeObject<List<AvailableBook>>(studentResponse);

                }
            }
            return View(books);
        }
        public async Task<ActionResult> IndexRequest()
        {

            IList<RequestBook> requests = new List<RequestBook>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("RequestBooks");
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    requests = JsonConvert.DeserializeObject<List<RequestBook>>(studentResponse);

                }
            }
            return View(requests);
        }
        //Deleting a book
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            AvailableBook books = new AvailableBook();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("AvailableBooks/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<AvailableBook>(studentResponse);

                }
            }
            return View(books);
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
                HttpResponseMessage res = await client.DeleteAsync("AvailableBooks/" + id);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Delete");
        }

        //adding a new book
        public ActionResult CreateBooks()
        {
            
            return View();
        }

        //giving values to add a new book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBooks([Bind(Include = "BookId,Category,BookTitle,Author,Publisher,Count")] AvailableBook availableBook)
        {
            if (ModelState.IsValid)
            {

               
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string coContent = JsonConvert.SerializeObject(availableBook);
                    HttpContent c = new StringContent(coContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync("AvailableBooks", c);
                   
                    if (res.IsSuccessStatusCode)
                    {


                        ViewBag.Message = "Book added successfully";
                        return View(availableBook);
                        


                    }
                   


                }

               
            }
           
            return View(availableBook);
            
        }
        //View Available users
        public async Task<ActionResult> IndexUser()
        {
            IList<UserTable> users = new List<UserTable>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("UserTables");
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<UserTable>>(studentResponse);

                }
            }
            return View(users);
        }
        //creating a new user
        
        public async Task<ActionResult> Edit(int? id)
        {
            RequestBook students = new RequestBook();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("RequestBooks/GetRequestBook?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<RequestBook>(studentResponse);

                }
            }
            return View(students);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RequestId,UserId,BookId,BookTitle,BorrowedDate,ReturnDate")] RequestBook student)
        {
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string coContent = JsonConvert.SerializeObject(student);
                    HttpContent c = new StringContent(coContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PutAsync("RequestBooks/PutRequestBook?id=" + student.RequestId + "&isedit=" + true, c);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("IndexRequest", "RequestBooks");

                    }
                }
            }
            return View(student);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }

}
