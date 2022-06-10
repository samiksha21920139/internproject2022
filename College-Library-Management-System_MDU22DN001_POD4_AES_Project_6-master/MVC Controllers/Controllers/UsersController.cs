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
    public class UsersController : Controller
    {
        // GET: Users
        string url = "https://localhost:44315/api/";
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
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser([Bind(Include = "UserName,UserId,UserRole,Department,Year")] UserTable student)
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
                    HttpResponseMessage res = await client.PostAsync("UserTables", c);
                    if (res.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "User added successfully";
                        return View(student);

                    }
                }
            }
            return View();
        }
        public async Task<ActionResult> Delete(int id)
        {
            UserTable students = new UserTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("UserTables/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var studentResponse = res.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<UserTable>(studentResponse);

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
                HttpResponseMessage res = await client.DeleteAsync("UserTables/" + id);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexUser");

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