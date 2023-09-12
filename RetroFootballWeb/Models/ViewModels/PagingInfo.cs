using System.Reflection;

namespace RetroFootballWeb.Models.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public PagingInfo()
        {

        }
        public PagingInfo(int totalIems, int page, int pageSize = 8)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalIems / pageSize);
            int currentPage = page;
            int startPage = currentPage - 1;
            int endPage = currentPage + 1;

            if (totalPages < 3)
            {
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                if (startPage <= 0)
                {
                    startPage = 1;
                    endPage = 3;
                }
                if (endPage > totalPages)
                {
                    endPage = totalPages;
                    startPage = endPage - 2;
                }
            }

            TotalItems = totalIems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
