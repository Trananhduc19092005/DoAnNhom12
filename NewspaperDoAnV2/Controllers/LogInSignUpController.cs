using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;
using NewspaperDoAnV2.Models;

namespace NewspaperDoAnV2.Controllers
{
    public class LogInSignUpController : Controller
    {
        NewspaperV13V2Entities1 db = new NewspaperV13V2Entities1();

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

            // Bắt Lỗi Session == null 

            catch (Exception ex) { return RedirectToAction("Login", "LoginSignUp"); }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            // Tìm Kiếm Xem trong db có Tài Khoản Nào có UserName Tồn Tại chưa

            var list = db.Users.Any(model => model.UserName == user.UserName);
            var Check_Exits_Email = db.Users.Any(model => model.UserEmail.Equals(user.UserEmail));

            if (list)
            {
                ViewBag.Message = "Tài Khoản Đã Tồn Tại !";
                return View();
            }

            if (Check_Exits_Email)
            {
                ViewBag.EmailErrorMessage = "Email Đã Tồn Tại !";
                return View();
            }
            if (ModelState.IsValid)
            {
                if (!list || !Check_Exits_Email)
                {

                    // Mặc Định User Khi Tạo Tài KHoản Role Là Users

                    user.Role_id = 2;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }

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
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                // RoleAdmin
                if (Convert.ToInt32(Session["Roles"].ToString()) == 1)
                {
                    user.UserID = Convert.ToInt32(Session["UserId"].ToString());
                    user.Role_id = 1;
                    user.UserName = Session["username"].ToString();
                    user.UserEmail = Session["UserEmail"].ToString();
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // RoleUser
                if (Convert.ToInt32(Session["Roles"].ToString()) == 2)
                {
                    user.UserID = Convert.ToInt32(Session["UserId"].ToString());
                    user.Role_id = 2;
                    user.UserName = Session["username"].ToString();
                    user.UserEmail = Session["UserEmail"].ToString();
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
            var check = db.Users.Where
            (x => x.UserName.Equals(Users_ThongTin.UserName) && 
            x.UserPassword.Equals(Users_ThongTin.UserPassword)).FirstOrDefault();

            // Role Admin

            if (check != null && check.Role_id == 1)
            {
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
                return RedirectToAction("Index",
                new RouteValueDictionary(
                    new
                    {
                        controller = "AdminHomePage",
                        action = "Index",
                        Area = "AdminArea"
                    }));
            }

            // Role Users
            
            if (check != null && check.Role_id == 2)
            {
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

                Users_ThongTin.Role_id = 2;

                // Lưu Quyền User vào Session

                Session["Roles"] = Users_ThongTin.Role_id.ToString();
                FormsAuthentication.SetAuthCookie(Users_ThongTin.UserName, false);
                return RedirectToAction(null ,
                new RouteValueDictionary(
                    new
                    {
                        controller = "HomePage",
                        action = "HomePage",
                        Area = "UserArea"
                    }));
            }
            ViewBag.ErrorMessage = "Vui Lòng Nhập Lại Sai Toàn Khoản Hoặc Mật Khẩu !";
            return View();
        }

        // Kiểm tra đăng nhập


        // Đăng xuất


        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction(null,
                new RouteValueDictionary(
                    new
                    {
                        controller = "HomePage",
                        action = "HomePage",
                        Area = "UserArea"
                    }));
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
