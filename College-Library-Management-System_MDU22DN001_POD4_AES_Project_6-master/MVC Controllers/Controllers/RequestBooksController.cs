using College_library_management_system.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace College_library_management_system.Controllers
{
    public class RequestBooksController : Controller
    {
        // GET: RequestBooks
        // LibraryContext db = new LibraryContext();
        string url = "https://localhost:44315/api/";
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

        // GET: RequestBooks/Details/5
       
        // GET: RequestBooks/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookTitle,BookId,UserID,BorrowedDate")] RequestBook requestBook)
        {
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string coContent = JsonConvert.SerializeObject(requestBook);
                    HttpContent c = new StringContent(coContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync("RequestBooks", c);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "StudentDashboard");

                    }
                }
            }
            return View(requestBook);
        }
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
                        return RedirectToAction("IndexRequest","RequestBooks");

                    }
                }
            }
            return View(student);
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
                HttpResponseMessage res = await client.DeleteAsync("RequestBooks/"+id);
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