using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace lab2_NEH
{
    class Program
    {
        class Task
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

        static void InsertTaskToGetLowestCMax(LinkedList<Task> currList, Task taskToInsert)
        {
            int[,] arr = new int[currList.Count + 1, taskToInsert.subtasks.Count + 1];

            LinkedListNode<Task> bestPosToInsertBefore = null;
            int bestPosCMax = int.MaxValue;

            foreach(Task task in currList)
            //Parallel.ForEach(currList, task =>
            {
                LinkedList<Task> tempList = new LinkedList<Task>(currList); //linked list copying in here - low performance action
                LinkedListNode<Task> nodeToInsertBefore = tempList.Find(task); //linear complexity - bad!!!
                tempList.AddBefore(nodeToInsertBefore, taskToInsert);
                int currCMax = CalcCMaxforPermutation(tempList);
                if (currCMax < bestPosCMax)
                {
                    bestPosCMax = currCMax;
                    bestPosToInsertBefore = nodeToInsertBefore;
                }
            }//);
            LinkedList<Task> tempList2 = new LinkedList<Task>(currList); //refactor this "2" shit
            LinkedListNode<Task> nodeToInsertAfter = tempList2.Last;
            tempList2.AddAfter(nodeToInsertAfter, taskToInsert);
            int currCMax2 = CalcCMaxforPermutation(tempList2);
            if (currCMax2 < bestPosCMax)
            {
                currList.AddAfter(currList.Find(nodeToInsertAfter.Value), taskToInsert);
            }
            else
            {
                currList.AddBefore(currList.Find(bestPosToInsertBefore.Value), taskToInsert);
            }
        }

        /// <summary>
        /// create arr and return last elem (CMax)
        /// </summary>
        /// <param name="currList"></param>
        /// <returns></returns>
        private static int CalcCMaxforPermutation(LinkedList<Task> currList)
        {
            int noOfThingsToDoInEachTask = currList.First().subtasks.Count;
            int[,] arr = new int[currList.Count + 1, noOfThingsToDoInEachTask + 1];

            int i = 1;
            int noOfThingsToDoInEachTaskPlus1 = noOfThingsToDoInEachTask + 1; //to improve performance
            LinkedListNode<Task> currTask = currList.First;
            while(currTask != null)
            {
                for (int j = 1; j < noOfThingsToDoInEachTaskPlus1; j++)
                {
                    arr[i, j] = Math.Max(arr[i - 1, j], arr[i, j - 1]) + currTask.Value.subtasks[j - 1];
                }
                
                i++;
                currTask = currTask.Next;
            }

            return arr[i - 1, noOfThingsToDoInEachTask];
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
            int jobNo = 1;
            DateTime startTime = DateTime.Now;
            foreach (LinkedList<Task> tasks in jobs)
            {
                LinkedList<Task> orderedTasks = new LinkedList<Task>(tasks.OrderBy(x => -1 * x.subtasks.Sum()));
                //powinno wyjsc 1/4/3/2 <- permutacja //wychodzi, że jednak 1/3/4/2 jest lepsze(CMax = 18 vs 20)...

                LinkedList<Task> correctPermutation = new LinkedList<Task>();
                correctPermutation.AddLast(orderedTasks.First()); //add task with biggest sum at first
                orderedTasks.RemoveFirst();

                while (orderedTasks.Count > 0)
                {
                    InsertTaskToGetLowestCMax(correctPermutation, orderedTasks.First());
                    orderedTasks.RemoveFirst();
                }

                Console.WriteLine("JOB NO: {0}", jobNo++);
                Console.WriteLine("Time elapsd: {0}", DateTime.Now - startTime);
                Console.WriteLine("C_Max = {0}", CalcCMaxforPermutation(correctPermutation));
                Console.WriteLine();
                foreach (Task task in correctPermutation)
                {
                    Console.Write(task.taskNo + " ");
                }
                Console.WriteLine("\n");

            }
            Console.ReadKey();
            //LinkedList<Task> tasks = new LinkedList<Task>();

            //tasks.AddLast(new Task(new List<int>() { 2, 1, 3 }));
            //tasks.AddLast(new Task(new List<int>() { 4, 1, 5 }));
            //tasks.AddLast(new Task(new List<int>() { 2, 3, 2 }));
            //tasks.AddLast(new Task(new List<int>() { 1, 4, 4 }));

        }
    }
}
