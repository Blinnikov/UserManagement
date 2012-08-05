using System;
using System.Collections.Generic;
using System.ServiceModel;

using Oktogo.UserManagement.Entities;

namespace Oktogo.UserManagement.Services
{
    [ServiceContract]
    public interface IUserManagementService
    {
        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        int GetUsersCount();

        [OperationContract]
        User[] GetUsers(int pageNumber, int pageSize);

        [OperationContract]
        void SaveUser(User user);

        [OperationContract]
        bool DeleteUser(int id);
    }
}
