using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewspaperDoAnV2.Models;

namespace NewspaperDoAnV2.Areas.AdminArea.Controllers
{
    public class NewspapersController : Controller
    {
        private NewspaperV13Entities db = new NewspaperV13Entities();

        // GET: AdminArea/Newspapers
        public ActionResult Index()
        {
            var newspapers = db.Newspapers.Include(n => n.Danh_muc).Include(n => n.User);
            return View(newspapers.ToList());
        }

        // GET: AdminArea/Newspapers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newspaper newspaper = db.Newspapers.Find(id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            return View(newspaper);
        }

        // GET: AdminArea/Newspapers/Create
        public ActionResult Create()
        {
            ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: AdminArea/Newspapers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "NewspaperId,Newspaper_tieude,Newspaper_tieudephu,Newspaper_anh,Newspaper_noidung,UserID,danhmuc_id")] Newspaper newspaper)
        {
            if (ModelState.IsValid)
            {
                db.Newspapers.Add(newspaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }           
            return View(newspaper);
        }

        // GET: AdminArea/Newspapers/Edit/5
        [ValidateInput(false)]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newspaper newspaper = db.Newspapers.Find(id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung", newspaper.danhmuc_id);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", newspaper.UserID);
            return View(newspaper);
        }

        // POST: AdminArea/Newspapers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewspaperId,Newspaper_tieude,Newspaper_tieudephu,Newspaper_anh,Newspaper_noidung,UserID,danhmuc_id")] Newspaper newspaper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newspaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung", newspaper.danhmuc_id);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", newspaper.UserID);
            return View(newspaper);
        }

        // GET: AdminArea/Newspapers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newspaper newspaper = db.Newspapers.Find(id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            return View(newspaper);
        }

        // POST: AdminArea/Newspapers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newspaper newspaper = db.Newspapers.Find(id);
            db.Newspapers.Remove(newspaper);
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
