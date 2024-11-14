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

        NewspaperV13Entities db = new NewspaperV13Entities();
        // GET: UserArea/HomePage

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



        public ActionResult BaiBao(int? id)
        {
            var NewspaperFind = db.Newspapers.Where(x => x.NewspaperId == id);
            var CommentFind = db.Comments.Where(x => x.NewspaperId == id);
            Session["NewspaperID"] = id;

            var NewspaperComment = new NewspaperComment()
            {
                comments = CommentFind.ToList(),
                newspapers = NewspaperFind.ToList(),
            };
            return View(NewspaperComment);
        }


        [HttpPost]
        public ActionResult DangCommentLenBaiBao(string Noidung)
        {
            var FindNewspaperId = Convert.ToInt32(Session["NewspaperID"].ToString());
            var New_newcomment = new Comment()
            {
                comment_noidung = Noidung,

                // Cho UserId bằng với Session["userid] nếu người dùng đăng nhập

                UserID = Convert.ToInt32(Session["UserId"].ToString()),
                NewspaperId = FindNewspaperId
            };
            db.Comments.Add(New_newcomment);
            db.SaveChanges();
            return RedirectToAction("BaiBao",
                    new RouteValueDictionary(
                        new
                        {
                            controller = "HomePage",
                            action = "BaiBao",
                            id = Convert.ToInt32(Session["NewspaperID"].ToString())
                        }));
        }


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