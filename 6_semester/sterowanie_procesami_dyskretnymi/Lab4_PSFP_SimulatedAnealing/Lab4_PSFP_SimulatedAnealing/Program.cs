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
        static void Main(string[] args)
        {
            var inputTasks = FileOperations.LoadDataFromFiles("pfsp_-_instancje.txt", "pfsp_-_transport.txt");
            int annealingTries = 5;
            
            using(StreamWriter statsStream = new StreamWriter("output.txt"), resultsStream = new StreamWriter("wyniki.txt"))
            {
                statsStream.WriteLine("task no, no of tasks, no of subtasks, input CMax, NEH CMax, Annealing CMax ({0} times)", annealingTries);

                for (int taskNo = 0; taskNo < inputTasks.Count; taskNo++)
                {
                    var NEHOrderedTasks = NEHOrdering.NEHOrderingCAvg(inputTasks[taskNo]);

                    LinkedList<Task>[] AnnealingNEHOrderedTasks = new LinkedList<Task>[annealingTries];
                    for (int i = 0; i < annealingTries; i++)
                    {
                        AnnealingNEHOrderedTasks[i] = AutoOrderingOptimization.SimulatedAnealing(
                            inputList: NEHOrderedTasks,
                            targetFoo: NEHOrdering.CalcCAvgforPermutation,
                            mutatorFoo: AutoOrderingOptimization.MutatorFooGenerator<Task>(0.6d, 0.32d),
                            tempDecreasingSpeed: 0.9998d,
                            startingTemperature: 300.0d + 3*i,
                            iterationsWOChangeToStop: 400 + 3*i
                        );
                    }

                    string outputMsg = String.Format("{0}. {1}. {2}. {3}. {4}",
                        taskNo,
                        inputTasks[taskNo].Count,
                        inputTasks[taskNo].First().subtasks.Count,
                        NEHOrdering.CalcCAvgforPermutation(inputTasks[taskNo]),
                        NEHOrdering.CalcCAvgforPermutation(NEHOrderedTasks)
                    );
                    foreach (var item in AnnealingNEHOrderedTasks)
                    {
                        outputMsg += ". " + Convert.ToString(NEHOrdering.CalcCAvgforPermutation(item));
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
