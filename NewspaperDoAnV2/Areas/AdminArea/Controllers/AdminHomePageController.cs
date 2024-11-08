﻿using NewspaperDoAnV2.Models;
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
        NewspaperV13Entities db = new NewspaperV13Entities();
        
        // GET: AdminArea/AdminHomePage
        public ActionResult Index()
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
    }
}