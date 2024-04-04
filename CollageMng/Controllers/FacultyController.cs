using CollageMng.Data;
using CollageMng.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Xml;
using System;
using System.IO;
using CollageMng.ViewModel;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CollageMng.Controllers
{
    public class FacultyController : Controller
    {
        private readonly AppDbContext db;
        public FacultyController(AppDbContext _db)
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

        [HttpGet]
        public IActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note data, IFormFile file)
        {
            if (IsAllDigits(data.Msg))
            {

                if (file != null && file.Length > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".pdf")
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string fileName = $"{timestamp}_{Path.GetFileName(file.FileName)}";

                        var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upnotes");

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
                        db.Note.Add(data);
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

        public IActionResult RemoveNote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveNote(int nid)
        {
            var entityToDelete = db.Note.Find(nid);

            if (entityToDelete == null)
            {
                TempData["AlertMessage"] = "Note id not found .";
                return View();// Or some other appropriate action
            }

            string fileName = entityToDelete.filePath;

            string filePath = Path.Combine("wwwroot/upnotes", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }


            // Step 2: Delete the entity
            db.Note.Remove(entityToDelete);
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

        public IActionResult AddMark()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMark(string DepartName, int Sems)
        {

            if (DepartName == "McaCource")
            {
                var data = db.McaCources.FirstOrDefault(e => e.sem == Sems);
                
                return RedirectToAction("AddIcMark", "Faculty", new { sem = data.sem, dname = DepartName,s1=data.code1, s2 = data.code2, s3 = data.code3, s4 = data.code4, s5 = data.code5, });
            }
            else if (DepartName == "MbaCource")
            {

                var data = db.MbaCource.FirstOrDefault(e => e.sem == Sems);
               
                return RedirectToAction("AddMamMark", "Faculty", new { sem = data.sem, dname = DepartName, s1 = data.code1, s2 = data.code2, s3 = data.code3, s4 = data.code4, s5 = data.code5, s6 = data.code6, s7 = data.code7 });
            }
            else if (DepartName == "ImcaCource")
            {
                var data = db.ImcaCource.FirstOrDefault(e => e.sem == Sems);
                return RedirectToAction("AddIcMark", "Faculty", new { sem = data.sem, dname = DepartName, s1 = data.code1, s2 = data.code2, s3 = data.code3, s4 = data.code4, s5 = data.code5, });
            }
            else
            {
                var data = db.ImbaCource.FirstOrDefault(e => e.sem == Sems);
                return RedirectToAction("AddMamMark", "Faculty", new { sem = data.sem, dname = DepartName, s1 = data.code1, s2 = data.code2, s3 = data.code3, s4 = data.code4, s5 = data.code5, s6 = data.code6, s7 = data.code7 });
            }

        }

        public IActionResult AddIcMark(int sem,string dname,string s1,string s2, string s3, string s4, string s5)
        {

            ViewData["Sem"] = sem;
            ViewData["Dname"] = dname;
                ViewData["s1"]= s1;
                ViewData["s2"]= s2;
                ViewData["s3"]= s3;
                ViewData["s4"]= s4;
                ViewData["s5"]= s5;

            

            return View();
            
        }
        public IActionResult AddMamMark(int sem, string dname, string s1, string s2, string s3, string s4, string s5, string s6, string s7)
        {
            ViewData["Sem"] = sem;
            ViewData["Dname"] = dname;
            ViewData["s1"] = s1;
            ViewData["s2"] = s2;
            ViewData["s3"] = s3;
            ViewData["s4"] = s4;
            ViewData["s5"] = s5;
            ViewData["s6"] = s6;
            ViewData["s7"] = s7;



            return View();

        }
        [HttpPost]
        public IActionResult AddIcMark(Icmarks data)
        {
            var allData = db.Mammarks.ToList();


            // Process the data as needed
            foreach (var item in allData)
            {
                if (item.Stuno == data.Stuno)
                {
                    ViewBag.Number = "This Student's enrollment number is alredy exist in databse ! ";
                    if (data.Dname == "MbaCource")
                    {
                        var data2 = db.MbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                    else
                    {
                        var data2 = db.ImbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                }
            }

            var allData2 = db.Icmarks.ToList();

            foreach (var item in allData2)
            {
                if (item.Stuno == data.Stuno)
                {
                    ViewBag.Number = "This Student's enrollment number is alredy exist in databse ! ";
                    if (data.Dname == "MbaCource")
                    {
                        var data2 = db.MbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                    else
                    {
                        var data2 = db.ImbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                }
            }

            db.Icmarks.Add(data);
            db.SaveChanges();

            return RedirectToAction("AddMark");

        }
        [HttpPost]
        public IActionResult AddMamMark(Mammarks data)
        {
            var allData = db.Mammarks.ToList();
            

            // Process the data as needed
            foreach (var item in allData)
            {
                if (item.Stuno == data.Stuno )
                {
                    ViewBag.Number = "This Student's enrollment number is alredy exist in databse ! ";
                    if (data.Dname == "MbaCource")
                    {
                        var data2 = db.MbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                    else
                    {
                        var data2 = db.ImbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                }
            }

            var allData2 = db.Icmarks.ToList(); 

            foreach (var item in allData2)
            {
                if (item.Stuno == data.Stuno)
                {
                    ViewBag.Number = "This Student's enrollment number is alredy exist in databse ! ";
                    if (data.Dname == "MbaCource")
                    {
                        var data2 = db.MbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                    else
                    {
                        var data2 = db.ImbaCource.FirstOrDefault(e => e.sem == data.sem);
                        ViewData["Sem"] = data.sem;
                        ViewData["Dname"] = data.Dname;
                        ViewData["s1"] = data2.code1;
                        ViewData["s2"] = data2.code2;
                        ViewData["s3"] = data2.code3;
                        ViewData["s4"] = data2.code4;
                        ViewData["s5"] = data2.code5;
                        ViewData["s6"] = data2.code6;
                        ViewData["s7"] = data2.code7;

                        return View();
                    }
                }
            }


            db.Mammarks.Add(data);
            db.SaveChanges();

            return RedirectToAction("AddMark");

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
                var data = db.Icmarks.FirstOrDefault(e => e.Dname == DepartName && e.sem == Sems && e.Stuno==enono);
                if (data == null)
                {
                    TempData["NodataFound"] = "Note id not found .";
                    return View();
                }
                else
                {
                    return RedirectToAction("ViewIcMark", "Faculty", new { sem = Sems, dname = DepartName, stu = data.Stuno, s1 = data.S1, s2 = data.S2, s3 = data.S3, s4 = data.S4, s5 = data.S5, });
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
                    return RedirectToAction("ViewMamMark", "Faculty", new { sem = Sems, dname = DepartName, stu = data.Stuno, s1 = data.S1, s2 = data.S2, s3 = data.S3, s4 = data.S4, s5 = data.S5, s6 = data.S6, s7 = data.S7, });
                }
                
            }
        }

        public IActionResult ViewIcMark(int sem,string dname, int stu , int s1 , int s2 , int s3 ,int s4 , int s5)
        {
            if(dname== "McaCource")
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

        public IActionResult ViewAllMark()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewAllMark(string DepartName, int Sems)
        {
            return RedirectToAction("SeeAllMark", "Faculty", new { sem = Sems, dname = DepartName  });
        }

        public IActionResult SeeAllMark(int sem,string dname)
        {
            if (dname == "MbaCource" || dname == "ImbaCource")
            {

                var Alldata = db.Mammarks.Where(e => e.Dname == dname && e.sem == sem).OrderBy(e => e.Stuno).ToList();
                if(dname=="MbaCource")
                {
                    var Cource = db.MbaCource.Where(e =>  e.sem == sem).FirstOrDefault();
                    ViewBag.c1 = Cource.code1;
                    ViewBag.c2 = Cource.code2;
                    ViewBag.c3 = Cource.code3;
                    ViewBag.c4 = Cource.code4;
                    ViewBag.c5 = Cource.code5;
                    ViewBag.c6 = Cource.code6;
                    ViewBag.c7 = Cource.code7;
                }
                else
                {
                    var Cource = db.ImbaCource.Where(e => e.sem == sem).FirstOrDefault();
                    ViewBag.c1 = Cource.code1;
                    ViewBag.c2 = Cource.code2;
                    ViewBag.c3 = Cource.code3;
                    ViewBag.c4 = Cource.code4;
                    ViewBag.c5 = Cource.code5;
                    ViewBag.c6 = Cource.code6;
                    ViewBag.c7 = Cource.code7;

                }


                ViewBag.Data = Alldata;
                ViewBag.dname = dname;
                ViewBag.sem = sem;
                return View();

            }
            else
            {
              
                var Alldata = db.Icmarks.Where(e => e.Dname == dname && e.sem == sem).OrderBy(e => e.Stuno).ToList();
                if (dname == "McaCource")
                {
                    var Cource = db.McaCources.Where(e => e.sem == sem).FirstOrDefault();
                    ViewBag.c1 = Cource.code1;
                    ViewBag.c2 = Cource.code2;
                    ViewBag.c3 = Cource.code3;
                    ViewBag.c4 = Cource.code4;
                    ViewBag.c5 = Cource.code5;
                }
                else
                {
                    var Cource = db.ImcaCource.Where(e => e.sem == sem).FirstOrDefault();
                    ViewBag.c1 = Cource.code1;
                    ViewBag.c2 = Cource.code2;
                    ViewBag.c3 = Cource.code3;
                    ViewBag.c4 = Cource.code4;
                    ViewBag.c5 = Cource.code5;

                }

                ViewBag.Data = Alldata;
                ViewBag.dname = dname;
                ViewBag.sem = sem;
                return View();

            }

        }

        [HttpPost]
        public IActionResult SeeAllMark(string dname,int sem) {
            if(dname== "McaCource")
            {
                var allRecords = db.Icmarks.Where(e => e.Dname == dname && e.sem == sem ).ToList();
                db.Icmarks.RemoveRange(allRecords);
                db.SaveChanges();
            }
            else if (dname == "MbaCource")
            {
                var allRecords = db.Mammarks.Where(e => e.Dname == dname && e.sem == sem).ToList();
                db.Mammarks.RemoveRange(allRecords);
                db.SaveChanges();
            }
            else if (dname == "ImcaCource")
            {
                var allRecords = db.Icmarks.Where(e => e.Dname == dname && e.sem == sem).ToList();
                db.Icmarks.RemoveRange(allRecords);
                db.SaveChanges();
            }
            else 
            {
                var allRecords = db.Mammarks.Where(e => e.Dname == dname && e.sem == sem).ToList();
                db.Mammarks.RemoveRange(allRecords);
                db.SaveChanges();
            }

            return RedirectToAction("ViewAllMark");
        }

        public IActionResult RemoveMark()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveMark(string dname,int id)
        {
            if (dname == "McaCource" )
            {
                var allRecords = db.Icmarks.Where(e => e.Dname == dname && e.Stuno == id).FirstOrDefault();
                if (allRecords == null)
                {
                    TempData["NodataFound"] = "Record not found .";
                    return View();
                }
                TempData["dataFound"] = "Record Deleted.";
                db.Icmarks.Remove(allRecords);
                db.SaveChanges();

            }
            else
            {
                
                var allRecords = db.Mammarks.Where(e => e.Dname == dname && e.Stuno == id).FirstOrDefault();
                if (allRecords == null)
                {
                    TempData["NodataFound"] = "Record not found .";
                    return View();
                }
                TempData["dataFound"] = "Record Deleted.";
                db.Mammarks.Remove(allRecords);

                db.SaveChanges();
            }
            return View();
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
            data.Username = Username;
            data.Email = Email;
            if (!IsAllDigits(Phone) || !Phone10(Phone))
            {
                // Handle the case where the uploaded file is not a valid image

                return RedirectToAction("UpdateProfile", "Faculty", new { error = "Enter Valid Phone number " });
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


                    return RedirectToAction("UpdateProfile", "Faculty", new { error = "Please upload a valid Profile Image (jpg,png,jpeg)." });

                }
            }

            db.SaveChanges();
            return RedirectToAction("ViewProfile");
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
            if (count == 10)
            { return true; }
            else
            {
                return false;
            }

        }

    }
}
