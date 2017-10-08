using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhatShouldIDoNow.Services;

namespace WsidnTests
{
    [TestClass]
    public class HashingWrapperTests
    {
        [TestMethod]
        public void CanIdentifyBcryptHash()
        {
            // arrange
            var bcryptHash = "$2b$10$2tD2xnNS51hnDXEl/FRyyOwXWsh15N2XoCT/xcEmC09XDjkN/flNG";
            var wrapper = new HashingWrapper();

            // act
            var result = wrapper.IsBcryptHash(bcryptHash);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanIdentifyNonBcryptHash()
        {
            // arrange
            var bcryptHash = "nothashed";
            var wrapper = new HashingWrapper();

            // act
            var result = wrapper.IsBcryptHash(bcryptHash);

            // assert
            Assert.IsFalse(result);
        }
    }
}
