using System;
using System.Collections.Generic;

using Oktogo.UserManagement.DataAccess;
using Oktogo.UserManagement.Entities;

namespace Oktogo.UserManagement.Services
{
    public class UserManagementService : IUserManagementService
    {
        private IDataAccessService DataAccessService { get; set; }

        public UserManagementService(IDataAccessService dataAccessService)
        {
            if (dataAccessService == null)
            {
                throw new ArgumentNullException("dataAccessService");
            }
            DataAccessService = dataAccessService;
        }

        public User GetUser(int id)
        {
            return DataAccessService.GetUser(id);
        }

        public User[]  GetUsers()
        {
            return DataAccessService.GetUsers();
        }

        public void SaveUser(User user)
        {
            DataAccessService.SaveUser(user);
        }

        public bool DeleteUser(int id)
        {
            return DataAccessService.DeleteUser(id);
        }
    }
}
