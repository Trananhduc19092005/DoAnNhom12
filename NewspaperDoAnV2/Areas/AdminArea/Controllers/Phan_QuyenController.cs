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
    public class Phan_QuyenController : Controller
    {
        private NewspaperV13Entities db = new NewspaperV13Entities();

        // GET: AdminArea/Phan_Quyen
        public ActionResult Index()
        {
            return View(db.Phan_Quyen.ToList());
        }

        // GET: AdminArea/Phan_Quyen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phan_Quyen phan_Quyen = db.Phan_Quyen.Find(id);
            if (phan_Quyen == null)
            {
                return HttpNotFound();
            }
            return View(phan_Quyen);
        }

        // GET: AdminArea/Phan_Quyen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/Phan_Quyen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Role_id,Role_name")] Phan_Quyen phan_Quyen)
        {
            if (ModelState.IsValid)
            {
                db.Phan_Quyen.Add(phan_Quyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phan_Quyen);
        }

        // GET: AdminArea/Phan_Quyen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phan_Quyen phan_Quyen = db.Phan_Quyen.Find(id);
            if (phan_Quyen == null)
            {
                return HttpNotFound();
            }
            return View(phan_Quyen);
        }

        // POST: AdminArea/Phan_Quyen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Role_id,Role_name")] Phan_Quyen phan_Quyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phan_Quyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phan_Quyen);
        }

        // GET: AdminArea/Phan_Quyen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phan_Quyen phan_Quyen = db.Phan_Quyen.Find(id);
            if (phan_Quyen == null)
            {
                return HttpNotFound();
            }
            return View(phan_Quyen);
        }

        // POST: AdminArea/Phan_Quyen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phan_Quyen phan_Quyen = db.Phan_Quyen.Find(id);
            db.Phan_Quyen.Remove(phan_Quyen);
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
