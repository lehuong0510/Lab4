using Lab4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab4.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            //Trich xuat du lieu tu CSDL va ket hop no voi thong tin Major cua nguoi Learners thong qua quan he Major
            var learners = db.Learners.Include(m => m.Major).ToList();
            return View(learners);
        }
        public IActionResult Create()
        {
            //hien thi danh sach chuyen nganh
            var majors = new List<SelectListItem>();
            foreach(var item in db.Majors)
            {
                majors.Add(new SelectListItem { Text = item.MajorName, Value = item.MajorID.ToString() });
            }
            ViewBag.MajorID = majors;
           // ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //tạo SelectList gửi về View để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }
        //get Update
        public IActionResult Edit(int id)
        {
            var learners = db.Learners.Find(id);
            if (learners == null)
            {
                return NotFound();
            }

            // Tạo SelectList để hiển thị danh sách chuyên ngành (Majors)
            var majors = new SelectList(db.Majors, "MajorID", "MajorName");
            ViewBag.MajorID = majors;

            return View(learners);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learners)
        {
            if (id != learners.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(learners);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Tạo SelectList để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learners);
        }

        // Delete
        public IActionResult Delete(int id)
        {
            var learners = db.Learners.Find(id);
            if (learners == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learners);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            db.Learners.Remove(learner);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
