using CollageMng.ViewModel;
using CollageMng.Models;
using Microsoft.AspNetCore.Mvc;
using CollageMng.Data;

namespace CollageMng.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;

        public HomeController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            IEnumerable<Gallery> data = db.Gallery;
            return View(data);
        }

        public IActionResult Loginpage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Loginpage(string email,string pass,string role)
        {
           

            if(role=="Student")
            {
                var user = db.Studata.SingleOrDefault(u => u.Email == email);
                if (user != null)
                {
                    // Compare entered password with the stored password
                    if (pass == user.Password)
                    {
                        // Password is correct, proceed with login logic
                            HttpContext.Session.SetInt32("userid",user.Id);
                            string url = Url.Action("Index", "Student");
                            return Redirect(url);
                        

                    }
                    else
                    {
                        // Password is incorrect
                        TempData["Message"] = "Invalid password.";

                    }
                }
                else
                {
                    // User with the specified email doesn't exist
                    TempData["Message"] = "Student not found.";
                }

                return View();

            }
            else
            {
                var user = db.Facdata.SingleOrDefault(u => u.Email == email);
                if (user != null)
                {
                    // Compare entered password with the stored password
                    if (pass == user.Password)
                    {
                        // Password is correct, proceed with login logic
                        if (user.Role == "Admin")
                        {
                            HttpContext.Session.SetInt32("userid", user.Id);
                            string url = Url.Action("Index", "Admin");

                            // Use the generated URL as needed (e.g., for redirection)
                            return Redirect(url);

                        }
                        else 
                        {
                            HttpContext.Session.SetInt32("userid", user.Id);
                            string url = Url.Action("Index", "Faculty");
                            return Redirect(url);

                        }
                       

                    }
                    else
                    {
                        // Password is incorrect
                        TempData["Message"] = "Invalid password.";

                    }
                }
                else
                {
                    // User with the specified email doesn't exist
                    TempData["Message"] = "Faculty not found.";
                }
                return View();
            }

        }

    }
}

