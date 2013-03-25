﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;

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

            //LinkedListNode<Task> bestPosToInsertBefore = null;
            //int bestPosCMax = int.MaxValue;
            //foreach (Task task in currList)
            //{
            //    LinkedList<Task> tempList = new LinkedList<Task>(currList); //linked list copying in here - low performance action //doesnt look so bad after performance analysis
            //    LinkedListNode<Task> nodeToInsertBefore = tempList.Find(task); //linear complexity - bad!!!
            //    tempList.AddBefore(nodeToInsertBefore, taskToInsert);
            //    int currCMax = CalcCMaxforPermutation(tempList);
            //    if (currCMax < bestPosCMax)
            //    {
            //        bestPosCMax = currCMax;
            //        bestPosToInsertBefore = nodeToInsertBefore;
            //    }
            //}
            //LinkedList<Task> tempList2 = new LinkedList<Task>(currList); //refactor this "2" shit
            //LinkedListNode<Task> nodeToInsertAfter = tempList2.Last;
            //tempList2.AddAfter(nodeToInsertAfter, taskToInsert);
            //int currCMax2 = CalcCMaxforPermutation(tempList2);
            //if (currCMax2 < bestPosCMax)
            //{
            //    currList.AddAfter(currList.Find(nodeToInsertAfter.Value), taskToInsert);
            //}
            //else
            //{
            //    currList.AddBefore(currList.Find(bestPosToInsertBefore.Value), taskToInsert);
            //}



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

        /// <summary>
        /// create arr and return last elem (CMax)
        /// </summary>
        /// <param name="currList"></param>
        /// <returns></returns>
        //private static int CalcCMaxforPermutation(LinkedList<Task> currList)
        //{
        //    int noOfThingsToDoInEachTask = currList.First().subtasks.Count;
        //    int[,] arr = new int[currList.Count + 1, noOfThingsToDoInEachTask + 1];

        //    int i = 1;
        //    int noOfThingsToDoInEachTaskPlus1 = noOfThingsToDoInEachTask + 1; //to improve performance
        //    LinkedListNode<Task> currTask = currList.First;
        //    while(currTask != null)
        //    {
        //        for (int j = 1; j < noOfThingsToDoInEachTaskPlus1; j++)
        //        {
        //            arr[i, j] = Math.Max(arr[i - 1, j], arr[i, j - 1]) + currTask.Value.subtasks[j - 1];
        //        }
                
        //        i++;
        //        currTask = currTask.Next;
        //    }

        //    return arr[i - 1, noOfThingsToDoInEachTask];
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        static void Main(string[] args)
        {
            List<LinkedList<Task>> jobs = LoadDataFromFile("bench_fs.txt");
            List<List<int>> permutations = new List<List<int>>(jobs.Count);
            for (int i = 0; i < jobs.Count; i++)
            {
                permutations.Add(new List<int>(jobs[i].Count));
            }

            DateTime startTime = DateTime.Now;
            

            Parallel.For(0, jobs.Count, i =>
            {
                //LinkedList<Task> orderedTasks = new LinkedList<Task>(tasks.OrderBy(x => -1 * x.subtasks.Sum()));
                LinkedList<Task> orderedTasks = new LinkedList<Task>(jobs[i].OrderBy(x => -1 * x.subtasks.Sum()));

                LinkedList<Task> correctPermutation = new LinkedList<Task>();
                correctPermutation.AddLast(orderedTasks.First()); //add task with biggest sum at first
                orderedTasks.RemoveFirst();

                while (orderedTasks.Count > 0)
                {
                    //InsertTaskToGetLowestCMax(correctPermutation, orderedTasks.First());
                    InsertTaskToGetLowestCMaxAccelerated(correctPermutation, orderedTasks.First());
                    orderedTasks.RemoveFirst();
                }

                Console.WriteLine("JOB NO: {0}", i + 1);
                Console.WriteLine("Time elapsd: {0}", DateTime.Now - startTime);
                //Console.WriteLine("C_Max = {0}", CalcCMaxforPermutation(correctPermutation));
                Console.WriteLine();
                foreach (Task task in correctPermutation)
                {
                    //Console.Write(task.taskNo + " ");
                    permutations[i].Add(task.taskNo);
                }
                Console.WriteLine("\n");

            });

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

            Console.WriteLine("Click any key to exit...");
            Console.ReadKey();
        }
    }
}
