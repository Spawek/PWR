using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_PSFP_SimulatedAnealing
{
    public class Task
    {
        public List<int> subtasks;
        public List<int> transportTimes;
        public int taskNo;
        public static int maxNo = 0;

        public Task(List<int> _subtasks, List<int> _transportTimes)
        {
            subtasks = _subtasks;
            transportTimes = _transportTimes;

            taskNo = ++maxNo;
        }

        public Task()
        {
            subtasks = new List<int>();
            transportTimes = new List<int>();

            taskNo = ++maxNo;
        }

        public static void ResetTaskCounter()
        {
            maxNo = 0;
        }
    }
}
