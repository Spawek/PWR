using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab3_CarliersAlgorithm
{
    public partial class Program
    {
        public static List<Task> LoadData(string dataFileName)
        {
            List<Task> task;
            using (StreamReader sr = new StreamReader(dataFileName))
            {
                string line = sr.ReadLine();
                task = new List<Task>(Convert.ToInt32(line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[0]));
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splittedLine = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    task.Add(new Task(
                        Convert.ToInt32(splittedLine[0]),
                        Convert.ToInt32(splittedLine[1]),
                        Convert.ToInt32(splittedLine[2])
                    ));
                }
            }

            return task;
        }

        static void SaveOrderingToFile(List<Task> ordering, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var item in ordering)
                {
                    sw.Write("{0} ", item.no);
                }
            }
        }

        public enum Mode
        {
            Shrage, NoHeapShrage, PrmtShrage, Calier
        }

        static Mode ChooseMode()
        {
            Console.WriteLine("Choose your mode:\n\t1 - Shrage\n\t2 - No Heap Shrage\n\t3 - PrmtShrage\n\t4 - Calier\n");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    return Mode.Shrage;
                    
                case ConsoleKey.D2:
                    return Mode.NoHeapShrage;

                case ConsoleKey.D3:
                    return Mode.PrmtShrage;

                case ConsoleKey.D4:
                    return Mode.Calier;

                default:
                    return ChooseMode();
            }
        }
    }
}
