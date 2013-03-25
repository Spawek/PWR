using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab2_NEH;
using System.Collections.Generic;

namespace NEH_tests
{
    [TestClass]
    public class UnitTest1
    {
        private LinkedList<Task> taskList;
        private Task taskToInsert;

        [TestInitialize]
        public void TestInit()
        {
            taskList = new LinkedList<Task>();
            taskList.AddLast(new Task(new List<int>() { 9, 4, 5, 8, 6 })); //z3
            taskList.AddLast(new Task(new List<int>() { 5, 9, 8, 10, 1 })); //z1
            taskList.AddLast(new Task(new List<int>() { 9, 3, 10, 1, 8 })); //z2

            taskToInsert = new Task(new List<int>() { 4, 8, 8, 7, 2 });
        }


        [TestMethod]
        public void EArrCreationTest()
        {
            int [,] actual = Program.CalcEArr(taskList);
            int[,] expectedArr = new int[4, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 9, 13, 18, 26, 32 }, { 0, 14, 23, 31, 41, 42 }, { 0, 23, 26, 41, 42, 50 } };

            CollectionAssert.AreEqual(expectedArr, actual);
        }

        [TestMethod]
        public void QArrCreationTest()
        {
            int[,] actual = Program.CalcQArr(taskList);
            int[,] expectedArr = new int[4, 6] { { 50, 40, 32, 27, 15, 0 }, { 41, 36, 27, 19, 9, 0 }, { 31, 22, 19, 9, 8, 0 }, { 0, 0, 0, 0, 0, 0 } };

            CollectionAssert.AreEqual(expectedArr, actual);
        }

        [TestMethod]
        public void FArrCreationTest()
        {
            int[,] actual = Program.CalcFArr(Program.CalcEArr(taskList), taskToInsert);
            int[,] expectedArr = new int[4, 6] { {0, 4, 12, 20, 27, 29 }, {0, 13, 21, 29, 36, 38 }, {0, 18, 31, 39, 48, 50 }, {0, 27, 35, 49, 56, 58 } };

            CollectionAssert.AreEqual(expectedArr, actual);
        }

        [TestMethod]
        public void FPlusQCreationTest()
        {
            var actual = Program.AddArrays(
                Program.CalcFArr(Program.CalcEArr(taskList), taskToInsert), 
                Program.CalcQArr(taskList)
            );

            int[,] expectedArr = new int[4, 6] { {54, 52, 52, 54, 44, 0 }, {54, 57, 56, 55, 47, 0}, {49, 53, 58, 57, 58, 0 }, {27, 35, 49, 56, 58, 0 } };

            CollectionAssert.AreEqual(expectedArr, actual);
        }

        [TestMethod]
        public void FindBestColTest()
        {
            var FPlusQ = Program.AddArrays(
                Program.CalcFArr(Program.CalcEArr(taskList), taskToInsert),
                Program.CalcQArr(taskList)
            );

            int minCol = Program.FindMinColInFPlusQArr(FPlusQ);
            Assert.AreEqual(0, minCol);
        }


    }
}
