using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab3_CarliersAlgorithm
{
    [TestClass]
    public class OrderingTests
    {
        [TestMethod]
        public void ShrageTest()
        {
            var inputData = Program.LoadData("in50.txt");
            var computedData = Program.ShrageOrdering(inputData);

            Assert.AreEqual(1513, computedData.Value); //from "wyniki.txt"
        }

        [TestMethod]
        public void PrmtShrageTest()
        {
            var inputData = Program.LoadData("in50.txt");
            int CMax = Program.PrmtShrageOrdering(inputData);

            //Assert.AreEqual(1492, CMax); //from "wyniki.txt" //its not that!!!
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShrageStarTest()
        {
            var inputData = Program.LoadData("in50.txt");
            int CMax = Program.PrmtShrageOrdering(inputData);

            Assert.AreEqual(1492, CMax); //from "wyniki.txt" 
        }
    }
}
