﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab4_PSFP_SimulatedAnealing;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private LinkedList<Task> tasks = new LinkedList<Task>();

        [TestInitialize]
        public void TestInit()
        {
            //                                       subtasks                  transport time
            tasks.AddLast(new Task(new List<int>() { 1, 5, 6 }, new List<int>() { 0, 3, 4 }));
            tasks.AddLast(new Task(new List<int>() { 5, 2, 4 }, new List<int>() { 0, 2, 4 }));
            tasks.AddLast(new Task(new List<int>() { 9, 1, 3 }, new List<int>() { 0, 1, 8 }));
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

        private int BruteforceCalcBestPermutationCMax(LinkedList<Task> inputTasks)
        {
            int bestCMax = int.MaxValue;
            for (LinkedListNode<Task> i = inputTasks.First; i != null; i = i.Next)
            {
                for (LinkedListNode<Task> j = inputTasks.First; j != null; j = j.Next)
                {
                    SwapWith<Task>(i, j);

                    int currCMax = Program.CalcCMaxforPermutation(inputTasks);
                    if (currCMax < bestCMax)
                    {
                        bestCMax = currCMax;
                    }
                }
            }

            return bestCMax;
        }

        [TestMethod]
        public void CalcCMaxforPermutationTest()
        {
            int actual = Program.CalcCMaxforPermutation(tasks);
            int expected = 28;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NEHOrderingTest()
        {
            int expected = BruteforceCalcBestPermutationCMax(tasks); 
            //IMPORTANT: ACTUALLY IT CAN BE BETTER SOLUTION THAN NEH ONE, BUT POSSIBILITY FOR THAT TO HAPPEN IS VERY SMALL WHEN NO OF TASK IS SMALL 
            //(SMALLER THAN TO MAKE ERROR BY CALCULATING IT ON MY OWN)

            var NEHSolution = Program.NEHOrderingCMax(tasks);
            int actual = Program.CalcCMaxforPermutation(NEHSolution);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SimulatedAnnealingSwapTest()
        {
            int expected = BruteforceCalcBestPermutationCMax(tasks);
            int actual = int.MaxValue;
            int delta = 1;

            for (int i = 0; i < 20; i++)
            {
                var AnnealingSolution = AutoOrderingOptimization.SimulatedAnealing(
                inputList: tasks,
                targetFoo: Program.CalcCMaxforPermutation,
                mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(1.0d, 0.0d), //swap only
                tempDecreasingSpeed: 0.97d,
                startingTemperature: 10000.0d,
                iterationsWOChangeToStop: 40);
                actual = Math.Min(actual, Program.CalcCMaxforPermutation(AnnealingSolution));
            }

            Assert.AreEqual(expected, actual, delta);
        }


        [TestMethod]
        public void SimulatedAnnealingInsertTest()
        {
            int expected = BruteforceCalcBestPermutationCMax(tasks);
            int actual = int.MaxValue;
            int delta = 1;

            for (int i = 0; i < 20; i++)
            {
                var AnnealingSolution = AutoOrderingOptimization.SimulatedAnealing(
                inputList: tasks,
                targetFoo: Program.CalcCMaxforPermutation,
                mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(0.0d, 1.0d), //insert only
                tempDecreasingSpeed: 0.97d,
                startingTemperature: 10000.0d,
                iterationsWOChangeToStop: 40);
                actual = Math.Min(actual, Program.CalcCMaxforPermutation(AnnealingSolution));

            }

            Assert.AreEqual(expected, actual, delta);
        }

        [TestMethod]
        public void SimulatedAnnealingChamgeOrredInBlockTest()
        {
            int expected = BruteforceCalcBestPermutationCMax(tasks);
            int actual = int.MaxValue;
            int delta = 1;
            for (int i = 0; i < 20; i++)
            {
                var AnnealingSolution = AutoOrderingOptimization.SimulatedAnealing(
                inputList: tasks,
                targetFoo: Program.CalcCMaxforPermutation,
                mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(0.0d, 0.0d), //change order in block only
                tempDecreasingSpeed: 0.97d,
                startingTemperature: 10000.0d,
                iterationsWOChangeToStop: 40);
                actual = Math.Min(actual, Program.CalcCMaxforPermutation(AnnealingSolution));

            }

            Assert.AreEqual(expected, actual, delta);
        }

        [TestMethod]
        public void SimulatedAnnealinMixedMutationTest()
        {
            int expected = BruteforceCalcBestPermutationCMax(tasks);
            int actual = int.MaxValue;
            int delta = 1;
            for (int i = 0; i < 20; i++)
            {
                var AnnealingSolution = AutoOrderingOptimization.SimulatedAnealing(
                inputList: tasks,
                targetFoo: Program.CalcCMaxforPermutation,
                mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(0.5d, 0.45d), //change order in block only
                tempDecreasingSpeed: 0.97d,
                startingTemperature: 10000.0d,
                iterationsWOChangeToStop: 40);
                actual = Math.Min(actual, Program.CalcCMaxforPermutation(AnnealingSolution));

            }

            Assert.AreEqual(expected, actual, delta);
        }
    }
}
