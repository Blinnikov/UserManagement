using System;
using System.Linq;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using Oktogo.UserManagement.Web.Controllers;
using Oktogo.UserManagement.Web.UserManagementService;
using Oktogo.UserManagement.Web.ViewModels;

namespace OktogoUserManagement.Tests
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserManagementService> mock;

        [SetUp]
        public void SetUp()
        {
            mock = new Mock<IUserManagementService>();
            var users = new[]
            {
                new User { Id = 1, FirstName = "George", LastName = "Washington", Email = "george.washington@whitehouse.gov" },
                new User { Id = 2, FirstName = "John", LastName = "Adams", Email = "george.washington@whitehouse.gov" },
                new User { Id = 3, FirstName = "Thomas", LastName = "Jefferson", Email = " thomas.jefferson.gov" },
                new User { Id = 4, FirstName = "James", LastName = "Madison", Email = "james.madison@whitehouse.gov " },
                new User { Id = 5, FirstName = "James", LastName = "Monroe", Email = "james.monroe@whitehouse.gov" },
                new User { Id = 6, FirstName = "John", LastName = "Quincy Adams", Email = "john.quincy.adams@whitehouse.gov" }
            };

            mock.Setup(m => m.GetUsersCount()).Returns(users.Length);
            mock.Setup(m => m.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int pageNumber, int pageSize) => users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray());
            mock.Setup(m => m.GetUser(It.IsAny<int>())).Returns((int i) => users.FirstOrDefault(u => u.Id == i));
            mock.Setup(m => m.DeleteUser(It.IsAny<int>())).Returns((int i) => users.Any(u => u.Id != i));
        }

        [Test]
        public void IndexAction_WhenInvoking_ReturnsFirstPageOfUsers()
        {
            var controller = new UserController(mock.Object);

            var usersResult = ((UsersListViewModel)controller.Index().ViewData.Model).Users;

            Assert.AreEqual(5, usersResult.Length);
            Assert.AreEqual("George", usersResult[0].FirstName);
            Assert.AreEqual("John", usersResult[1].FirstName);
            Assert.AreEqual("Thomas", usersResult[2].FirstName);
            Assert.AreEqual("James", usersResult[3].FirstName);
            Assert.AreEqual("James", usersResult[4].FirstName);
        }

        [Test]
        public void IndexAction_WhenInvoking_CanPaginate()
        {
            var controller = new UserController(mock.Object);
            var result = ((UsersListViewModel)controller.Index(2, 3).Model).Users;
            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual("Madison", result[0].LastName);
            Assert.AreEqual("Monroe", result[1].LastName);
            Assert.AreEqual("Quincy Adams", result[2].LastName);
        }

        [Test]
        public void IndexAction_WhenInvoking_ReturnViewModel()
        {
            var controller = new UserController(mock.Object);

            var viewModel = (UsersListViewModel)controller.Index(2, 2).Model;

            var pagerData = viewModel.PagerData;
            Assert.AreEqual(2, pagerData.PageNumber);
            Assert.AreEqual(2, pagerData.PageSize);
            Assert.AreEqual(6, pagerData.UsersCount);
            Assert.AreEqual(3, pagerData.PagesCount);
        }

        [Test]
        public void EditAction_WhenInvoking_AllowsEditExistingUser()
        {
            var controller = new UserController(mock.Object);
            var firstUser = (User)((ViewResult)controller.Edit(1)).Model;
            var seconduser = (User)((ViewResult)controller.Edit(2)).Model;
            var thirduser = (User)((ViewResult)controller.Edit(3)).Model;

            Assert.AreEqual(1, firstUser.Id);
            Assert.AreEqual(2, seconduser.Id);
            Assert.AreEqual(3, thirduser.Id);
        }

        [Test]
        public void EditAction_WhenInvoking_ProhibitsEditNonexistingUser()
        {
            var controller = new UserController(mock.Object);
            var result = controller.Edit(101);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void EditAction_CanSaveValidChanges()
        {
            var controller = new UserController(mock.Object);
            var user = new User { FirstName = "Igor", LastName = "Blinnikov" };
            var actionResult = controller.Edit(user);

            mock.Verify(m => m.SaveUser(user));
            Assert.IsNotInstanceOf<ViewResult>(actionResult);
        }

        [Test]
        public void EditAction_CannotSaveInvalidChanges()
        {
            var controller = new UserController(mock.Object);
            var user = new User { FirstName = "Igor", LastName = "Blinnikov" };
            controller.ModelState.AddModelError("Email", "Email is required");
            var actionResult = controller.Edit(user);

            mock.Verify(m => m.SaveUser(It.IsAny<User>()), Times.Never());
            Assert.IsInstanceOf<ViewResult>(actionResult);
        }

        [Test]
        public void DeleteAction_WhenInvoking_AllowsDeleteExistingUser()
        {
            var user = new User { Id = 1, FirstName = "George", LastName = "Washington", Email = "george.washington@whitehouse.gov" };
            var controller = new UserController(mock.Object);
            var result = controller.Delete(user.Id);
            mock.Verify(m => m.DeleteUser(user.Id));
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }
    }
}