using System;

namespace SortingProject
{
    public class MyQuickSort<T> where T : IComparable<T>
    {
        public void Sort(T[] array)
        {
            if (array.Length <= 1)
            {
                return;
            }

            int pivotIndex = Partition(array);


            T[] left = array[..pivotIndex];
            T[] right = array[(pivotIndex + 1)..];

            Sort(left);
            Sort(right);


            for (int k = 0; k < left.Length; k++)
                array[k] = left[k];

            for (int k = 0; k < right.Length; k++)
                array[pivotIndex + 1 + k] = right[k];
        }

        private int Partition(T[] array)
        {
            T pivot = array[^1];
            int i = -1;

            for (int j = 0; j < array.Length - 1; j++)
            {
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            Swap(array, i + 1, array.Length - 1);
            return i + 1;
        }

        private void Swap(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}