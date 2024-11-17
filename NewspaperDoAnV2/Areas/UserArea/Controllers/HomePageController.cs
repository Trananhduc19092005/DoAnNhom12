using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NewspaperDoAnV2.Models;
using NewspaperDoAnV2.Models.NewspaperSearch;
using NewspaperDoAnV2.Models.NewspaperComment;
using System.Web.Routing;

namespace NewspaperDoAnV2.Areas.UserArea.Controllers
{
    public class HomePageController : Controller
    {

        NewspaperV13V2Entities1 db = new NewspaperV13V2Entities1();

        // Trang chủ chính

        public ActionResult HomePage(string OrderBy)
        {
            var newspaper = db.Newspapers.AsQueryable();
            switch (OrderBy)
            {
                case "name_asc": newspaper = newspaper.OrderBy(x => x.Newspaper_tieude); break;
                case "name_desc":newspaper = newspaper.OrderByDescending(x => x.Newspaper_tieude); break;
            }
            return View(newspaper.ToList());
        }

        // Hiển Thị Bài Báo KHi ấn vào đọc thêm

        public ActionResult BaiBao(int? NewspaperId)
        {
            var NewspaperFind = db.Newspapers.Where(x => x.NewspaperId == NewspaperId);
            var CommentFind = db.Comments.Where(x => x.NewspaperId == NewspaperId);
            Session["Newspaper"] = NewspaperId;

            var NewspaperComment = new NewspaperComment()
            {
                comments = CommentFind.OrderByDescending(x => x.ThoiDiem_Comment).ToList(),
                newspapers = NewspaperFind.ToList(),
            };
            return View(NewspaperComment);
        }


        // Đăng Comment Lên Bài Báo

        [HttpPost]
        public ActionResult DangCommentLenBaiBao(string Noidung)
        {
            try
            {
                var FindNewspaperId = Convert.ToInt32(Session["Newspaper"].ToString());
                var New_newcomment = new Comment()
                {
                    comment_noidung = Noidung,
                    // Cho UserId bằng với Session["userid] nếu người dùng đăng nhập
                    UserID = Convert.ToInt32(Session["UserId"].ToString()),
                    NewspaperId = FindNewspaperId,
                    ThoiDiem_Comment = DateTime.Now
                };
                db.Comments.Add(New_newcomment);
                db.SaveChanges();
                return RedirectToAction(null,
                   new RouteValueDictionary(
                       new
                       {
                           controller = "HomePage",
                           action = "BaiBao",
                           NewspaperId = FindNewspaperId
                       }));
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Login", "LogInSignUp", new { area = "" });
            }
        }
        // Trang Thể Thao

        public ActionResult TheThao(string OrderBy)
        {
            
                var newspaper = db.Newspapers.AsQueryable();
                switch (OrderBy)
                {
                    case "name_asc": newspaper = newspaper.OrderBy(x => x.Newspaper_tieude); break;
                    case "name_desc": newspaper = newspaper.OrderByDescending(x => x.Newspaper_tieude); break;
                }
                return View(newspaper.ToList());
        }

        // Trang Bất Động Sản

        public ActionResult BatDongSan(string OrderBy)
        {
            var newspaper = db.Newspapers.AsQueryable();
            switch (OrderBy)
            {
                case "name_asc": newspaper = newspaper.OrderBy(x => x.Newspaper_tieude); break;
                case "name_desc": newspaper = newspaper.OrderByDescending(x => x.Newspaper_tieude); break;
            }
            return View(newspaper.ToList());
        }

        // Trang Công Nghệ

        public ActionResult CongNghe(string OrderBy)
        {
            var newspaper = db.Newspapers.AsQueryable();
            switch (OrderBy)
            {
                case "name_asc": newspaper = newspaper.OrderBy(x => x.Newspaper_tieude); break;
                case "name_desc": newspaper = newspaper.OrderByDescending(x => x.Newspaper_tieude); break;
            }
            return View(newspaper.ToList());
        }

        // Trang Chính Trị

        public ActionResult ChinhTri(string OrderBy)
        {
          
                var newspaper = db.Newspapers.AsQueryable();
                switch (OrderBy)
                {
                    case "name_asc": newspaper = newspaper.OrderBy(x => x.Newspaper_tieude); break;
                    case "name_desc": newspaper = newspaper.OrderByDescending(x => x.Newspaper_tieude); break;
                }
                return View(newspaper.ToList());
        }

        // Trang Tìm Kiếm

        public ActionResult Search(string search , string OrderByDanhMuc , string OrderByName)
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
    }
}