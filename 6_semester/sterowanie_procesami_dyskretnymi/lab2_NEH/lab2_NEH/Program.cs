using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab2_NEH
{
    public class Task
    {
        public List<int> subtasks;
        public int taskNo;
        public static int maxNo = 0;

        public Task(List<int> _subtasks)
        {
            subtasks = _subtasks;

            taskNo = ++maxNo;
        }

        public Task()
        {
            subtasks = new List<int>();

            taskNo = ++maxNo;
        }

        public static void ResetTaskCounter()
        {
            maxNo = 0;
        }
    }

    public class Program
    {
        static void InsertTaskToGetLowestCMaxAccelerated(LinkedList<Task> currList, Task taskToInsert)
        {
            int[,] E = CalcEArr(currList);
            int[,] Q = CalcQArr(currList);
            int[,] F = CalcFArr(E, taskToInsert);
            int[,] FPlusQ = AddArrays(F, Q);
            int minCol = FindMinColInFPlusQArr(FPlusQ);

            if (minCol == currList.Count)
            {
                currList.AddLast(taskToInsert);
            }
            else
            {
                currList.AddBefore(currList.Find(currList.ElementAt(minCol)), taskToInsert); //could be done faster but its not important
            }
        }

        public static int FindMinColInFPlusQArr(int[,] FPlusQ)
        {
            int minVal = int.MaxValue;
            int minCol = 0;
            for (int i = 0; i < FPlusQ.GetLength(0); i++)
            {
                int maxValInCol = int.MinValue;
                for (int j = 0; j < FPlusQ.GetLength(1); j++)
                {
                    maxValInCol = FPlusQ[i, j] > maxValInCol ? FPlusQ[i, j] : maxValInCol;
                }
                if (maxValInCol < minVal)
                {
                    minVal = maxValInCol;
                    minCol = i;
                }
            }

            return minCol;
        }

        public static int[,] AddArrays(int[,] F, int[,] Q)
        {
            int[,] FPlusQ = new int[F.GetLength(0), F.GetLength(1)];
            for (int i = 0; i < F.GetLength(0); i++)
			{
                for (int j = 0; j < F.GetLength(1)-1; j++)
                {
                   FPlusQ[i,j] = F[i,j+1] + Q[i,j];
                }
			}

            return FPlusQ;
        }

        public static int[,] CalcFArr(int[,] E, Task taskToInsert)
        {
            int[,] F = new int[E.GetLength(0), E.GetLength(1)];
            for (int i = 0; i < E.GetLength(0); i++)
			{
                for (int j = 1; j < E.GetLength(1); j++)
			    {
                    F[i, j] = Math.Max(F[i, j - 1], E[i, j]) + taskToInsert.subtasks[j - 1];
	    		}
			}

            return F;
        }

        public static int[,] CalcEArr(LinkedList<Task> currList)
        {
            int tasksNo = currList.Count;
            int subtasksNo = currList.First().subtasks.Count;

            //calc E arr
            int[,] E = new int[tasksNo + 1, subtasksNo + 1];
            var currTask = currList.First;
            for (int i = 1; i < tasksNo + 1; i++, currTask = currTask.Next)
            {
                for (int j = 1; j < subtasksNo + 1; j++)
                {
                    E[i, j] = Math.Max(E[i - 1, j], E[i, j - 1]) + currTask.Value.subtasks[j - 1];
                }
            }

            return E;
        }

        public static int[,] CalcQArr(LinkedList<Task> currList)
        {
            int tasksNo = currList.Count;
            int subtasksNo = currList.First().subtasks.Count;

            int[,] Q = new int[tasksNo + 1, subtasksNo + 1];
            var currTask = currList.Last;
            for (int i = tasksNo - 1; i >= 0; i--, currTask = currTask.Previous)
            {
                for (int j = subtasksNo - 1; j >= 0; j--)
                {
                    Q[i, j] = Math.Max(Q[i + 1, j], Q[i, j + 1]) + currTask.Value.subtasks[j];
                }
            }

            return Q;
        }

        static void InsertTaskToGetLowestCMax(LinkedList<Task> currList, Task taskToInsert)
        {
            int[,] arr = new int[currList.Count + 1, taskToInsert.subtasks.Count + 1];

            LinkedListNode<Task> bestPosToInsertBefore = null;
            int bestPosCMax = int.MaxValue;
            LinkedListNode<Task> node = currList.First; //DOESNT WORK FOR SOME REASON  - will be much faster
            while (node != null)
            {
                currList.AddBefore(node, taskToInsert);
                int currCMax = CalcCMaxforPermutation(currList);
                if (currCMax < bestPosCMax)
                {
                    bestPosCMax = currCMax;
                    bestPosToInsertBefore = node;
                }
                currList.Remove(taskToInsert);
                node = node.Next;
            }
            currList.AddLast(taskToInsert);
            int currCMax2 = CalcCMaxforPermutation(currList);
            if (currCMax2 >= bestPosCMax)
            {
                currList.RemoveLast();
                currList.AddBefore(bestPosToInsertBefore, taskToInsert);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] //for optimization
        public static int CalcCMaxforPermutation(LinkedList<Task> currList)
        {
            int[] arr = new int[currList.First().subtasks.Count + 1];
            int noOfThingsToDoInEachTaskPlusOne = currList.First().subtasks.Count + 1;

            LinkedListNode<Task> currNode = currList.First;
            while(currNode != null)
            {
                for (int j = 1; j < noOfThingsToDoInEachTaskPlusOne; j++)
                {
                    arr[j] = Math.Max(arr[j - 1], arr[j]) + currNode.Value.subtasks[j - 1];
                }
                currNode = currNode.Next;
            }

            return arr[currList.First().subtasks.Count];
        }

        static List<LinkedList<Task>> LoadDataFromFile(string fileName)
        {
            List<LinkedList<Task>> output = new List<LinkedList<Task>>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if(line.Contains("ta")) continue;
                    if (line.Split(new char[] { ' ' }).Length == 2)
                    {
                        output.Add(new LinkedList<Task>());
                        Task.ResetTaskCounter();
                        continue;
                    }

                    string[] splittedLine = line.Split(new string[]{" ", "\t"}, StringSplitOptions.RemoveEmptyEntries);
                    if(splittedLine.Length > 0) output[output.Count - 1].AddLast(new Task());
                    for (int i = 1; i < splittedLine.Length; i = i+2)
			        {
                        output[output.Count - 1].Last().subtasks.Add(Convert.ToInt32(splittedLine[i]));
			        }
                }
            }

            return output;
        }

        private enum Mode
        {
            standard, accelerated, unknown
        }

        private static Mode getModeFromUser()
        {
            Console.WriteLine("SELECT MODE:");
            Console.WriteLine("\t1 = standard NEH mode");
            Console.WriteLine("\t2 = accelerated NEH mode");
            Console.WriteLine();

            Mode mode = Mode.unknown;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    mode = Mode.standard;
                    break;

                case ConsoleKey.D2:
                    mode = Mode.accelerated;
                    break;

                default:
                    mode = Mode.unknown;
                    break;
            }

            if (mode == Mode.unknown)
            {
                Console.WriteLine("Wrong mode!\n");
                return getModeFromUser();
            }
            return mode;
        }
        static void Main(string[] args)
        {
            Mode mode = getModeFromUser();

            List<LinkedList<Task>> jobs = LoadDataFromFile("bench_fs.txt");
            List<List<int>> permutations = new List<List<int>>(jobs.Count);
            for (int i = 0; i < jobs.Count; i++)
            {
                permutations.Add(new List<int>(jobs[i].Count));
            }

            DateTime startTime = DateTime.Now;

            //map subtasks -> work time
            Dictionary<int, List<TimeSpan>> statistics = new Dictionary<int, List<TimeSpan>>();
            Object statsLock = new object();

            //Parallel.For(0, jobs.Count, i =>            //PARALLEL MODE
            for (int i = 0; i < jobs.Count; i++)      //SINGLE THREAD MODE
            {
                DateTime jobStartTIme = DateTime.Now;
                LinkedList<Task> orderedTasks = new LinkedList<Task>(jobs[i].OrderBy(x => -1 * x.subtasks.Sum()));

                LinkedList<Task> correctPermutation = new LinkedList<Task>();
                correctPermutation.AddLast(orderedTasks.First()); //add task with biggest sum at first
                orderedTasks.RemoveFirst();

                while (orderedTasks.Count > 0)
                {
                    if (mode == Mode.standard)InsertTaskToGetLowestCMax(correctPermutation, orderedTasks.First());
                    else if (mode == Mode.accelerated)InsertTaskToGetLowestCMaxAccelerated(correctPermutation, orderedTasks.First());
                    else throw new ApplicationException("UNKNOWN MODE");

                    orderedTasks.RemoveFirst();
                }

                //stats saving
                lock (statsLock)
                {
                    if (!statistics.ContainsKey(jobs[i].Count))
                    {
                        statistics.Add(jobs[i].Count, new List<TimeSpan>());
                    }
                    statistics[jobs[i].Count].Add(DateTime.Now - jobStartTIme);
                }

                Console.WriteLine("JOB NO: {0}", i + 1);
                Console.WriteLine("SUBTASKS NO: {0}", jobs[i].Count);
                Console.WriteLine("STAGES NO: {0}", jobs[i].First().subtasks.Count);
                Console.WriteLine("THIS TASK TIME: {0}", DateTime.Now - jobStartTIme);
                Console.WriteLine("Time elapsd: {0}", DateTime.Now - startTime);
                Console.WriteLine("C_Max = {0}", CalcCMaxforPermutation(correctPermutation));
                Console.WriteLine();

                foreach (Task task in correctPermutation)
                {   
                    //Console.Write(task.taskNo + " ");
                    permutations[i].Add(task.taskNo);
                }
                Console.WriteLine("\n");

            }//);

            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                foreach (var item in permutations)
                {
                    foreach (var no in item)
                    {
                        sw.Write(" " + no);
                    }
                    sw.WriteLine();
                }
            }

            Console.WriteLine("\n\n\nTIME STATISTICS:");
            Console.WriteLine("SUBTASKS  \t\tTIME [MS]");
            foreach (int key in statistics.Keys)
            {
                Console.WriteLine("   {0}\t\t--->      {1}", key, statistics[key].Average(x => x.Milliseconds));
            }
            Console.WriteLine("\n\nTOTAL TIME: {0}", DateTime.Now - startTime);


            Console.WriteLine("Click any key to exit...");
            Console.ReadKey();
        }
    }

    //TESTS PASTED HERE JUST TO USE ONLY 1 FILE
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
            int[,] actual = Program.CalcEArr(taskList);
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
            int[,] expectedArr = new int[4, 6] { { 0, 4, 12, 20, 27, 29 }, { 0, 13, 21, 29, 36, 38 }, { 0, 18, 31, 39, 48, 50 }, { 0, 27, 35, 49, 56, 58 } };

            CollectionAssert.AreEqual(expectedArr, actual);
        }

        [TestMethod]
        public void FPlusQCreationTest()
        {
            var actual = Program.AddArrays(
                Program.CalcFArr(Program.CalcEArr(taskList), taskToInsert),
                Program.CalcQArr(taskList)
            );

            int[,] expectedArr = new int[4, 6] { { 54, 52, 52, 54, 44, 0 }, { 54, 57, 56, 55, 47, 0 }, { 49, 53, 58, 57, 58, 0 }, { 27, 35, 49, 56, 58, 0 } };

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
