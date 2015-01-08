using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblakoo;

namespace OblakooTest
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestGetFiles()
        {
            var account = new Account(new MockStorage(), new MockDrive("/"));
        }
    }
}
