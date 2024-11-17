using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewspaperDoAnV2.Areas.AdminArea.Controllers
{
    public class ErrorMessageController : Controller
    {
        // GET: AdminArea/ErrorMessage
        public ActionResult DanhMucError()
        {
            return View();
        }

        public ActionResult NewspaperError()
        {
            return View();
        }
    }
}