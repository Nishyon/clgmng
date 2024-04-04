using CollageMng.Data;
using CollageMng.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Xml;

namespace CollageMng.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext db;
        public StudentController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ViewTt()
        {
            ViewBag.dname = "mca";
            ViewBag.sem = "1";
            IEnumerable<Tt> data = db.Tt;
            var filteredData = data.Where(p => p.Dn == "mca" && p.Sem == 1);

            return View(filteredData);
        }

        [HttpPost]
        public IActionResult ViewTt(string dname, int sem)
        {

            IEnumerable<Tt> data = db.Tt;
            var filteredData = data.Where(p => p.Dn == dname && p.Sem == sem);
            ViewBag.dname = dname;
            ViewBag.sem = sem;

            return View(filteredData);
        }
        public IActionResult ViewEvents()
        {
            IEnumerable<Event> events = db.Event;

            return View(events);
        }
        public IActionResult ViewSyllabus()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewSyllabus(String DepartName, int Sems)
        {
            IEnumerable<Syllabus> data = db.Syllabus;

            var filteredData = data.Where(p => p.Dname == DepartName && p.Sem == Sems);
            if (filteredData.Any())
            {
                return View(filteredData);
            }
            else
            {
                TempData["NodataFound"] = "Data not found .";
                return View();
            }

        }

        public IActionResult ViewNotes()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewNotes(String DepartName, int Sems)
        {
            IEnumerable<Note> data = db.Note;

            var filteredData = data.Where(p => p.Dname == DepartName && p.Sem == Sems);
            if (filteredData.Any())
            {
                return View(filteredData);
            }
            else
            {
                TempData["NodataFound"] = "Data not found .";
                return View();
            }

        }


        public IActionResult ViewMark()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewMark(string DepartName, int Sems, int enono)
        {

            if (DepartName == "McaCource" || DepartName == "ImcaCource")
            {
                var data = db.Icmarks.FirstOrDefault(e => e.Dname == DepartName && e.sem == Sems && e.Stuno == enono);
                if (data == null)
                {
                    TempData["NodataFound"] = "Note id not found .";
                    return View();
                }
                else
                {
                    return RedirectToAction("ViewIcMark", "Student", new { sem = Sems, dname = DepartName, stu = data.Stuno, s1 = data.S1, s2 = data.S2, s3 = data.S3, s4 = data.S4, s5 = data.S5, });
                }
            }
            else
            {
                var data = db.Mammarks.FirstOrDefault(e => e.Dname == DepartName && e.sem == Sems && e.Stuno == enono);
                if (data == null)
                {
                    TempData["NodataFound"] = "Note id not found .";
                    return View();
                }
                else
                {
                    return RedirectToAction("ViewMamMark", "Student", new { sem = Sems, dname = DepartName, stu = data.Stuno, s1 = data.S1, s2 = data.S2, s3 = data.S3, s4 = data.S4, s5 = data.S5, s6 = data.S6, s7 = data.S7, });
                }

            }
        }

        public IActionResult ViewIcMark(int sem, string dname, int stu, int s1, int s2, int s3, int s4, int s5)
        {
            if (dname == "McaCource")
            {
                var data2 = db.McaCources.FirstOrDefault(e => e.sem == sem);

                ViewData["sub1"] = data2.code1;
                ViewData["sub2"] = data2.code2;
                ViewData["sub3"] = data2.code3;
                ViewData["sub4"] = data2.code4;
                ViewData["sub5"] = data2.code5;

            }
            else
            {
                var data2 = db.ImcaCource.FirstOrDefault(e => e.sem == sem);

                ViewData["sub1"] = data2.code1;
                ViewData["sub2"] = data2.code2;
                ViewData["sub3"] = data2.code3;
                ViewData["sub4"] = data2.code4;
                ViewData["sub5"] = data2.code5;
            }
            ViewData["Sem"] = sem;
            ViewData["Dname"] = dname;
            ViewData["stu"] = stu;
            ViewData["s1"] = s1;
            ViewData["s2"] = s2;
            ViewData["s3"] = s3;
            ViewData["s4"] = s4;
            ViewData["s5"] = s5;

            return View();

        }
        public IActionResult ViewMamMark(int sem, string dname, int stu, int s1, int s2, int s3, int s4, int s5, int s6, int s7)
        {
            if (dname == "MbaCource")
            {
                var data2 = db.MbaCource.FirstOrDefault(e => e.sem == sem);

                ViewData["sub1"] = data2.code1;
                ViewData["sub2"] = data2.code2;
                ViewData["sub3"] = data2.code3;
                ViewData["sub4"] = data2.code4;
                ViewData["sub5"] = data2.code5;
                ViewData["sub6"] = data2.code6;
                ViewData["sub7"] = data2.code7;

            }
            else
            {
                var data2 = db.ImbaCource.FirstOrDefault(e => e.sem == sem);

                ViewData["sub1"] = data2.code1;
                ViewData["sub2"] = data2.code2;
                ViewData["sub3"] = data2.code3;
                ViewData["sub4"] = data2.code4;
                ViewData["sub5"] = data2.code5;
                ViewData["sub6"] = data2.code6;
                ViewData["sub7"] = data2.code7;
            }
            ViewData["Sem"] = sem;
            ViewData["Dname"] = dname;
            ViewData["stu"] = stu;
            ViewData["s1"] = s1;
            ViewData["s2"] = s2;
            ViewData["s3"] = s3;
            ViewData["s4"] = s4;
            ViewData["s5"] = s5;
            ViewData["s6"] = s6;
            ViewData["s7"] = s7;

            return View();
        }

        public IActionResult ViewProfile() {
            Displaydata();
            if(ViewBag.Profile==null)
            {
                string url = Url.Action("Loginpage", "Home");
                TempData["Message"] = "Please Login Fisrt";
                // Use the generated URL as needed (e.g., for redirection)
                return Redirect(url);
            }
            return View(); 
        }


        public IActionResult UpdateProfile(string error = null)
        {
            Displaydata();
            if (error != null)
            {
                ViewBag.Message = error;
            }

            if (ViewBag.Profile == null)
            {
                string url = Url.Action("Loginpage", "Home");
                TempData["Message"] = "Please Login Fisrt";
                // Use the generated URL as needed (e.g., for redirection)
                return Redirect(url);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string Username, string Email, string Phone, IFormFile file)
        {
            var data = db.Studata.FirstOrDefault(x => x.Id == HttpContext.Session.GetInt32("userid"));
            data.Username = Username;
            data.Email = Email;
            if (!IsAllDigits(Phone) || !Phone10(Phone))
            {
                // Handle the case where the uploaded file is not a valid image

                return RedirectToAction("UpdateProfile", "Student", new { error = "Enter Valid Phone number " });
            }
            data.Phone = Phone;
           
            if (file != null && file.Length > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".Jpeg")
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = $"{timestamp}_{Path.GetFileName(file.FileName)}";

                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uppropic");

                    // Ensure the uploads directory exists, create if necessary
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    var path = Path.Combine(uploadsDirectory, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    string filePath = Path.Combine("wwwroot/uppropic", data.Photo);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    data.Photo = fileName;
                    db.SaveChanges();

                    // Process the uploaded file as needed
                    return RedirectToAction("ViewProfile");
                }
                else
                {
                    // Handle the case where the uploaded file is not a valid image


                    return RedirectToAction("UpdateProfile", "Student", new { error = "Please upload a valid Profile Image (jpg,png,jpeg)." });

                }
            }

            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        public void Displaydata()
        {
            ViewBag.Profile = db.Studata.Where(x => x.Id.Equals(HttpContext.Session.GetInt32("userid"))).FirstOrDefault();
        }
        private bool IsAllDigits(string input)
        {

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
        private bool Phone10(string input)
        {
            int count = 0;
            foreach (char c in input)
            {
                count = count + 1;
            }
            if (count == 10)
            { return true; }
            else
            {
                return false;
            }

        }

    }
}
