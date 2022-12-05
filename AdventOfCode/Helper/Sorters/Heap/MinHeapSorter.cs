using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helper.Sorters.Heap
{
    public class MinHeapSorter<T>
    {
        T[] arrToSort = null;

        public MinHeapSorter(T[] arr)
        {
            arrToSort = arr;
        }
        public MinHeapSorter()
        {
        }

        public void AddItem(T item)
        {
            if (arrToSort == null)
                arrToSort = new T[1];
            else if (arrToSort[arrToSort.Length - 1] != null)
                Array.Resize(ref arrToSort, arrToSort.Length + 1);

            arrToSort[arrToSort.Length - 1] = item;

            for (int i = (arrToSort.Length - 1) / 2; i >= 0; i--)
                Heapify(i);
        }

        public T ExtractMinItem()
        {
            if (arrToSort.Length == 0)
                return default;

            T min = arrToSort[0];

            arrToSort[0] = arrToSort[arrToSort.Length - 1];
            Array.Resize(ref arrToSort, arrToSort.Length - 1);
            Heapify(0);

            return min;
        }

        public IEnumerable<T> GetNextMinItem()
        {
            if (arrToSort.Length == 0)
                yield return default;

            while (arrToSort.Length > 0)
            {
                T min = arrToSort[0];

                arrToSort[0] = arrToSort[arrToSort.Length - 1];
                Array.Resize(ref arrToSort, arrToSort.Length - 1);
                Heapify(0);

                yield return min;
            }

        }

        void Heapify(int index)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            if (index < 0)
                return;

            var leftChildIndex = (2 * index) + 1;
            var rightChildIndex = leftChildIndex + 1;
            var smallesIndex = leftChildIndex;

            if (leftChildIndex < arrToSort.Length && rightChildIndex < arrToSort.Length &&
                comparer.Compare(arrToSort[leftChildIndex], arrToSort[rightChildIndex]) < 0)
                smallesIndex = leftChildIndex;

            else if (leftChildIndex < arrToSort.Length && rightChildIndex < arrToSort.Length &&
                comparer.Compare(arrToSort[leftChildIndex], arrToSort[rightChildIndex]) > 0)
                smallesIndex = rightChildIndex;

            if (leftChildIndex < arrToSort.Length &&
                comparer.Compare(arrToSort[index], arrToSort[smallesIndex]) < 0)
                smallesIndex = index;

            // swap smallestIndex with index
            if (smallesIndex < arrToSort.Length &&
                index < arrToSort.Length &&
                smallesIndex != index)
            {
                var tmp = arrToSort[index];
                arrToSort[index] = arrToSort[smallesIndex];
                arrToSort[smallesIndex] = tmp;
                if (smallesIndex >= (arrToSort.Length - 1) / 2)
                    Heapify(smallesIndex);
            }
        }
    }
}
