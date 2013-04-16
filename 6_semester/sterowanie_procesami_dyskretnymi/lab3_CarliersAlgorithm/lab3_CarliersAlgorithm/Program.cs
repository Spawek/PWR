using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

 /* basing on:
 * http://dominik.zelazny.staff.iiar.pwr.wroc.pl/pliki/zad3/Zadanie_3.pdf
 * http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Carlier.pdf
 * http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Schrage.pdf
 */

/*
 * note for readers:
 * I don't rly want to create all project in 1 file, 
 * but I'm obligated to send this code in a strange
 * way to my prof, and it's easier to do it in this way
 */

namespace lab3_CarliersAlgorithm
{
    public class Task
    {
        public int R;
        public int P;
        public int Q;
        public int no;

        private static int nextNo = 1;

        public Task(int _R, int _P, int _Q)
        {
            R = _R;
            P = _P;
            Q = _Q;
            no = nextNo++;
        }

        public void resetNextNo()
        {
            nextNo = 1;
        }
    }

    public class Program
    {
        public static List<Task> LoadData(string dataFileName)
        {
            List<Task> task;
            using(StreamReader sr = new StreamReader(dataFileName))
            {
                string line = sr.ReadLine();
                task = new List<Task>(Convert.ToInt32(line.Split(new char[]{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries)[0]));
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

        /// <summary>
        /// basing on:
        /// http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Schrage.pdf
        /// </summary>
        /// <param name="N">unordered tasks</param>
        /// <returns>pair(ordered tasks, cmax)</returns>
        public static KeyValuePair<List<Task>, int> ShrageOrdering(List<Task> inputData)
        {
            int t = 0;
            int CMax = 0;
            List<Task> N = new List<Task>(inputData);
            List<Task> G = new List<Task>(N.Count);
            List<Task> orderedTasks = new List<Task>(N.Count);

            while (G.Count != 0 || N.Count != 0)
            {
                int minVal;
                Task e;
                while (N.Count != 0 && (minVal = N.Min(x => x.R)) <= t)
                {
                    e = N.Find(x => x.R == minVal);
                    G.Add(e);
                    N.Remove(e);
                }
                if(G.Count == 0)
                {
                    t = N.Min(x => x.R);
                    continue;
                }
                int maxVal = G.Max(x => x.Q);
                e = G.Find(task => task.Q == maxVal); 
                orderedTasks.Add(e);
                t += e.P;
                CMax = Math.Max(CMax, t + e.Q);
                G.Remove(e);
            }


            return new KeyValuePair<List<Task>,int>(orderedTasks, CMax);
        }

        /// <summary>
        /// basing on:
        /// http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Schrage.pdf
        /// </summary>
        /// <param name="N">unordered tasks</param>
        /// <returns>pair(ordered tasks, cmax)</returns>
        public static KeyValuePair<List<Task>, int> PrmtShrageOrdering(List<Task> inputData)
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            List<Task> tasks = LoadData("in100.txt");
            var shrageOrderedTasks = ShrageOrdering(tasks);

            Heap<int> myHeap = new Heap<int>((x, y) => x > y);

            Console.ReadKey();
        }


    }
}
