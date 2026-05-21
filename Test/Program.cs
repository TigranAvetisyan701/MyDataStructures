using System;
using System.Linq;
using MyLinkedListLib;
using StackLib;
using MyQueueLib;
using MyBinaryTreeProj;

namespace Tests
{
    internal class Program
    {
        static int passed = 0;
        static int failed = 0;

        static void Main(string[] args)
        {
            TestLinkedList();
            TestLinkedListStack();
            TestArrayStack();
            TestQueue();
            TestBinaryTree();

            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine($"  Results: {passed} passed, {failed} failed");
            Console.WriteLine("================================");
        }

        #region Helpers
        static void Assert(string testName, bool condition)
        {
            if (condition)
            {
                Console.WriteLine($"  [PASS] {testName}");
                passed++;
            }
            else
            {
                Console.WriteLine($"  [FAIL] {testName}");
                failed++;
            }
        }

        static void AssertThrows<TException>(string testName, Action action) where TException : Exception
        {
            try
            {
                action();
                Console.WriteLine($"  [FAIL] {testName} (no exception thrown)");
                failed++;
            }
            catch (TException)
            {
                Console.WriteLine($"  [PASS] {testName}");
                passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [FAIL] {testName} (wrong exception: {ex.GetType().Name})");
                failed++;
            }
        }

        static void Section(string name)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {name} ---");
        }
        #endregion

        #region LinkedList Tests
        static void TestLinkedList()
        {
            Section("MyLinkedList");

            var list = new MyLinkedList<int>();

            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            Assert("AddLast: Count is 3", list.Count == 3);
            Assert("AddLast: Head is 1", list.Head.Value == 1);
            Assert("AddLast: Tail is 3", list.Tail.Value == 3);

            list.AddFirst(0);
            Assert("AddFirst: Head is 0", list.Head.Value == 0);
            Assert("AddFirst: Count is 4", list.Count == 4);

            Assert("Contains: existing value", list.Contains(2));
            Assert("Contains: missing value", !list.Contains(99));

            list.Remove(0);
            Assert("Remove head: Head updated", list.Head.Value == 1);
            Assert("Remove head: Count is 3", list.Count == 3);

            list.Remove(3);
            Assert("Remove tail: Tail updated", list.Tail.Value == 2);
            Assert("Remove tail: Count is 2", list.Count == 2);

            list.Remove(1);
            Assert("Remove middle: Count is 1", list.Count == 1);

            bool removed = list.Remove(99);
            Assert("Remove missing: returns false", !removed);

            list.AddLast(10);
            list.AddLast(20);
            list.RemoveFirst();
            Assert("RemoveFirst: Head updated", list.Head.Value == 10);
            list.RemoveLast();
            Assert("RemoveLast: Tail updated", list.Tail.Value == 10);

            list.Clear();
            Assert("Clear: Count is 0", list.Count == 0);
            Assert("Clear: Head is null", list.Head == null);
            Assert("Clear: Tail is null", list.Tail == null);

            list.AddLast(5);
            list.AddLast(6);
            Assert("First: returns head value", list.First() == 5);
            Assert("Last: returns tail value", list.Last() == 6);

            list.Clear();
            AssertThrows<InvalidOperationException>("First on empty: throws", () => list.First());
            AssertThrows<InvalidOperationException>("Last on empty: throws", () => list.Last());

            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            int[] expected = { 1, 2, 3 };
            Assert("Enumeration: correct order", list.SequenceEqual(expected));

            var arr = new int[3];
            list.CopyTo(arr, 0);
            Assert("CopyTo: correct values", arr.SequenceEqual(expected));
        }
        #endregion

        #region LinkedList Stack Tests
        static void TestLinkedListStack()
        {
            Section("MyStack (LinkedList-based)");

            var stack = new MyStack<int>();

            Assert("Empty stack: Count is 0", stack.Count == 0);
            Assert("Empty stack: IsEmpty is true", stack.IsEmpty);

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert("Push: Count is 3", stack.Count == 3);
            Assert("IsEmpty: false after push", !stack.IsEmpty);

            Assert("Peek: returns top without removing", stack.Peek() == 3);
            Assert("Peek: Count unchanged", stack.Count == 3);

            Assert("Pop: returns 3", stack.Pop() == 3);
            Assert("Pop: returns 2", stack.Pop() == 2);
            Assert("Pop: Count is 1", stack.Count == 1);

            stack.Pop();
            Assert("Pop last: Count is 0", stack.Count == 0);

            AssertThrows<InvalidOperationException>("Pop on empty: throws", () => stack.Pop());
            AssertThrows<InvalidOperationException>("Peek on empty: throws", () => stack.Peek());
        }
        #endregion

        #region Array Stack Tests
        static void TestArrayStack()
        {
            Section("MyArrayStack (Array-based)");

            var stack = new ArrayStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert("Peek: returns top", stack.Peek() == 3);

            Assert("Pop: returns 3", stack.Pop() == 3);
            Assert("Pop: returns 2", stack.Pop() == 2);
            Assert("Pop: returns 1", stack.Pop() == 1);

            AssertThrows<InvalidOperationException>("Pop on empty: throws", () => stack.Pop());
            AssertThrows<InvalidOperationException>("Peek on empty: throws", () => stack.Peek());

            for (int i = 0; i < 20; i++)
                stack.Push(i);
            Assert("Dynamic resize: 20 pushes succeed", stack.Peek() == 19);

            for (int i = 19; i >= 0; i--)
                Assert($"Pop after resize: {i}", stack.Pop() == i);
        }
        #endregion

        #region Queue Tests
        static void TestQueue()
        {
            Section("MyQueue");

            var queue = new MyQueue<int>();

            Assert("Empty queue: Count is 0", queue.Count == 0);

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert("Enqueue: Count is 3", queue.Count == 3);

            Assert("Peek: returns front", queue.Peek() == 1);
            Assert("Peek: Count unchanged", queue.Count == 3);

            Assert("Dequeue: returns 1", queue.Dequeue() == 1);
            Assert("Dequeue: returns 2", queue.Dequeue() == 2);
            Assert("Dequeue: Count is 1", queue.Count == 1);

            queue.Dequeue();
            AssertThrows<InvalidOperationException>("Dequeue on empty: throws", () => queue.Dequeue());
            AssertThrows<InvalidOperationException>("Peek on empty: throws", () => queue.Peek());

            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            int[] expected = { 10, 20, 30 };
            Assert("Enumeration: FIFO order", queue.SequenceEqual(expected));
        }
        #endregion

        #region Binary Tree Tests
        static void TestBinaryTree()
        {
            Section("MyBinaryTree");

            var tree = new MyBinaryTree<int>();

            Assert("Empty tree: Count is 0", tree.Count == 0);

            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(1);
            tree.Add(4);
            tree.Add(6);
            tree.Add(8);
            Assert("Add: Count is 7", tree.Count == 7);

            Assert("Contains: existing value 5", tree.Contains(5));
            Assert("Contains: existing value 1", tree.Contains(1));
            Assert("Contains: existing value 8", tree.Contains(8));
            Assert("Contains: missing value 99", !tree.Contains(99));

            int[] expected = { 1, 3, 4, 5, 6, 7, 8 };
            Assert("InOrder: sorted ascending", tree.SequenceEqual(expected));

            tree.Remove(1);
            Assert("Remove leaf: Count is 6", tree.Count == 6);
            Assert("Remove leaf: no longer contains 1", !tree.Contains(1));

            tree.Remove(3);
            Assert("Remove one-child node: Count is 5", tree.Count == 5);
            Assert("Remove one-child node: no longer contains 3", !tree.Contains(3));
            Assert("Remove one-child node: child 4 still present", tree.Contains(4));

            tree.Remove(7);
            Assert("Remove two-child node: Count is 4", tree.Count == 4);
            Assert("Remove two-child node: no longer contains 7", !tree.Contains(7));
            Assert("Remove two-child node: children still present", tree.Contains(6) && tree.Contains(8));

            tree.Remove(5);
            Assert("Remove root: Count is 3", tree.Count == 3);
            Assert("Remove root: still valid BST order", tree.SequenceEqual(new[] { 4, 6, 8 }));

            bool removed = tree.Remove(99);
            Assert("Remove missing: returns false", !removed);
        }
        #endregion
    }
}