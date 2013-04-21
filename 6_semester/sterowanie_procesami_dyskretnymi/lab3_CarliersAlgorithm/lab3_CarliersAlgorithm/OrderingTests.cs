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
        public void ShrageTest1()
        {
            var inputData = Program.LoadData("in50.txt");
            var computedData = Program.ShrageOrdering(inputData);

            Assert.AreEqual(1513, computedData.Value); //from "wyniki.txt"
        }

        [TestMethod]
        public void ShrageTest2()
        {
            var inputData = Program.LoadData("in100.txt");
            var computedData = Program.ShrageOrdering(inputData);

            Assert.AreEqual(3076, computedData.Value); //from "wyniki.txt"
        }

        [TestMethod]
        public void ShrageTest3()
        {
            var inputData = Program.LoadData("in200.txt");
            var computedData = Program.ShrageOrdering(inputData);

            Assert.AreEqual(6416, computedData.Value); //from "wyniki.txt"
        }


        [TestMethod]
        public void PrmtShrageTest1()
        {
            var inputData = Program.LoadData("in50.txt");
            int CMax = Program.PrmtShrageOrdering(inputData).Value;

            Assert.AreEqual(1492, CMax); //from "wyniki.txt" 
        }

        [TestMethod]
        public void PrmtShrageTest2()
        {
            var inputData = Program.LoadData("in100.txt");
            int CMax = Program.PrmtShrageOrdering(inputData).Value;

            Assert.AreEqual(3070, CMax); //from "wyniki.txt" 
        }

        [TestMethod]
        public void PrmtShrageTest3()
        {
            var inputData = Program.LoadData("in200.txt");
            int CMax = Program.PrmtShrageOrdering(inputData).Value;

            Assert.AreEqual(6398, CMax); //from "wyniki.txt" 
        }

        [TestMethod]
        public void CalierTest1()
        {
           var inputData = Program.LoadData("in50.txt");
           var computedData = Program.CaliersOrdering(inputData);

            Assert.AreEqual(1492, computedData.Value); //from "wyniki.txt"        
        }

        [TestMethod]
        public void CalierTest2()
        {
            var inputData = Program.LoadData("in100.txt");
            var computedData = Program.CaliersOrdering(inputData);

            Assert.AreEqual(3070, computedData.Value); //from "wyniki.txt"        
        }
        [TestMethod]
        public void CalierTest3()
        {
            var inputData = Program.LoadData("in200.txt");
            var computedData = Program.CaliersOrdering(inputData);

            Assert.AreEqual(6398, computedData.Value); //from "wyniki.txt"        
        }

        [TestMethod]
        public void CalcATest()
        {
            List<Task> inputData = new List<Task>(){
                new Task(4,1,4),
                new Task(5,1,5),
                new Task(7,1,6),
                new Task(9,1,1)
            };
            int CMax = 14; //precalculated

            Task B = inputData[2];
            int BIndex = 2;
            int U = CMax;
            Task A = inputData.Find(x =>
            {
                int xIndex = inputData.IndexOf(x); //can be optimized
                return U == x.R + inputData.GetRange(xIndex, BIndex - xIndex + 1).Sum(y => y.P) + B.Q; //can be optimized
            });

            Assert.AreEqual(inputData[2], A);


        }
    }
}
