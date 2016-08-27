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
    public class DictWordsController : Controller
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

            var words = from w in db.DictWords
                        where w.DictWordID > 4
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

        // GET: DictWords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictWord dictWord = db.DictWords.Find(id);
            if (dictWord == null)
            {
                return HttpNotFound();
            }
            return View(dictWord);
        }

        // GET: DictWords/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictWords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "word_gender,german_name,english_name,speech_type,conjugation")] DictWord dictWord)
        {
            if (ModelState.IsValid)
            {

                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[DictWords] ON");

                db.DictWords.Add(dictWord);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[DictWords] OFF");


                return RedirectToAction("Index");



            }

            return View(dictWord);
        }

        // GET: DictWords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictWord dictWord = db.DictWords.Find(id);
            if (dictWord == null)
            {
                return HttpNotFound();
            }
            return View(dictWord);
        }

        // POST: DictWords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DictWordID,word_gender,german_name,english_name,speech_type,conjugation")] DictWord dictWord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictWord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dictWord);
        }

        // GET: DictWords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictWord dictWord = db.DictWords.Find(id);
            if (dictWord == null)
            {
                return HttpNotFound();
            }
            return View(dictWord);
        }

        // POST: DictWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DictWord dictWord = db.DictWords.Find(id);
            db.DictWords.Remove(dictWord);
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
