using Oktogo.UserManagement.Web.UserManagementService;

namespace Oktogo.UserManagement.Web.ViewModels
{
    public class UsersListViewModel
    {
        public User[] Users { get; set; }

        public PagerData PagerData { get; set; }
    }
}