using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ReportCompilerTest
{
    /// <summary>
    /// Das ist die Testklasse der Klasse Connection im Projekt ReportCompiler
    ///</summary>
    [TestClass()]
    public class ConnectionTest
    {
        /// <summary>
        /// Testet das Verhalten der Property Database
        ///</summary>
        [TestMethod()]
        public void DatabaseTest()
        {
            Connection target = new Connection(); // TODO: Initialize to an appropriate value
            string expected = "TestDatenbank"; // TODO: Initialize to an appropriate value
            string actual;
            target.Database = expected;
            actual = target.Database;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet das Verhalten der Property Id
        ///</summary>
        [TestMethod()]
        public void IdTest()
        {
            Connection target = new Connection(); // TODO: Initialize to an appropriate value
            string expected = "Test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet das Verhalten der Property Password
        ///</summary>
        [TestMethod()]
        public void PasswordTest()
        {
            Connection target = new Connection(); // TODO: Initialize to an appropriate value
            string expected = "1234"; // TODO: Initialize to an appropriate value
            string actual;
            target.Password = expected;
            actual = target.Password;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Testet das Verhalten der Property Server
        ///</summary>
        [TestMethod()]
        public void ServerTest()
        {
            Connection target = new Connection(); // TODO: Initialize to an appropriate value
            string expected = "TestServer"; // TODO: Initialize to an appropriate value
            string actual;
            target.Server = expected;
            actual = target.Server;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet das Verhalten der Property Type
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            Connection target = new Connection(); // TODO: Initialize to an appropriate value
            string expected = "int"; // TODO: Initialize to an appropriate value
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet das Verhalten der Property Username
        ///</summary>
        [TestMethod()]
        public void UsernameTest()
        {
            Connection target = new Connection(); // TODO: Initialize to an appropriate value
            string expected = "Benuter"; // TODO: Initialize to an appropriate value
            string actual;
            target.Username = expected;
            actual = target.Username;
            Assert.AreEqual(expected, actual);
        }
    }
}
