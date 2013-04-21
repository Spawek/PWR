﻿using System;
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
        /// <param name="N">unordered tasks</param>
        /// <returns>pair(ordered tasks, CMax)</returns>
        public static KeyValuePair<List<Task>, int> PrmtShrageOrdering(List<Task> inputData)
        {
            int t = 0;
            int CMax = 0;
            Heap<Task> N = new Heap<Task>((x, y) => x.R > y.R);
            N.Insert(inputData);
            Heap<Task> G = new Heap<Task>((x, y) => x.Q < y.Q);
            List<Task> output = new List<Task>();
            Task e;
            Task l = new Task(0, 0, int.MaxValue);

            while (G.Count != 0 || N.Count != 0)
            {
                while (N.Count != 0 && N.ShowLast().R <= t)
                {
                    e = N.ExtractLast();
                    G.Insert(e);
                    output.Add(e);

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

            return new KeyValuePair<List<Task>, int>(output, CMax);
        }

        public static KeyValuePair<List<Task>, int> PrmtShrageOrderingNotChagingInput(List<Task> inputData)
        {
            List<Task> inputDataCopy = new List<Task>(inputData.Count);

            foreach (var item in inputData)
            {
                inputDataCopy.Add(new Task(item, false));
            }

            return PrmtShrageOrdering(inputDataCopy);
        }
    }
}