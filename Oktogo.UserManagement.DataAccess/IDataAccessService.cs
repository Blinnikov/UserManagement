using System.Collections.Generic;

using Oktogo.UserManagement.Entities;

namespace Oktogo.UserManagement.DataAccess
{
    public interface IDataAccessService
    {
        User GetUser(int id);

        User[] GetUsers();

        void SaveUser(User user);

        bool DeleteUser(int id);
    }
}