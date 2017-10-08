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

            _hashingWrapperMock = new Mock<IHashingWrapper>();

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
    }
}
