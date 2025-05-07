using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
   public class ProductQueryParams
    {

        private const int DefultPageSize  = 5;
        private const int MaximumPageSize = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }

        public string? SearchValue { get; set; }

        public int PageIndex { get; set; }=1;
        //public int PageSize { get; set; }


        private int pagesize = DefultPageSize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaximumPageSize ? MaximumPageSize : value; }



        }


    }
}
