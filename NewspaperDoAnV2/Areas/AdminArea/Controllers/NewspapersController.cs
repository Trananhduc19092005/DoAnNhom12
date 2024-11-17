using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewspaperDoAnV2.Models;
using NewspaperDoAnV2.Models.NewspaperSearch;

namespace NewspaperDoAnV2.Areas.AdminArea.Controllers
{
    public class NewspapersController : Controller
    {
        NewspaperV13V2Entities1 db = new NewspaperV13V2Entities1();


        // GET: AdminArea/Newspapers
        public ActionResult Index(string search, string OrderByDanhMuc, string OrderByName)
        {
            if (IsAdmin_IsLogin())
            {
   
                NewspaperSearchVM searchVM = new NewspaperSearchVM();

                var NewspaperList = db.Newspapers.AsQueryable();

                if (search != null)
                {
                    searchVM.NewspaperName = search;
                    NewspaperList = NewspaperList.Where(x => x.Newspaper_tieude.Contains(search));
                    ViewBag.Search = search;
                }


                if (OrderByDanhMuc != null)
                {
                    switch (OrderByDanhMuc)
                    {
                        case "BatDongSan": NewspaperList = NewspaperList.Where(x => x.Danh_muc.danhmuc_noidung == "Bất Động Sản"); break;
                        case "ChinhTri": NewspaperList = NewspaperList.Where(x => x.Danh_muc.danhmuc_noidung == "Chính Trị"); break;
                        case "TheThao": NewspaperList = NewspaperList.Where(x => x.Danh_muc.danhmuc_noidung == "Thể Thao"); break;
                        case "CongNghe": NewspaperList = NewspaperList.Where(x => x.Danh_muc.danhmuc_noidung == "Công Nghệ"); break;
                        default:
                            NewspaperList = NewspaperList.OrderBy(x => x.NewspaperId);
                            break;
                    }
                    searchVM.NewspaperOrderByDanhMuc = OrderByDanhMuc;
                }

                if (OrderByName != null)
                {
                    switch (OrderByName)
                    {
                        case "name_asc": NewspaperList = NewspaperList.OrderBy(x => x.Newspaper_tieude); break;
                        case "name_desc": NewspaperList = NewspaperList.OrderByDescending(x => x.Newspaper_tieude); break;

                        default:
                            NewspaperList = NewspaperList.OrderBy(x => x.NewspaperId);
                            break;
                    }
                    searchVM.OrderByName = OrderByName;
                }


                searchVM.newspaperlist = NewspaperList.ToList();

                return View(searchVM);
        
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // GET: AdminArea/Newspapers/Details/5
        public ActionResult Details(int? id)
        {
            if (IsAdmin_IsLogin())
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
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // GET: AdminArea/Newspapers/Create
        public ActionResult Create()
        {
            if (IsAdmin_IsLogin())
            {
                ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung");
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
                return View();
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // POST: AdminArea/Newspapers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Newspaper newspaper)
        {
            if (ModelState.IsValid)
            {
                ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung");
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
                var new_newspaper = new Newspaper()
                {
                    NewspaperId = newspaper.NewspaperId,
                    Newspaper_tieude = newspaper.Newspaper_tieude,
                    Newspaper_tieudephu = newspaper.Newspaper_tieudephu,
                    Newspaper_anh = newspaper.Newspaper_anh,
                    Newspaper_noidung = newspaper.Newspaper_noidung,
                    UserID = Convert.ToInt32(Session["UserId"].ToString()),
                    danhmuc_id = newspaper.danhmuc_id
                };
                db.Newspapers.Add(new_newspaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newspaper);
        }

        // GET: AdminArea/Newspapers/Edit/5
        [ValidateInput(false)]

        public ActionResult Edit(int? id)
        {
            if (IsAdmin_IsLogin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Newspaper newspaper = db.Newspapers.Find(id);
                User users = new User();
                if (newspaper == null)
                {
                    return HttpNotFound();
                }

                string test = "";
                if (newspaper.User.UserName == "duc19092005")
                {
                    test = newspaper.User.UserName;
                }

                ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung", newspaper.danhmuc_id);

                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", test);

                return View(newspaper);
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
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
                newspaper.UserID = Convert.ToInt32(Session["UserId"].ToString());
                db.Entry(newspaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.danhmuc_id = new SelectList(db.Danh_muc, "danhmuc_id", "danhmuc_noidung", newspaper.danhmuc_id);
            return View(newspaper);
        }

        // GET: AdminArea/Newspapers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsAdmin_IsLogin())
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
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // POST: AdminArea/Newspapers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Newspaper newspaper = db.Newspapers.Find(id);
                db.Newspapers.Remove(newspaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                return RedirectToAction("NewspaperError", "ErrorMessage", new { controller = "ErrorMessage" });
            }
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
