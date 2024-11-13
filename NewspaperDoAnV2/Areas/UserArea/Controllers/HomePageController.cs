using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NewspaperDoAnV2.Models;
using NewspaperDoAnV2.Models.NewspaperSearch;

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
            var Find = db.Newspapers.Find(id);

            List<Newspaper> newspapaerList = new List<Newspaper>
            {
                Find
            };

            return View(newspapaerList.AsEnumerable());
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