using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.Services;

namespace WsidnTests
{
    [TestClass]
    public class UserSignUpServiceTests
    {
        private Mock<IUserQueries> _userQueriesMock;
        private Mock<IUserCommands> _userCommandsMock;
        public Mock<IHashingWrapper> _hashingWrapperMock;
        private UserSignUpService _service;

        [TestInitialize]
        public void Initialize()
        {
            _userQueriesMock = new Mock<IUserQueries>();

            _userCommandsMock = new Mock<IUserCommands>();
            _userCommandsMock
                .Setup(x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(1);

            _hashingWrapperMock = new Mock<IHashingWrapper>();
            _hashingWrapperMock
                .Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("hash");

            _service = new UserSignUpService(
                _userQueriesMock.Object, 
                _userCommandsMock.Object,
                _hashingWrapperMock.Object);
        }

        [TestMethod]
        public void KnowsWhenUsernameIsAvailable()
        {
            // arrange
            _userQueriesMock
                .Setup(x => x.GetWhetherUsernameExists(It.IsAny<string>()))
                .Returns(false);

            // act
            var result = _service.IsUsernameAvailable("available");

            // assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void KnowsWhenUsernameIsNotAvailable()
        {
            // arrange
            _userQueriesMock
                .Setup(x => x.GetWhetherUsernameExists(It.IsAny<string>()))
                .Returns(true);

            // act
            var result = _service.IsUsernameAvailable("notavailable");

            // assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void KnowsWhenEmailIsAvailable()
        {
            // arrange
            _userQueriesMock
                .Setup(x => x.GetWhetherEmailExists(It.IsAny<string>()))
                .Returns(false);

            // act
            var result = _service.IsUsernameAvailable("notexists");

            // assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void KnowsWhenEmailIsNotAvailable()
        {
            // arrange
            _userQueriesMock
                .Setup(x => x.GetWhetherUsernameExists(It.IsAny<string>()))
                .Returns(true);

            // act
            var result = _service.IsUsernameAvailable("exists");

            // assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void SignUpUserWithHashedPassword()
        {
            // arrange
            // act
            var result = _service.SignUpUser("email", "username", "passord");

            // assert
            _userCommandsMock
                .Verify(x => x.CreateUser("email", "username", "hash"), Times.Once);
            Assert.AreEqual(1, result);
        }
    }
}
