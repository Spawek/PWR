using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public static LinkedList<Task> NEHOrdering(LinkedList<Task> inputOrdering)
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



        static void Main(string[] args)
        {
            var inputTasks = FileOperations.LoadDataFromFiles("pfsp_-_instancje.txt", "pfsp_-_transport.txt");

            int choosenTask = 42; //coz its the answer to the universe!
            var NEHOrderedTasks = NEHOrdering(inputTasks[choosenTask]); //MAGIC! :D

            //TODO: use AVG(C) instead of MAX(C)
            AutoOrderingOptimization.SimulatedAnealing(
                inputList: inputTasks[choosenTask], 
                targetFoo: CalcCMaxforPermutation, 
                mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(0.6d, 0.37d), 
                tempDecreasingSpeed: 0.99d, 
                startingTemperature: 500.0d, 
                iterationsWOChangeToStop: 100
            );

            Console.WriteLine("NEH: {0}", CalcCMaxforPermutation(NEHOrderedTasks));
            Console.WriteLine("Annealing: {0}", CalcCMaxforPermutation(NEHOrderedTasks));

            Console.ReadKey();
        }
    }
}
