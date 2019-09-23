using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRace;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRacingCar()
        {
            string expected = "Busted" + Environment.NewLine + "bob won 120" + Environment.NewLine + "ai won 140" + Environment.NewLine;
            MainForm obj = new MainForm(false, true, true, 50);
            string actual = obj.unitTest();
            Assert.AreEqual(expected, actual);


        }
    }
}
