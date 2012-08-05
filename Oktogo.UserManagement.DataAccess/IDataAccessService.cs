using System.Collections.Generic;

using Oktogo.UserManagement.Entities;

namespace Oktogo.UserManagement.DataAccess
{
    public interface IDataAccessService
    {
        User GetUser(int id);

        int GetUsersCount();

        User[] GetUsers(int pageNumber, int pageSize);

        void SaveUser(User user);

        bool DeleteUser(int id);
    }
}