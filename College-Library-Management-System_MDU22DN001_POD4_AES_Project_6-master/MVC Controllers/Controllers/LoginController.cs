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
    public class LoginController : Controller
    {
        // GET: LoginI
        //private readonly LibraryContext db = new LibraryContext();
        readonly string url= "https://localhost:44315/api/";
       
        public ActionResult Login()
        {
           return View();
        }
        //Login Authentication*/
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(SignIn login)
        {
            // profile.v_Login = login;
            SignIn dbdata= new SignIn();

            using (var client = new HttpClient())
            {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.GetAsync("SignIns/GetSignIn?username=" + login.UserName+"&password="+login.Password);
                    if (res.IsSuccessStatusCode)
                    {
                        var loginResponse = res.Content.ReadAsStringAsync().Result;
                        dbdata = JsonConvert.DeserializeObject<SignIn>(loginResponse);
                        if (dbdata.UserRole=="Admin")
                        {
                            Session["UserId"] = dbdata.UserId;
                            return RedirectToAction("Index", "AdminDashboard");
                        }
                        else if(dbdata.UserRole=="Student"||dbdata.UserRole=="Teacher")
                        {
                            Session["UserId"] = dbdata.UserId;
                            return RedirectToAction("Index", "StudentDashboard");
                        }    
                        else
                        {
                             ViewBag.Message = "Invalid login";
                             return View();
                        }
                  
                     }

            }
            return View();
        }


        public ActionResult SignUp()
        {

            return View();
        }

        //giving values to add a new book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp([Bind(Include = "UserName,UserId,UserRole,Password")] SignIn signup)
        {
            if (ModelState.IsValid)
            {


                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string coContent = JsonConvert.SerializeObject(signup);
                    HttpContent c = new StringContent(coContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync("SignIns", c);

                    if (res.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "User Sign Up successful";
                        return View();
                    }

                }

            }

            return View(signup);

        }


    }
}