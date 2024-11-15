using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using NewspaperDoAnV2.Models;

namespace NewspaperDoAnV2.Areas.AdminArea.Controllers
{
    public class Danh_mucController : Controller
    {
        
        NewspaperV13Entities db = new NewspaperV13Entities();

        // GET: AdminArea/Danh_muc
        public ActionResult Index()
        {
            if (IsAdmin_IsLogin())
            {
                return View(db.Danh_muc.ToList());
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // GET: AdminArea/Danh_muc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Danh_muc danh_muc = db.Danh_muc.Find(id);
            if (danh_muc == null)
            {
                return HttpNotFound();
            }
            return View(danh_muc);
        }

        // GET: AdminArea/Danh_muc/Create
        public ActionResult Create()
        {
            if (IsAdmin_IsLogin())
            {
                return View();
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // POST: AdminArea/Danh_muc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "danhmuc_id,danhmuc_noidung")] Danh_muc danh_muc)
        {
            if (ModelState.IsValid)
            {
                db.Danh_muc.Add(danh_muc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danh_muc);
        }

        // GET: AdminArea/Danh_muc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsAdmin_IsLogin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Danh_muc danh_muc = db.Danh_muc.Find(id);
                if (danh_muc == null)
                {
                    return HttpNotFound();
                }
                return View(danh_muc);
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // POST: AdminArea/Danh_muc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "danhmuc_id,danhmuc_noidung")] Danh_muc danh_muc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danh_muc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danh_muc);
        }

        // GET: AdminArea/Danh_muc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsAdmin_IsLogin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Danh_muc danh_muc = db.Danh_muc.Find(id);
                if (danh_muc == null)
                {
                    return HttpNotFound();

                }
                return View(danh_muc);
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // POST: AdminArea/Danh_muc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Danh_muc danh_muc = db.Danh_muc.Find(id);
            db.Danh_muc.Remove(danh_muc);
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

        public bool IsAdmin_IsLogin()
        {
            if (Session["username"] != null && Convert.ToInt32(Session["Roles"]) == 1 && Session["Roles"] != null)
            {
                return true;
            }
            return false;
        }
    }
}
