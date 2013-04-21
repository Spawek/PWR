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
        static void Main(string[] args)
        {
            Mode choosenMode = ChooseMode();
            string[] dataFilesNames = new string[] { "in50.txt", "in100.txt", "in200.txt" };

            foreach (string fileName in dataFilesNames)
            {
                Task.RestartNo();
                var data = LoadData(fileName);
                KeyValuePair<List<Task>, int> output;
                DateTime startTime = DateTime.Now;
                switch (choosenMode)
                {
                    case Mode.Shrage:
                        output = ShrageOrdering(data);
                        break;

                    case Mode.PrmtShrage:
                        output = PrmtShrageOrdering(data);
                        break;

                    case Mode.Calier:
                        output = CaliersOrdering(data);
                        break;

                    default:
                        throw new ApplicationException("wrong mode!");
                }
                Console.WriteLine("\n{0}: C_Max: {1}, calculations time: {2}", fileName, output.Value, DateTime.Now - startTime);
                SaveOrderingToFile(output.Key, fileName.Replace("in", "out"));
            }

            Console.ReadKey();
        }

    }
}
