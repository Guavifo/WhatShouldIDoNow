using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using WhatShouldIDoNow.Controllers;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.DataAccess.Models;
using WhatShouldIDoNow.Models;
using WhatShouldIDoNow.Services;

namespace WsidnTests
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<ISecurityService> _securityServiceMock;
        private Mock<IUserQueries> _userQueriesMock;
        private Mock<IUserSignUpService> _userSignUpServiceMock;
        private UserController _controller;
        private User _user;

        [TestInitialize]
        public void Initialize()
        {
            _user = new User
            {
                Id = 1,
                UserName = "username",
                PasswordHash = "hash",
                Email = "email"
            };

            _securityServiceMock = new Mock<ISecurityService>();
            _securityServiceMock
                .Setup(x => x.VerifyUserPassword("username", "password"))
                .Returns(new PasswordStatus { IsPasswordHashed = true, IsPasswordMatch = true });

            _userQueriesMock = new Mock<IUserQueries>();
            _userQueriesMock
                .Setup(x => x.GetUserByUserName("username"))
                .Returns(_user);

            _userSignUpServiceMock = new Mock<IUserSignUpService>();
            _controller = new UserController(
                _securityServiceMock.Object,
                _userQueriesMock.Object,
                _userSignUpServiceMock.Object);
        }

        [TestMethod]
        public async Task DontUpdatePasswordWhenAlreadyHashed()
        {
            // arrange
            var plainPassword = "password";
            var username = "username";

            // act
            var result = await _controller.SignIn(
                new SignInViewModel
                {
                    Password = plainPassword,
                    UserName = username
                });

            // assert
            var redirect = result as RedirectResult;
            Assert.IsNotNull(redirect);
            _securityServiceMock
                .Verify(x => x.UpdateUserPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdatePasswordWhenItsPlainText()
        {
            // arrange
            _securityServiceMock
                .Setup(x => x.VerifyUserPassword("username", "plainpassword"))
                .Returns(new PasswordStatus
                {
                    IsPasswordHashed = false,
                    IsPasswordMatch = true
                });

            // act
            var result = await _controller.SignIn(
                new SignInViewModel
                {
                    Password = "plainpassword",
                    UserName = "username"
                });

            // assert
            var redirect = result as RedirectResult;
            Assert.IsNotNull(redirect);
            _securityServiceMock
                .Verify(x => x.UpdateUserPassword("username", "plainpassword"), Times.Once);
        }

        [TestMethod]
        public async Task DoNotUpdatePasswordWhenItsPlainTextAndDoesNotMatch()
        {
            // arrange
            _securityServiceMock
                .Setup(x => x.VerifyUserPassword("username", "plainpassword"))
                .Returns(new PasswordStatus
                {
                    IsPasswordHashed = false,
                    IsPasswordMatch = false
                });

            // act
            var result = await _controller.SignIn(
                new SignInViewModel
                {
                    Password = "plainpassword",
                    UserName = "username"
                });

            // assert
            var redirect = result as ActionResult;
            Assert.IsNotNull(redirect);
            _securityServiceMock
                .Verify(x => x.UpdateUserPassword("username", "plainpassword"), Times.Never);
        }
    }
}
