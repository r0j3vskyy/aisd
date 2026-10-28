using System.Collections;
using System.Collections.Generic;

namespace Algorithms;

public sealed class DoublyLinkedList : IEnumerable<int>
{
    private sealed class Node
    {
        public Node(int value) => Value = value;

        public int Value { get; }
        public Node? Previous { get; set; }
        public Node? Next { get; set; }
    }

    private Node? head;
    private Node? tail;

    public int Count { get; private set; }

    public void AddFirst(int value)
    {
        var node = new Node(value)
        {
            Next = head
        };

        if (head is null)
            tail = node;
        else
            head.Previous = node;

        head = node;
        Count++;
    }

    public void AddLast(int value)
    {
        var node = new Node(value)
        {
            Previous = tail
        };

        if (tail is null)
            head = node;
        else
            tail.Next = node;

        tail = node;
        Count++;
    }

    public bool RemoveFirst(int value)
    {
        var current = head;
        while (current is not null)
        {
            if (current.Value == value)
            {
                RemoveNode(current);
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public int[] ToArray()
    {
        var values = new int[Count];
        var current = head;
        var index = 0;

        while (current is not null)
        {
            values[index++] = current.Value;
            current = current.Next;
        }

        return values;
    }

    public IEnumerator<int> GetEnumerator()
    {
        var current = head;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"[{string.Join(", ", this)}]";

    private void RemoveNode(Node node)
    {
        if (node.Previous is null)
            head = node.Next;
        else
            node.Previous.Next = node.Next;

        if (node.Next is null)
            tail = node.Previous;
        else
            node.Next.Previous = node.Previous;

        Count--;
    }
}
