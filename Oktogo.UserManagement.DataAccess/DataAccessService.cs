using System.Collections.Generic;
using System.Linq;

using Oktogo.UserManagement.Entities;

namespace Oktogo.UserManagement.DataAccess
{
    public class DataAccessService : IDataAccessService
    {
        public User GetUser(int id)
        {
            using (var db = new UserDbContext())
            {
                return db.Users.Find(id);
            }
        }

        public User[] GetUsers(int pageNumber, int pageSize)
        {
            using (var db = new UserDbContext())
            {
                return db.Users
                    .OrderBy(p => p.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToArray();
            }
        }

        public void SaveUser(User user)
        {
            using (var db = new UserDbContext())
            {
                if (user.Id == 0)
                {
                    db.Users.Add(user);
                }
                else
                {
                    var existingUser = db.Users.Find(user.Id);
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                }
                db.SaveChanges();
            }
        }

        public bool DeleteUser(int id)
        {
            using (var db = new UserDbContext())
            {
                var currentUser = db.Users.Find(id);
                if (currentUser == null)
                {
                    return false;
                }
                db.Users.Remove(currentUser);
                db.SaveChanges();
                return true;
            }
        }
    }
}