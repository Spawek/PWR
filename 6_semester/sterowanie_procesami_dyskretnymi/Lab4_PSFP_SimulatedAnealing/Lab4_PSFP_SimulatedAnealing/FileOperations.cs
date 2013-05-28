using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab4_PSFP_SimulatedAnealing
{
    internal class FileOperations
    {
        internal static List<LinkedList<Task>> LoadDataFromFiles(string subtaskFileName, string transportTimesFileName)
        {
            List<LinkedList<Task>> output = LoadSubtasksFromFile(subtaskFileName);
            output = LoadTransportTimesFromFile(transportTimesFileName, output);

            return output;
        }

        private static List<LinkedList<Task>> LoadTransportTimesFromFile(string transportTimesFileName, List<LinkedList<Task>> taskList)
        {
            using (StreamReader sr = new StreamReader(transportTimesFileName))
            {
                string line;
                int currListPos = 0;
                LinkedListNode<Task> currLListNode = currLListNode = taskList[currListPos].First;
                while ((line = sr.ReadLine()) != null)
                {
                    if (sr.EndOfStream) break;
                    if (line == String.Empty)
                    {
                        currLListNode = taskList[++currListPos].First;
                        currLListNode.Value.transportTimes.Add(0); //transport to first node is always zero in here
                    }
                    else
                    {
                        string[] splittedLine = line.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string transportTime in splittedLine)
                        {
                            currLListNode.Value.transportTimes.Add(Convert.ToInt32(transportTime));
                        }
                    }
                }
            }

            return taskList;
        }

        private static List<LinkedList<Task>> LoadSubtasksFromFile(string subtaskFileName)
        {
            List<LinkedList<Task>> output = new List<LinkedList<Task>>();
            using (StreamReader sr = new StreamReader(subtaskFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("ta")) continue;
                    if (line.Split(new char[] { ' ' }).Length == 2)
                    {
                        output.Add(new LinkedList<Task>());
                        Task.ResetTaskCounter();
                        continue;
                    }

                    string[] splittedLine = line.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splittedLine.Length > 0) output[output.Count - 1].AddLast(new Task());
                    for (int i = 0; i < splittedLine.Length; i++)
                    {
                        output[output.Count - 1].Last().subtasks.Add(Convert.ToInt32(splittedLine[i]));
                    }
                }
            }
            return output;
        }

    }
}
