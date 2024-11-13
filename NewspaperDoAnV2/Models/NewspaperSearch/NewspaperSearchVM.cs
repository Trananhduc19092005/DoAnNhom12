using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewspaperDoAnV2.Models.NewspaperSearch
{
    public class NewspaperSearchVM
    {
        public string NewspaperName { get; set; }

        public string NewspaperOrderByDanhMuc { get; set; }

        public string OrderByName { get; set; }

        public List<Newspaper> newspaperlist { get; set; }

    }
}