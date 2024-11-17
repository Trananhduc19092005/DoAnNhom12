using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewspaperDoAnV2.Models;
using NewspaperDoAnV2.Models.CommentSearch;
using NewspaperDoAnV2.Models.NewspaperSearch;

namespace NewspaperDoAnV2.Controllers
{
    public class CommentDetailController : Controller
    {
        NewspaperV13V2Entities1 db = new NewspaperV13V2Entities1();

        public ActionResult Index(string OrderByDanhMucSortByCommentDetail, string OrderByCommentTime)
        {
            if (Session["username"] != null)
            {
                CommentDetail cmtdetail = new CommentDetail();
                var CommentList = db.Comments.AsQueryable();
                if (OrderByDanhMucSortByCommentDetail != null)
                {
                    switch (OrderByDanhMucSortByCommentDetail)
                    {
                        case "name_asc": CommentList = CommentList.OrderBy(x => x.comment_noidung); break;
                        case "name_desc": CommentList = CommentList.OrderByDescending(x => x.comment_noidung); break;

                        default:
                            CommentList = CommentList.OrderBy(x => x.commentid); break;
                    }
                    cmtdetail.SortByCommentDetail = OrderByDanhMucSortByCommentDetail;
                }

                if (OrderByCommentTime != null)
                {
                    switch (OrderByCommentTime)
                    {
                        case "time_asc": CommentList = CommentList.OrderBy(x => x.ThoiDiem_Comment); break;
                        case "time_desc": CommentList = CommentList.OrderByDescending(x => x.ThoiDiem_Comment); break;
                    }
                    cmtdetail.SortByCommentTime = OrderByCommentTime;
                }


                cmtdetail.CommentList = CommentList.ToList();

                return View(cmtdetail);
            }
            return RedirectToAction("Login", "LogInSignUp");
        }

        public ActionResult Edit(int? id , int ? newspaperid , DateTime ? datetime)
        {
            if (Session["username"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment cmt = db.Comments.Find(id);
                if (cmt == null)
                {
                    return HttpNotFound();
                }
                Session["NewspaperCommentId"] = newspaperid;
                Session["CommentId"] = id;
                Session["Datetime"] = datetime;
                return View(cmt);
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.commentid = Convert.ToInt32(Session["CommentId"]);
                comment.UserID = Convert.ToInt32(Session["UserId"].ToString());
                comment.NewspaperId = Convert.ToInt32(Session["NewspaperCommentId"]);
                comment.ThoiDiem_Comment = Convert.ToDateTime(Session["Datetime"]);
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index" , "CommentDetail");
            }
            return View(comment);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["username"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment commentFind = db.Comments.Find(id);
                if (commentFind == null)
                {
                    return HttpNotFound();

                }
                return View(commentFind);
            }
            return RedirectToAction("Login", "LogInSignUp", new { area = "" });
        }

        // POST: AdminArea/Danh_muc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}