using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NewspaperDoAnV2.Models;

namespace NewspaperDoAnV2.Areas.UserArea.Controllers
{
    public class HomePageController : Controller
    {

        NewspaperV13Entities db = new NewspaperV13Entities();
        // GET: UserArea/HomePage
        public ActionResult HomePage()
        {
            return View(db.Newspapers.ToList());
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


        public ActionResult TheThao()
        {
            return View(db.Newspapers.ToList());
        }

        public ActionResult BatDongSan()
        {
            return View(db.Newspapers.ToList());
        }

        public ActionResult CongNghe()
        {
            return View(db.Newspapers.ToList());
        }

        public ActionResult ChinhTri()
        {
            return View(db.Newspapers.ToList());
        }
    }
}