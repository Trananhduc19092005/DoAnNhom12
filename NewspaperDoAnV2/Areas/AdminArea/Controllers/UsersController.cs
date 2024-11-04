using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NewspaperDoAnV2.Models;

namespace NewspaperDoAnV2.Areas.AdminArea.Controllers
{
    public class UsersController : Controller
    {
        private NewspaperV13Entities db = new NewspaperV13Entities();

        // GET: AdminArea/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Phan_Quyen);
            return View(users.ToList());
        }

        // GET: AdminArea/Users/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: AdminArea/Users/Create
        public ActionResult Create()
        {
            ViewBag.Role_id = new SelectList(db.Phan_Quyen, "Role_id", "Role_name");
            return View();
        }

        // POST: AdminArea/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserPassword,UserRePassword,Repassword,UserEmail,Role_id")] User user)
        {
            var list = db.Users.Any(model => model.UserName == user.UserName);

            if (list)
            {
                ViewBag.Message = "This Account Is Already Exist";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
               if (!list)
               {
                    user.Role_id = 1;

                    db.Users.Add(user);

                    db.SaveChanges();

                    return RedirectToAction("Login");
               }
            }

            ViewBag.Role_id = new SelectList(db.Phan_Quyen, "Role_id", "Role_name", user.Role_id);
            return View(user);
        }

        // GET: AdminArea/Users/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_id = new SelectList(db.Phan_Quyen, "Role_id", "Role_name", user.Role_id);
            return View(user);
        }

        // POST: AdminArea/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "UserID,UserName,UserPassword,Role_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_id = new SelectList(db.Phan_Quyen, "Role_id", "Role_name", user.Role_id);
            return View(user);
        }

        // GET: AdminArea/Users/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: AdminArea/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }


        // Đăng Nhập 

        [HttpPost]
        public ActionResult Login(User Users_ThongTin)
        {
            if (CheckLogin(Users_ThongTin))
            {
                Session["username"] = Users_ThongTin.UserName.ToString(); 
                Session["userid"] = Users_ThongTin.UserID.ToString();
                return RedirectToAction("Index" , "AdminHomePage");
            }
            return RedirectToAction("Login");
        }

        // Kiểm tra đăng nhập


        public bool CheckLogin(User Users_ThongTin)
        {
            var check = db.Users.Where(x => x.UserName.Equals(Users_ThongTin.UserName) && x.UserPassword.Equals(Users_ThongTin.UserPassword)).FirstOrDefault();
            if (check != null) 
            {
                return true;
            }
            return false;
        }


        // Đăng xuất


        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index" , "Users");
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
