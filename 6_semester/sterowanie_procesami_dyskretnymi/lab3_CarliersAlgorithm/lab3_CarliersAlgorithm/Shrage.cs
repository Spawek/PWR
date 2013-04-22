using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_CarliersAlgorithm
{
    public partial class Program
    {
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
            Heap<Task> N = new Heap<Task>((x, y) => x.R > y.R);
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
                    e.workTime.Clear();
                    e.workTime.Add(new KeyValuePair<int, int>(t, t + e.P)); //should not impact tests //needed for Calier
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
        /// <param name="N">unordered tasks - IT CHANGES THIS DATA!</param>
        /// <returns>pair(ordered tasks, CMax)</returns>
        public static KeyValuePair<List<Task>, int> ShrageOrderingWoHeap(List<Task> inputData)
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
                if (G.Count == 0)
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

            return new KeyValuePair<List<Task>, int>(orderedTasks, CMax);
        }
    }
}
