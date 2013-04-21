using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_CarliersAlgorithm
{
    public partial class Program
    {
        static int UB = int.MaxValue;
        static List<Task> output;
        /// <summary>
        /// basing on:
        /// http://dominik.zelazny.staff.iiar.pwr.wroc.pl/materialy/Algorytm_Carlier.pdf
        /// </summary>
        /// <param name="N">unordered tasks - IT CHANGES THIS DATA!</param>
        /// <returns>pair(ordered tasks, CMax)</returns>
        public static KeyValuePair<List<Task>, int> CaliersOrdering(List<Task> input)
        {
            var shrageOut = ShrageOrdering(input);
            int U = shrageOut.Value;
            //if (U < UB) //WTF?!?! after commending it algoritm started working correctly
            {
                UB = U;
                output = shrageOut.Key;
            }

            Task B = output.FindLast(x => U == x.workTime.Last().Value + x.Q);
            int BIndex = output.IndexOf(B);

            Task A = output.Find(x =>
            {
                int xIndex = output.IndexOf(x); //can be optimized
                return UB == x.R + output.GetRange(xIndex, BIndex - xIndex + 1).Sum(y => y.P) + B.Q; //can be optimized //IMPORTANT: changed from U
            });
            int AIndex = output.IndexOf(A);

            Task C = output.GetRange(AIndex, BIndex - AIndex + 1).FindLast(x => x.Q < B.Q);
            int CIndex = output.IndexOf(C);

            if (C == null)
            {
                return new KeyValuePair<List<Task>, int>(output, U);
            }

            //can be optimized easily
            int MinRInRange = output.GetRange(CIndex + 1, BIndex - CIndex).Min(x => x.R);
            int MinQInRange = output.GetRange(CIndex + 1, BIndex - CIndex).Min(x => x.Q);
            int PSumInRange = output.GetRange(CIndex + 1, BIndex - CIndex).Sum(x => x.P);

            int oldRC = C.R;
            C.R = Math.Max(C.R, MinRInRange + PSumInRange);

            int LB = PrmtShrageOrderingNotChagingInput(output).Value;

            if (LB < UB)
            {
                CaliersOrdering(input);
            }
            C.R = oldRC;

            int oldCQ = C.Q;
            C.Q = Math.Max(C.Q, MinQInRange + PSumInRange);
            LB = PrmtShrageOrderingNotChagingInput(output).Value;
            if (LB < UB)
            {
                CaliersOrdering(input);
            }
            C.Q = oldCQ;

            int CMax = 0;
            int t = 0;
            foreach (var item in output)
            {
                t = Math.Max(item.R, t) + item.P;
                CMax = Math.Max(CMax, t + item.Q);
            }
            return new KeyValuePair<List<Task>, int>(output, CMax);
        }

    }
}
