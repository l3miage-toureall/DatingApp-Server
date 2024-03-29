using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemsPage, int totalItems, int totalPage)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPage;
            TotalItems = totalItems;
            TotalPage = totalPage;
        }

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPage { get; set; }
    }
}