using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab4_PSFP_SimulatedAnealing
{
    public class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] //for optimization
        public static int CalcCMaxforPermutation(LinkedList<Task> currList)
        {
            int[] arr = new int[currList.First().subtasks.Count + 1];
            int noOfThingsToDoInEachTaskPlusOne = currList.First().subtasks.Count + 1;

            LinkedListNode<Task> currNode = currList.First;
            while (currNode != null)
            {
                for (int j = 1; j < noOfThingsToDoInEachTaskPlusOne; j++)
                {
                    arr[j] = Math.Max(arr[j - 1] + currNode.Value.transportTimes[j - 1], arr[j]) + currNode.Value.subtasks[j - 1];
                }
                currNode = currNode.Next;
            }

            return arr[currList.First().subtasks.Count];
        }



        private static void InsertTaskToGetLowestCMax(LinkedList<Task> currList, Task taskToInsert)
        {
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

        public static LinkedList<Task> NEHOrderingCMax(LinkedList<Task> inputOrdering)
        {
            LinkedList<Task> inputClone = new LinkedList<Task>(inputOrdering.OrderBy(x => -1 * x.subtasks.Sum()));
            LinkedList<Task> correctPermutation = new LinkedList<Task>();

            correctPermutation.AddLast(inputClone.First()); //add task with biggest sum at first
            inputClone.RemoveFirst();

            while (inputClone.Count > 0)
            {
                InsertTaskToGetLowestCMax(correctPermutation, inputClone.First());
                inputClone.RemoveFirst();
            }

            return correctPermutation;
        }

        public static LinkedList<Task> NEHOrderingCAvg(LinkedList<Task> inputOrdering)
        {
            LinkedList<Task> inputClone = new LinkedList<Task>(inputOrdering.OrderBy(x => -1 * x.subtasks.Sum()));
            LinkedList<Task> correctPermutation = new LinkedList<Task>();

            correctPermutation.AddLast(inputClone.First()); //add task with biggest sum at first
            inputClone.RemoveFirst();

            while (inputClone.Count > 0)
            {
                InsertTaskToGetLowestCAvg(correctPermutation, inputClone.First());
                inputClone.RemoveFirst();
            }

            return correctPermutation;
        }

        private static void InsertTaskToGetLowestCAvg(LinkedList<Task> currList, Task taskToInsert)
        {
            LinkedListNode<Task> bestPosToInsertBefore = null;
            double bestPosCMax = double.MaxValue;
            LinkedListNode<Task> node = currList.First; //DOESNT WORK FOR SOME REASON  - will be much faster
            while (node != null)
            {
                currList.AddBefore(node, taskToInsert);
                double currCMax = CalcCAvgforPermutation(currList);
                if (currCMax < bestPosCMax)
                {
                    bestPosCMax = currCMax;
                    bestPosToInsertBefore = node;
                }
                currList.Remove(taskToInsert);
                node = node.Next;
            }
            currList.AddLast(taskToInsert);
            double currCMax2 = CalcCAvgforPermutation(currList);
            if (currCMax2 >= bestPosCMax)
            {
                currList.RemoveLast();
                currList.AddBefore(bestPosToInsertBefore, taskToInsert);
            }
        }

        private static double CalcCAvgforPermutation(LinkedList<Task> currList)
        {
            int[] arr = new int[currList.First().subtasks.Count + 1];
            int noOfThingsToDoInEachTaskPlusOne = currList.First().subtasks.Count + 1;

            LinkedListNode<Task> currNode = currList.First;
            int endTimesSum = 0;
            while (currNode != null)
            {
                for (int j = 1; j < noOfThingsToDoInEachTaskPlusOne; j++)
                {
                    arr[j] = Math.Max(arr[j - 1] + currNode.Value.transportTimes[j - 1], arr[j]) + currNode.Value.subtasks[j - 1];
                }
                endTimesSum += arr[noOfThingsToDoInEachTaskPlusOne - 1];
                currNode = currNode.Next;
            }

            return (double)endTimesSum / currList.Count; //TODO: it should be double
        }

        static void Main(string[] args)
        {
            var inputTasks = FileOperations.LoadDataFromFiles("pfsp_-_instancje.txt", "pfsp_-_transport.txt");
            int annealingTries = 5;
            
            using(StreamWriter statsStream = new StreamWriter("output.txt"), resultsStream = new StreamWriter("wyniki.txt"))
            {
                statsStream.WriteLine("task no, no of tasks, no of subtasks, input CMax, NEH CMax, Annealing CMax ({0} times)", annealingTries);

                for (int taskNo = 0; taskNo < inputTasks.Count; taskNo++)
                {
                    var NEHOrderedTasks = NEHOrderingCAvg(inputTasks[taskNo]); //MAGIC! :D

                    //TODO: use AVG(C) instead of MAX(C)
                    LinkedList<Task>[] AnnealingNEHOrderedTasks = new LinkedList<Task>[annealingTries];
                    for (int i = 0; i < annealingTries; i++)
                    {
                        AnnealingNEHOrderedTasks[i] = AutoOrderingOptimization.SimulatedAnealing(
                            inputList: NEHOrderedTasks,
                            targetFoo: CalcCAvgforPermutation,
                            mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(0.6d, 0.32d),
                            tempDecreasingSpeed: 0.9998d,
                            startingTemperature: 300.0d + 3*i,
                            iterationsWOChangeToStop: 400 + 3*i
                        );
                    }

                    string outputMsg = String.Format("{0}, {1}, {2}, {3}, {4}",
                        taskNo,
                        inputTasks[taskNo].Count,
                        inputTasks[taskNo].First().subtasks.Count,
                        CalcCAvgforPermutation(inputTasks[taskNo]),
                        CalcCAvgforPermutation(NEHOrderedTasks)
                    );
                    foreach (var item in AnnealingNEHOrderedTasks)
                    {
                        outputMsg += ", " + Convert.ToString(CalcCAvgforPermutation(item));
                    }

                    statsStream.WriteLine(outputMsg);
                    statsStream.Flush();
                    Console.WriteLine(outputMsg);

                    FileOperations.WritePermutationToStream(NEHOrderedTasks, resultsStream);
                }

            }

            Console.ReadKey();
        }
    }
}
