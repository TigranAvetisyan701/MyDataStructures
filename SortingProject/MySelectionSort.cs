namespace MySelectionSortProj
{
    public class MySelectionSort<T>
        where T : IComparable<T>
    {
        public void Sort(T[] items)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < items.Length; j++)
                {
                    if (items[j].CompareTo(items[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }


                if (minIndex != i)
                {
                    T temp = items[i];
                    items[i] = items[minIndex];
                    items[minIndex] = temp;
                }
            }
        }
    }
}
