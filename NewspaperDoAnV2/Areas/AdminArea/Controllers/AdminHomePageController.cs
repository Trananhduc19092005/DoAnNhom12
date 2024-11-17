using NewspaperDoAnV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewspaperDoAnV2.Areas.AdminArea.Controllers
{
    public class AdminHomePageController : Controller
    {
        NewspaperV13V2Entities1 db = new NewspaperV13V2Entities1();

        // GET: AdminArea/AdminHomePage

        public ActionResult Index()
        {
            if (IsAdmin_IsLogin())
            {
                try
                {
                    var items = new Newspaper_ChuyenMuc
                    {
                        danhmucList = db.Danh_muc.ToList(),
                        newspaperList = db.Newspapers.ToList(),
                    };
                    List<Newspaper_ChuyenMuc> List = new List<Newspaper_ChuyenMuc>
                {
                    items
                };
                    return View(List.AsEnumerable());
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Login", "Users");
                }
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
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