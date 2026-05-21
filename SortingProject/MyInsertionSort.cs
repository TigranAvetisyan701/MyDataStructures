namespace MyInsertionSortProj;


public class MyInsertionSort<T>
where T : IComparable<T>
{
    public void Sort(T[] items)
    {
        for (int i = 1; i < items.Length; i++)
        {
            T current = items[i];
            int j = i - 1;


            while (j >= 0 && items[j].CompareTo(current) > 0)
            {
                items[j + 1] = items[j];
                j--;
            }


            items[j + 1] = current;
        }
    }

}