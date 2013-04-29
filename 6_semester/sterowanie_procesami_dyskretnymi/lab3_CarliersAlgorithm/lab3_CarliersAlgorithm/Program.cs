using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

 /* basing on:
 * http://dominik.zelazny.staff.iiar.pwr.wroc.pl/pliki/zad3/Zadanie_3.pdf
 * http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Carlier.pdf
 * http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Schrage.pdf
 */

namespace lab3_CarliersAlgorithm
{
    public partial class Program
    {
        const int EXUCUTIONS_NO = 500;

        static void Main(string[] args)
        {
            Mode choosenMode = ChooseMode();
            string[] dataFilesNames = new string[] { "in50.txt", "in100.txt", "in200.txt" };

            foreach (string fileName in dataFilesNames)
            {
                Task.RestartNo();
                var data = LoadData(fileName);
                TimeSpan timeSum = TimeSpan.Zero;
                KeyValuePair<List<Task>, int> output = new KeyValuePair<List<Task>,int>(new List<Task>(), 0);
                for (int i = 0; i < EXUCUTIONS_NO; i++)
                {
                    DateTime startTime = DateTime.Now;
                    switch (choosenMode)
                    {
                        case Mode.Shrage:
                            output = ShrageOrdering(data);
                            break;

                        case Mode.NoHeapShrage:
                            output = ShrageOrderingWoHeap(data);
                            break;

                        case Mode.PrmtShrage:
                            output = PrmtShrageOrderingNotChagingInput(data);
                            break;

                        case Mode.Calier:
                            output = CaliersOrdering(data);
                            break;

                        default:
                            throw new ApplicationException("wrong mode!");
                    }
                    timeSum += DateTime.Now - startTime;
                }
                Console.WriteLine("\n{0}: C_Max: {1}, calculations time: {2}ms", fileName, output.Value, timeSum.TotalMilliseconds / EXUCUTIONS_NO);
                SaveOrderingToFile(output.Key, fileName.Replace("in", "out"));
            }
            Console.WriteLine("(NOTE: tests are basing on {0} tries)", EXUCUTIONS_NO);
            Console.WriteLine("\n\n\n");
            Main(new string[]{});
        }

    }
}
