using System;
using System.Collections;
using System.Collections.Generic;
using MyLinkedListLib;

namespace Queue.Priority
{
    public class MyPriorityQueue<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private MyLinkedList<T> _items = new MyLinkedList<T>();

        public void Enqueue(T item)
        {
            if (_items.Count == 0)
            {
                _items.AddLast(item);
            }
            else
            {
                var current = _items.Head;

                while (current != null && current.Value.CompareTo(item) > 0)
                {
                    current = current.Next;
                }

                if (current == null)
                {
                    _items.AddLast(item);
                }
                else
                {
                    _items.AddBefore(current, item);
                }
            }
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException();

            var value = _items.First();
            _items.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException();

            return _items.First();
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}