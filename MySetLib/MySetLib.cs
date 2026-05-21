using System.Collections;

namespace SetProject;

public class MySet<T> : IEnumerable<T> where T : IComparable<T>
{
    private readonly List<T> _list = new List<T>();

    public MySet() { }

    public MySet(IEnumerable<T> items)
    {
        AddRange(items);
    }

    public void Add(T item)
    {
        if (Contains(item)) return;
        _list.Add(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public bool Contains(T item)
    {
        return _list.Contains(item);
    }

    public void Remove(T item)
    {
        _list.Remove(item);
    }

    public int Count => _list.Count;

    public MySet<T> Union(MySet<T> other)
    {
        MySet<T> result = new MySet<T>(_list);
        result.AddRange(other);
        return result;
    }

    public MySet<T> Intersection(MySet<T> other)
    {
        MySet<T> result = new MySet<T>();
        foreach (var item in _list)
        {
            if (other.Contains(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public MySet<T> Difference(MySet<T> other)
    {
        MySet<T> result = new MySet<T>(_list);
        foreach (var item in other)
        {
            result.Remove(item);
        }
        return result;
    }

    public MySet<T> SymmetricDifference(MySet<T> other)
    {
        MySet<T> union = Union(other);
        MySet<T> intersection = Intersection(other);
        return union.Difference(intersection);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}