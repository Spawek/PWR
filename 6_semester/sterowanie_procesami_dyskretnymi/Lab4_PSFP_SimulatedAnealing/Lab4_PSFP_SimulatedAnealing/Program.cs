using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_PSFP_SimulatedAnealing
{
    class Program
    {
        static void Main(string[] args)
        {
            var Tasks = FileOperations.LoadDataFromFiles("pfsp_-_instancje.txt", "pfsp_-_transport.txt");
            //TODO: wczytywanie danych o transporcie nie dziala
        }
    }
}
