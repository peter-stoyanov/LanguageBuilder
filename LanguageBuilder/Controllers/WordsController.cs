using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageBuilder.Models;
using PagedList;

namespace LanguageBuilder.Controllers
{
    public class WordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Words
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "german_name" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var words = from w in db.Words
                           select w;

            if (!String.IsNullOrEmpty(searchString))
            {
                words = words.Where(w => w.german_name.Contains(searchString));
                                       //|| w.english_name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "german_name":
                    words = words.OrderByDescending(w => w.german_name);
                    break;
                default:
                    words = words.OrderBy(w => w.german_name);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(words.ToPagedList(pageNumber, pageSize));
            //return View(db.Words.ToList());
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words words = db.Words.Find(id);
            if (words == null)
            {
                return HttpNotFound();
            }
            return View(words);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,german_name,english_name,word_gender")] Words words)
        {
            if (ModelState.IsValid)
            {
                db.Words.Add(words);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(words);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words words = db.Words.Find(id);
            if (words == null)
            {
                return HttpNotFound();
            }
            return View(words);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,german_name,english_name,word_gender")] Words words)
        {
            if (ModelState.IsValid)
            {
                db.Entry(words).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(words);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words words = db.Words.Find(id);
            if (words == null)
            {
                return HttpNotFound();
            }
            return View(words);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Words words = db.Words.Find(id);
            db.Words.Remove(words);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
