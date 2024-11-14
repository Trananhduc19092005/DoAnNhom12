using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewspaperDoAnV2.Models.NewspaperComment
{
    public class NewspaperComment
    {
        public List<Newspaper> newspapers = new List<Newspaper>();
        public List<Comment> comments = new List<Comment>();
    }
}