﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageBuilder.Models;

namespace LanguageBuilder.Controllers
{
    public class UserWordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserWords
        public ActionResult Index()
        {
            var userWords = db.UserWords.Include(u => u.DictWord).Include(u => u.Student);
            return View(userWords.ToList());
        }

        // GET: UserWords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWord userWord = db.UserWords.Find(id);
            if (userWord == null)
            {
                return HttpNotFound();
            }
            return View(userWord);
        }

        // GET: UserWords/Create
        public ActionResult Create()
        {
            ViewBag.DictWordID = new SelectList(db.DictWords, "DictWordID", "word_gender");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName");
            return View();
        }

        // POST: UserWords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserWordID,DictWordID,StudentID,Level,LastReview,NextReview")] UserWord userWord)
        {
            if (ModelState.IsValid)
            {
                db.UserWords.Add(userWord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DictWordID = new SelectList(db.DictWords, "DictWordID", "word_gender", userWord.DictWordID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName", userWord.StudentID);
            return View(userWord);
        }

        // GET: UserWords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWord userWord = db.UserWords.Find(id);
            if (userWord == null)
            {
                return HttpNotFound();
            }
            ViewBag.DictWordID = new SelectList(db.DictWords, "DictWordID", "word_gender", userWord.DictWordID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName", userWord.StudentID);
            return View(userWord);
        }

        // POST: UserWords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserWordID,DictWordID,StudentID,Level,LastReview,NextReview")] UserWord userWord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userWord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DictWordID = new SelectList(db.DictWords, "DictWordID", "word_gender", userWord.DictWordID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName", userWord.StudentID);
            return View(userWord);
        }

        //POST my Add method
        
        public ActionResult Add(int id)
        {

            //string LoggedInID = HttpContext.Current.User.Identity.GetUserId();
            string LoggedInID = HttpContext.User.Identity.Name;
            int studentID = db.Students.Single(s => s.UserCrossID.CompareTo(LoggedInID) == 0).ID;

            var newEntry = new UserWord 
            {
                //StudentID = students.Single(s => s.LastName == "Alexander").ID,
                //DictWordID = DictWord.Single(c => c.german_name == "Haus").DictWordID,
                StudentID = studentID,
                //db.UserProfiles.Single(a => a.UserName == User.Identity.Name);
                DictWordID = id,
                Level = 1,
                LastReview = DateTime.Now,
                NextReview = DateTime.Now.AddDays(3)

            };
            db.UserWords.Add(newEntry);
            db.SaveChanges();
            


            return RedirectToAction("Index", "DictWords");


        }






        // GET: UserWords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWord userWord = db.UserWords.Find(id);
            if (userWord == null)
            {
                return HttpNotFound();
            }
            return View(userWord);
        }

        // POST: UserWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserWord userWord = db.UserWords.Find(id);
            db.UserWords.Remove(userWord);
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
