using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
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
            try
            {
                var users = db.Users.Include(u => u.Phan_Quyen);
                var username = Session["username"].ToString();
                var GetUserName = db.Users.FirstOrDefault(x => x.UserName.Equals(username));
                return View(GetUserName);
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Login" , "Users");
            }
        }

        public ActionResult Create()
        {
            ViewBag.Role_id = new SelectList(db.Phan_Quyen, "Role_id", "Role_name");
            return View();
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = Convert.ToInt32(Session["UserId"].ToString());
                user.Role_id = 1;
                user.UserName = Session["username"].ToString();
                user.UserEmail = Session["UserEmail"].ToString();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
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
                // Lưu Session UserName

                Session["username"] = Users_ThongTin.UserName.ToString();

                // Lưu Thông Tin UserName Để Truy Vấn

                var username = Session["username"].ToString();

                // Truy Vấn Để Lấy UserId và UserEmail

                var UserId = db.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault().UserID;
                var UserEmail = db.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault().UserEmail;

                // Lưu Session UserId và Email

                Session["UserId"] = UserId;
                Session["UserEmail"] = UserEmail;
                

                // Cho User Một Session

                Users_ThongTin.Role_id = 1;

                // Lưu Quyền User vào Session

                Session["Roles"] = Users_ThongTin.Role_id.ToString();
                FormsAuthentication.SetAuthCookie(Users_ThongTin.UserName, false);
                return RedirectToAction("Index", "AdminHomePage");
            }
            return RedirectToAction("Login");
        }

        // Kiểm tra đăng nhập


        private bool CheckLogin(User Users_ThongTin)
        {
            var check = db.Users.Where(x => x.UserName.Equals(Users_ThongTin.UserName) && x.UserPassword.Equals(Users_ThongTin.UserPassword) && x.Role_id == 1).FirstOrDefault();
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
