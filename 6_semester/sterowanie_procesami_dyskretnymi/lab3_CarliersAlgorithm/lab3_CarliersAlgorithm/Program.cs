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

        public int P_left;
        
        /// <summary>
        /// list(start time, end time) showing when work on obj was done
        /// </summary>
        public List<KeyValuePair<int, int>> workTime = new List<KeyValuePair<int, int>>();

        public int no;

        private static int nextNo = 1;

        public Task(int _R, int _P, int _Q)
        {
            R = _R;
            P = _P;
            Q = _Q;

            P_left = P;
            
            no = nextNo++;
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
        /// <param name="N">unordered tasks - IT CHANGES THIS DATA!</param>
        /// <returns>pair(ordered tasks, CMax)</returns>
        public static KeyValuePair<List<Task>, int> ShrageOrdering(List<Task> inputData)
        {
            int t = 0;
            int CMax = 0;
            Heap<Task> N = new Heap<Task>((x,y) => x.R > y.R);
            N.Insert(inputData);
            Heap<Task> G = new Heap<Task>((x, y) => x.Q < y.Q);
            List<Task> orderedTasks = new List<Task>(N.Count);

            while (G.Count != 0 || N.Count != 0)
            {
                while (N.Count != 0 && N.ShowLast().R <= t)
                {
                    G.Insert(N.ExtractLast());
                }
                if (G.Count == 0)
                {
                    t = N.ShowLast().R;
                }
                else
                {
                    Task e = G.ExtractLast();
                    orderedTasks.Add(e);
                    t += e.P;
                    CMax = Math.Max(CMax, t + e.Q);
                }
            }


            return new KeyValuePair<List<Task>, int>(orderedTasks, CMax);
        }

        /// <summary>
        /// basing on:
        /// http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Schrage.pdf
        /// </summary>
        /// <param name="N">unordered tasks</param>
        /// <returns>C_Max</returns>
        public static int PrmtShrageOrdering(List<Task> inputData)
        {
            int t = 0;
            int CMax = 0;
            Heap<Task> N = new Heap<Task>((x, y) => x.R > y.R);
            N.Insert(inputData);
            Heap<Task> G = new Heap<Task>((x, y) => x.Q < y.Q);
            Task e;
            Task l = new Task(0, 0, int.MaxValue);

            while (G.Count != 0 || N.Count != 0)
            {
                while (N.Count != 0 && N.ShowLast().R <= t)
                {
                    e = N.ExtractLast();
                    G.Insert(e);

                    if (e.Q > l.Q)
                    {
                        l.P = t - e.R;
                        t = e.R;

                        if (l.P > 0)
                        {
                            G.Insert(l);
                        }
                    }
                }

                if (G.Count == 0)
                {
                    t = N.ShowLast().R;
                }
                else
                {
                    e = G.ExtractLast();
                    l = e;
                    t += e.P;
                    CMax = Math.Max(CMax, t + e.Q);
                }
            }

            return CMax;
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
