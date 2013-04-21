using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task(Task task, bool copyWorkTime = true)
        {
            R = task.R;
            P = task.P;
            Q = task.Q;

            P_left = task.P_left;
            no = task.no;

            if (copyWorkTime)
            {
                foreach (var item in task.workTime)
                {
                    workTime.Add(new KeyValuePair<int, int>(item.Key, item.Value));
                }
            }
        }

        public static void RestartNo()
        {
            nextNo = 1;
        }
    }

}
