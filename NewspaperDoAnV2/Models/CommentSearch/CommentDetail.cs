using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewspaperDoAnV2.Models.CommentSearch
{
    public class CommentDetail
    {
        public string SortByCommentDetail { get; set; }

        public string SortByCommentTime { get; set; }

        public List<Comment> CommentList { get; set;} = new List<Comment>();
    }
}