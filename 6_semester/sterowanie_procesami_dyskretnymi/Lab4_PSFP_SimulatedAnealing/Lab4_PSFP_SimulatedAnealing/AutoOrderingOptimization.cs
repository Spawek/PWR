using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4_PSFP_SimulatedAnealing
{
    public class AutoOrderingOptimization
    {
        //from: http://stackoverflow.com/questions/3473787/swapping-nodes-on-a-single-linked-list
        private static void Swap<T>(LinkedListNode<T> first, LinkedListNode<T> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            T tmp = first.Value;
            first.Value = second.Value;
            second.Value = tmp;
        }

        Func<double, double, double> BoltzmanDistrubution = ((x, y) => x + y);

        private static double BoltzmanDistributionAtPoint(double energyChange, double temperature)
        {
            const double BOLTZMAN_CONSTANT = 1;// 1.380648813E-23;

            return Math.Exp(energyChange / (BOLTZMAN_CONSTANT * temperature));
        }

        private static Random rand = new Random();


        /// <summary>
        /// ensures: node1 < node2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        private static void Get2RandomNodes<T>(LinkedList<T> list, out LinkedListNode<T> node1, out LinkedListNode<T> node2)
        {
            int index1 = rand.Next(list.Count-1);
            int index2 = -1;
            while (index1 >= index2)
            {
                index2 = rand.Next(list.Count);
            }

            node1 = list.First;
            for (int i = 0; i < index1; i++)
            {
                node1 = node1.Next;
            }

            node2 = list.First;
            for (int i = 0; i < index2; i++)
            {
                node2 = node2.Next;
            }
        }

        private static void RandomlySwapElements<T>(LinkedList<T> list)
        {
            LinkedListNode<T> node1;
            LinkedListNode<T> node2;
            Get2RandomNodes<T>(list, out node1, out node2);

            Swap(node1, node2);
        }

        private static void RandomlyReinsertElement<T>(LinkedList<T> list)
        {
            LinkedListNode<T> node1;
            LinkedListNode<T> node2;
            Get2RandomNodes<T>(list, out node1, out node2);

            T node1Val = node1.Value;
            list.Remove(node1);
            list.AddAfter(node2, node1Val);
        }

        /// <summary>
        /// it actually doesn't reverse order but makes strange permutation of block (so its even better)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private static void ReverseRandomBlock<T>(LinkedList<T> list)
        {
            LinkedListNode<T> node1;
            LinkedListNode<T> node2;
            Get2RandomNodes<T>(list, out node1, out node2);

            int next = 0;

            while (node1 != null && node2 != null && node1.Next != node2 && node2.Next != node1)
            {
                Swap(node1, node2);
                if (next % 2 == 0)
                {
                    node2 = node2.Next;
                }
                else
                {
                    node1 = node1.Next;
                }

                next = (next + 1) % 2;
            }
        }


        /// <summary>
        /// reverseBlockChange = 1 - swapChange - insertChance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="swapChance"></param>
        /// <param name="insertChance"></param>
        /// <returns></returns>
        public static Action<LinkedList<T>> MutatorFooGenerator<T>
            (double swapChance, double insertChance)
        {
            return x => 
            {
                if (insertChance + swapChance > 1.0d ||
                    insertChance < 0.0d ||
                    swapChance < 0.0d)
                {
                    throw new ArgumentException();
                }

                Random rand = new Random();
                double randomInRangeOf0To1 = rand.NextDouble();
                
                if(randomInRangeOf0To1 < swapChance)
                {
                    RandomlySwapElements(x);
                }
                else if(randomInRangeOf0To1 < swapChance + insertChance)
                {
                    RandomlyReinsertElement(x);
                }
                else
                {
                    ReverseRandomBlock(x);
                }
            };
        }

        public static LinkedList<T> SimulatedAnealing<T>
            (LinkedList<T> inputList, 
            Func<LinkedList<T>, double> targetFoo,
            Action<LinkedList<T>> mutatorFoo,
            double tempDecreasingSpeed, double startingTemperature,
            int iterationsWOChangeToStop)
        {
            double temperature = startingTemperature;
            int iterationsWoChange = 0;

            LinkedList<T> actualList = new LinkedList<T>(inputList);
            LinkedList<T> bestFoundOrder = new LinkedList<T>(inputList);
            double bestOrderTarget = targetFoo(bestFoundOrder);

            while (iterationsWoChange < iterationsWOChangeToStop)
            {
                LinkedList<T> listToMutate = new LinkedList<T>(actualList);
                mutatorFoo(listToMutate);

                double preMutateTarget = targetFoo(actualList);
                double postMutateTarget = targetFoo(listToMutate);
                double targetChange = preMutateTarget - postMutateTarget;

                double a;
                if (rand.NextDouble() < (a = BoltzmanDistributionAtPoint(targetChange, temperature)))
                {
                    actualList = listToMutate;

                    if (targetChange != 0) //when temp is low there are a lot of changes which are not chaning anything
                    {
                        iterationsWoChange = 0;
                    }

                    double actualTarget = targetFoo(actualList);
                    if (actualTarget < bestOrderTarget)
                    {
                        bestOrderTarget = actualTarget;
                        bestFoundOrder = new LinkedList<T>(actualList);
                    }
                }
                else
                {
                    iterationsWoChange++;
                }
                temperature *= tempDecreasingSpeed;
                //Console.WriteLine("TEMP: {0}, Change chance: {1}, Iterations WO change: {2}", temperature, a, iterationsWoChange);
            }

            return bestFoundOrder;
        }


    }
}
