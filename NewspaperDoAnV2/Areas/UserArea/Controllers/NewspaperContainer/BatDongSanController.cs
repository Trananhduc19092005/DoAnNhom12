using NewspaperDoAnV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace NewspaperDoAnV2.Areas.UserArea.Controllers.NewspaperContainer
{
    public class BatDongSanController : Controller
    {
        // GET: UserArea/BatDongSan
        private NewspaperV13Entities db = new NewspaperV13Entities();
        public ActionResult Index()
        {
            var newspapers = db.Newspapers.Include(n => n.Danh_muc).Include(n => n.User);
            return View(newspapers);
        }

        // Tìm Kiếm Bài Báo có Id

        public ActionResult BaiBao(int? id)
        {
            List<Newspaper> nb = new List<Newspaper>
            {
                db.Newspapers.Find(id)
            };
            return View(nb.AsEnumerable());
        }
    }
}