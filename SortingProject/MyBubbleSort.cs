namespace MyBubbleProj;

public class MyBubbleSort<T>
    where T : IComparable<T>

{
    public void Sort(T[] items)
    {
        bool swapped;

        do
        {
            swapped = false;
            for (int i = 1; i < items.Length; i++)
            {
                if (Compare(items[i - 1], items[i]) > 0)
                {
                    Swap(items, i - 1, i);
                    swapped = true;
                }
            }
        } while (swapped != false);

    }
    private void Swap(T[] items, int i, int j)
    {
        T temp = items[i];
        items[i] = items[j];
        items[j] = temp;
    }

    private int Compare(T a, T b)
    {
        return a.CompareTo(b);
    }

}