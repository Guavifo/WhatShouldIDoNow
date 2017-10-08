﻿using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.Services;
using Microsoft.AspNetCore.Http;

namespace WsidnTests
{
    [TestClass]
    public class SecurityServiceTests
    {
        private Mock<IHttpContextAccessor> _httpContext;
        private SecurityService _securityService;
        private Mock<IUserQueries> _userQueryMock;
        private Mock<IHashingWrapper> _hashingWrapperMock;
 
        [TestInitialize]
        public void Intialize()
        {
            _httpContext = new Mock<IHttpContextAccessor>();

            // used http://bcrypthashgenerator.apphb.com/?PlainText=password to get hash
            _userQueryMock = new Mock<IUserQueries>();
            _userQueryMock
                .Setup(x => x.GetPasswordHashByUserName("username"))
                .Returns("hash");

            _hashingWrapperMock = new Mock<IHashingWrapper>();
            _hashingWrapperMock
                .Setup(x => x.Verify("password", "hash"))
                .Returns(true);

            _securityService = new SecurityService(
                _httpContext.Object, 
                _userQueryMock.Object,
                _hashingWrapperMock.Object);
        }

        [TestMethod]
        public void PasswordMatchesHash()
        {
            // arrange
            var password = "password";
            var username = "username";

            // act
            var result = _securityService.VerifyUserPassword(username, password);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PasswordDoesNotMatchHash()
        {
            // arrange
            var password = "wrongpassword";
            var username = "username";

            // act
            var result = _securityService.VerifyUserPassword(username, password);

            // assert
            Assert.IsFalse(result);
        }
    }
}
