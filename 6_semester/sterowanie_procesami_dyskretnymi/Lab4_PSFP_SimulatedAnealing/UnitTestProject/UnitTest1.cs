using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab4_PSFP_SimulatedAnealing;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalcCMaxforPermutationTest()
        {
            LinkedList<Task> tasks = new LinkedList<Task>();
            
            //                                       subtasks                  transport time
            tasks.AddLast(new Task(new List<int>() { 1, 5, 6 }, new List<int>() { 0, 3, 4 }));
            tasks.AddLast(new Task(new List<int>() { 5, 2, 4 }, new List<int>() { 0, 2, 4 }));
            tasks.AddLast(new Task(new List<int>() { 9, 1, 3 }, new List<int>() { 0, 1, 8 }));

            int actual = Program.CalcCMaxforPermutation(tasks);
            int expected = 28;

            Assert.AreEqual(expected, actual);
        }

        //from: http://stackoverflow.com/questions/3473787/swapping-nodes-on-a-single-linked-list
        public static void SwapWith<T>(LinkedListNode<T> first, LinkedListNode<T> second)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (second == null)
                throw new ArgumentNullException("second");

            T tmp = first.Value;
            first.Value = second.Value;
            second.Value = tmp;
        }

        [TestMethod]
        public void NEHOrderingTest()
        {
            LinkedList<Task> tasks = new LinkedList<Task>();

            //                                       subtasks                  transport time
            tasks.AddLast(new Task(new List<int>() { 1, 5, 6 }, new List<int>() { 0, 3, 4 }));
            tasks.AddLast(new Task(new List<int>() { 5, 2, 4 }, new List<int>() { 0, 2, 4 }));
            tasks.AddLast(new Task(new List<int>() { 9, 1, 3 }, new List<int>() { 0, 1, 8 }));

            //expected value is calculated by checking all possibilities 
            int bestCMax = Program.CalcCMaxforPermutation(tasks);
            for (LinkedListNode<Task> i = tasks.First; i != null; i = i.Next)
            {
                for (LinkedListNode<Task> j = tasks.First; j != null; j = j.Next)
                {
                    SwapWith<Task>(i, j);

                    int currCMax = Program.CalcCMaxforPermutation(tasks);
                    if (currCMax < bestCMax)
                    {
                        bestCMax = currCMax;
                    }
                }
            }
            int expected = bestCMax; 
            //IMPORTANT: ACTUALLY IT CAN BE BETTER SOLUTION THAN NEH ONE, BUT POSSIBILITY FOR THAT TO HAPPEN IS VERY SMALL WHEN NO OF TASK IS SMALL 
            //(SMALLER THAN TO MAKE ERROR BY CALCULATING IT ON MY OWN)

            var NEHSolution = Program.NEHOrdering(tasks);
            int actual = Program.CalcCMaxforPermutation(NEHSolution);

            Assert.AreEqual(expected, actual);
        }
    }
}
