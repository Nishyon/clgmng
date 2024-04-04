using CollageMng.Data;
using CollageMng.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollageMng.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext db;
        public AdminController(AppDbContext _db) {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Login model)
        {
          
                db.Login_Data.Add(model);
                db.SaveChanges();
            
            return View();
        }
        public IActionResult AddStu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStu(Studata model, IFormFile file,string dname,int sem)
        {
            
            if(file != null && file.Length > 0)
            {

                if (!IsAllDigits(model.Phone) || !Phone10(model.Phone))
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid Phone number";
                    return View();
                }
                if (!IsAllDigits(model.Stuno))
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid Student number";
                    return View();
                }

                var allData = db.Studata.ToList();


                // Process the data as needed
                foreach (var item in allData)
                {
                    if (item.Stuno == model.Stuno)
                    {
                        ViewBag.Message = "This Student's enrollment number is alredy exist in databse ! ";
                        return View();
                    }
                }

                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".png")
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

                    model.Role = "Student";
                    model.Dname = dname;
                    model.Sem = sem;
                    model.Photo = fileName;
                    db.Studata.Add(model);
                    db.SaveChanges();

                    // Process the uploaded file as needed
                    return View();
                }
                else
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid image file (jpg, jpeg, png).";
                    return View();
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                ViewBag.Message = "Please upload a Image";
                return View();
            }
        }

        public IActionResult AddFac()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFac(Facdata model, IFormFile file, string dname)
        {

            if (file != null && file.Length > 0)
            {
                if (!IsAllDigits(model.Phone) || !Phone10(model.Phone))
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid Phone number";
                    return View();
                }
               
                
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".png")
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

                   
                    model.Dname = dname;
                    model.Photo = fileName;
                    db.Facdata.Add(model);
                    db.SaveChanges();

                    // Process the uploaded file as needed
                    return View();
                }
                else
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid image file (jpg, jpeg, png).";
                    return View();
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                ViewBag.Message = "Please upload a Image";
                return View();
            }
        }

        public IActionResult ChangeTimetable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeTimetable(string dname, int sem)
        {
            
            return RedirectToAction("ChangeTt", "Admin", new { sem = sem, dname = dname});
        }
       
        public IActionResult ChangeTt(int sem,string dname)
        {

            ViewBag.dname = dname;
            ViewBag.sem = sem;
            IEnumerable<Tt> data = db.Tt;
            var filteredData = data.Where(p => p.Dn == dname && p.Sem == sem);

            return View(filteredData);
        }
        [HttpPost]
        public IActionResult ChangeTt(string day, string dname, int sem, string a, string b, string c, string d, string e)
        {



            var timetable = db.Tt.FirstOrDefault(t => t.Dn == dname && t.Sem == sem && t.Day == day);

            timetable.Lec1 = a;
            timetable.Lec2 = b;
            timetable.Lec3 = c;
            timetable.Lec4 = d;
            timetable.Lec5 = e;

            db.SaveChanges();
            return RedirectToAction("ChangeTimetable");
        }
       
        [HttpGet]
        public IActionResult ManageEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManageEvent(Event data, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".png")
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = $"{timestamp}_{Path.GetFileName(file.FileName)}";

                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

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

                    data.Img = fileName;
                    db.Event.Add(data);
                    db.SaveChanges();

                    // Process the uploaded file as needed
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid image file (jpg, jpeg, png).";
                    return View();
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                ViewBag.Message = "Please upload a Image";
                return View();
            }
        }


        public IActionResult RemoveEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveEvent(int eventid)
        {
            var entityToDelete = db.Event.Find(eventid);

            if (entityToDelete == null)
            {
                TempData["AlertMessage"] = "Event id not found .";
                return View();// Or some other appropriate action
            }
            string fileName = entityToDelete.Img;

            string filePath = Path.Combine("wwwroot/uploads", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            db.Event.Remove(entityToDelete);
            db.SaveChanges();

            return View();
        }

        public IActionResult ViewEvents()
        {
            IEnumerable<Event> events = db.Event;
            
            return View(events);
        }

        public IActionResult AddSyllabus()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddSyllabus(Syllabus data, IFormFile file)
        {
            if (IsAllDigits(data.Msg))
            {           
            
            if (file != null && file.Length > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".pdf")
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = $"{timestamp}_{Path.GetFileName(file.FileName)}";

                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upsyllabus");

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

                    data.filePath = fileName;
                    db.Syllabus.Add(data);
                    db.SaveChanges();

                    // Process the uploaded file as needed
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid Syllabus file (pdf).";
                    return View();
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                ViewBag.Message = "Please upload a File";
                return View();
            }
            }
            else
            {
                ViewBag.Message = "Subject code contain only integer number";
                return View();
            }
        }

       

        public IActionResult RemoveSyllabus()
        {
            return View();
        }

        public IActionResult ViewSyllabus()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewSyllabus(string DepartName, int Sems)
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


        [HttpPost]
        public IActionResult RemoveSyllabus(int sylid)
        {
            var entityToDelete = db.Syllabus.Find(sylid);

            if (entityToDelete == null)
            {
                TempData["AlertMessage"] = "Syllabus id not found .";
                return View();// Or some other appropriate action
            }

            string fileName = entityToDelete.filePath;

            string filePath = Path.Combine("wwwroot/upsyllabus", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }


            // Step 2: Delete the entity
            db.Syllabus.Remove(entityToDelete);
            db.SaveChanges();

            return View();
        }

        public IActionResult ViewNotes()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewNotes(string DepartName, int Sems)
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
                    return RedirectToAction("ViewIcMark", "Admin", new { sem = Sems, dname = DepartName, stu = data.Stuno, s1 = data.S1, s2 = data.S2, s3 = data.S3, s4 = data.S4, s5 = data.S5, });
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
                    return RedirectToAction("ViewMamMark", "Admin", new { sem = Sems, dname = DepartName, stu = data.Stuno, s1 = data.S1, s2 = data.S2, s3 = data.S3, s4 = data.S4, s5 = data.S5, s6 = data.S6, s7 = data.S7, });
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

        public IActionResult ViewTt() {
            ViewBag.dname = "mca";
            ViewBag.sem = "1";
            IEnumerable<Tt> data = db.Tt;
            var filteredData = data.Where(p => p.Dn == "mca" && p.Sem == 1);

            return View(filteredData); 
        }

        [HttpPost]
        public IActionResult ViewTt(string dname,int sem)
        {
            
            IEnumerable<Tt> data = db.Tt;
            var filteredData = data.Where(p => p.Dn == dname && p.Sem == sem);
            ViewBag.dname = dname;
            ViewBag.sem = sem;

            return View(filteredData);
        }

        public IActionResult ViewProfile()
        {
            Displaydata();
            if (ViewBag.Profile == null)
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
        public async Task<IActionResult> UpdateProfile(string Username, string Email, string Phone, IFormFile file, string ClassCordinator, string Education, string Speciality, string dname)
        {
            var data = db.Facdata.FirstOrDefault(x => x.Id == HttpContext.Session.GetInt32("userid"));
            data.Username=Username;
            data.Email= Email;
            if (!IsAllDigits(Phone) || !Phone10(Phone))
            {
                // Handle the case where the uploaded file is not a valid image

                return RedirectToAction("UpdateProfile", "Admin", new { error = "Enter Valid Phone number "});
            }
            data.Phone = Phone;
            data.ClassCordinator = ClassCordinator;
            data.Education = Education;
            data.Speciality = Speciality;
            data.Dname = dname;
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
                    

                    return RedirectToAction("UpdateProfile", "Admin", new { error = "Please upload a valid Profile Image (jpg,png,jpeg)." });

                }
            }

            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        public IActionResult UpdateSem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateSem(string DepartName,int Sems)
        {
            return RedirectToAction("UpSem", "Admin", new { dname = DepartName , sem = Sems});
        }

        public IActionResult UpSem(string dname,int sem,string error=null)
        {
            IEnumerable<Studata> data = db.Studata;
            var filteredData = data.Where(p => p.Dname == dname && p.Sem == sem);
            if(error!= null)
            {
                ViewBag.error=error;
            }
            ViewBag.dname = dname;
            ViewBag.sem = sem;
            return View(filteredData);
        }

        [HttpPost]
        public IActionResult UpSem(string sno,string dname, string osem, int sem)
        {
            if(sem<=0)
            {
                return RedirectToAction("UpSem", "Admin", new { dname = dname, sem = sem ,error="Enter valid Semester"});
            }

            var data = db.Studata.FirstOrDefault(t => t.Stuno == sno);

            data.Sem = sem;

            db.SaveChanges();
            return RedirectToAction("UpSem", "Admin", new { dname = dname, sem = osem});

        }

        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(Gallery model,IFormFile file)
        {
            if (file != null && file.Length > 0)
            { 
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".png")
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = $"{timestamp}_{Path.GetFileName(file.FileName)}";

                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Gallery");

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

                    model.Photo = fileName;
                    db.Gallery.Add(model);
                    db.SaveChanges();

                    // Process the uploaded file as needed
                    return View();
                }
                else
                {
                    // Handle the case where the uploaded file is not a valid image
                    ViewBag.Message = "Please upload a valid image file (jpg, jpeg, png).";
                    return View();
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                ViewBag.Message = "Please upload a Image";
                return View();
            }
        }

        public IActionResult RemoveImage()
        {
            IEnumerable<Gallery> data = db.Gallery;
            return View(data);
        }

        [HttpPost]
        public IActionResult RemoveImage(int Imgid)
        {
            var entityToDelete = db.Gallery.Find(Imgid);
            if (entityToDelete == null)
            {
                TempData["AlertMessage"] = "Image id not found .";
                return View();// Or some other appropriate action
            }
            string fileName = entityToDelete.Photo;

            string filePath = Path.Combine("wwwroot/Gallery", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            db.Gallery.Remove(entityToDelete);
            db.SaveChanges();

            return RedirectToAction("RemoveImage");
        }


        public void Displaydata()
        {
            ViewBag.Profile = db.Facdata.Where(x => x.Id.Equals(HttpContext.Session.GetInt32("userid"))).FirstOrDefault();
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
            if(count == 10)
            { return true; }
            else
            {
                return false;
            }
            
        }
    }
}
