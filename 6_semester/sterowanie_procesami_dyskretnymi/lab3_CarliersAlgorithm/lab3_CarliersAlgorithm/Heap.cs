using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab3_CarliersAlgorithm
{
    class Heap<T>
    {
        Func<T, T, bool> cmpFcn;
        List<T> data = new List<T>();

        public Heap(Func<T, T, bool> cmpPredicate)
        {
            cmpFcn = cmpPredicate;
        }

        public T ExtractLast()
        {
            if (data.Count < 0)
                throw new ApplicationException("heap is empty");

            T last = data[0];
            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            this.Heapify(0);
            return last;
        }

        private void Heapify(int i)
        {
            int smallest;
            int l = 2 * i;
            int r = 2 * i + 1;

            if (l < data.Count && cmpFcn(data[i], data[l]))
            {
                smallest = l;
            }
            else
            {
                smallest = i;
            }

            if (r < data.Count && cmpFcn(data[smallest], data[r]))
            {
                smallest = r;
            }

            if (smallest != i)
            {
                T tmp = data[i];
                data[i] = data[smallest];
                data[smallest] = tmp;
                this.Heapify(smallest);
            }
        }

        public void Insert(T o)
        {
            data.Add(default(T));
            int i = data.Count - 1;
            while (i > 0 && cmpFcn(data[i / 2], o))
            {
                data[i] = data[i / 2];
                i = i / 2;
            }

            data[i] = o;
        }

        public int Count
        {
            get { return data.Count; }
        }
    }

    [TestClass]
    public class HeapTests
    {
        [TestMethod]
        public void InsertTest()
        {
            Heap<int> heap = new Heap<int>((x, y) => x > y);

            int[] data = new int[] { 5, 3, 7, 84, 4, 2, 5, 432, 23, 432, 4, 32, 5, 62, 621, 1, 1, 1, 1, 5, -5432, 7 };
            foreach (int val in data)
            {
                heap.Insert(val);
            }

            foreach (int val in data.OrderBy(x => x))
            {
                Assert.AreEqual(val, heap.ExtractLast());
            }
        }
    }
}
