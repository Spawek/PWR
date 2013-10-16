using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_PSFP_SimulatedAnealing
{
    public class NEHOrdering
    {
        public static double CalcCMaxforPermutation(LinkedList<Task> currList)
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
            double bestPosCMax = int.MaxValue;
            LinkedListNode<Task> node = currList.First; 
            while (node != null)
            {
                currList.AddBefore(node, taskToInsert);
                double currCMax = CalcCMaxforPermutation(currList);
                if (currCMax < bestPosCMax)
                {
                    bestPosCMax = currCMax;
                    bestPosToInsertBefore = node;
                }
                currList.Remove(taskToInsert);
                node = node.Next;
            }
            currList.AddLast(taskToInsert);
            double currCMax2 = CalcCMaxforPermutation(currList);
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
            LinkedListNode<Task> node = currList.First;
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

        public static double CalcCAvgforPermutation(LinkedList<Task> currList)
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
    }
}
