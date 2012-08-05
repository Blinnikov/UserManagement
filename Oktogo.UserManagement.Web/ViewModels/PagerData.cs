using System;

namespace Oktogo.UserManagement.Web.ViewModels
{
    public class PagerData
    {
        public int UsersCount { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int PagesCount
        {
            get { return (int)Math.Ceiling((decimal)UsersCount / PageSize); }
        }
    }
}