using System.Collections.Generic;

namespace Algorithms;

public sealed class BinarySearchTree
{
    private sealed class Node
    {
        public Node(int value) => Value = value;

        public int Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
    }

    private Node? root;

    public void Insert(int value)
    {
        root = Insert(root, value);
    }

    public bool Contains(int value)
    {
        var current = root;
        while (current is not null)
        {
            if (value == current.Value)
                return true;

            current = value < current.Value ? current.Left : current.Right;
        }

        return false;
    }

    public bool Remove(int value)
    {
        var removed = false;
        root = Remove(root, value, ref removed);
        return removed;
    }

    public IReadOnlyList<int> InOrder() => Traverse(TraversalOrder.InOrder);
    public IReadOnlyList<int> PreOrder() => Traverse(TraversalOrder.PreOrder);
    public IReadOnlyList<int> PostOrder() => Traverse(TraversalOrder.PostOrder);

    private static Node Insert(Node? node, int value)
    {
        if (node is null)
            return new Node(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else
            node.Right = Insert(node.Right, value);

        return node;
    }

    private static Node? Remove(Node? node, int value, ref bool removed)
    {
        if (node is null)
            return null;

        if (value < node.Value)
            node.Left = Remove(node.Left, value, ref removed);
        else if (value > node.Value)
            node.Right = Remove(node.Right, value, ref removed);
        else
        {
            removed = true;
            if (node.Left is null)
                return node.Right;
            if (node.Right is null)
                return node.Left;

            var successor = node.Right;
            while (successor.Left is not null)
                successor = successor.Left;

            node.Value = successor.Value;
            bool successorRemoved = false;
            node.Right = Remove(node.Right, successor.Value, ref successorRemoved);
        }

        return node;
    }

    private IReadOnlyList<int> Traverse(TraversalOrder order)
    {
        var result = new List<int>();
        Traverse(root, order, result);
        return result;
    }

    private static void Traverse(Node? node, TraversalOrder order, List<int> result)
    {
        if (node is null)
            return;

        if (order == TraversalOrder.PreOrder)
            result.Add(node.Value);

        Traverse(node.Left, order, result);

        if (order == TraversalOrder.InOrder)
            result.Add(node.Value);

        Traverse(node.Right, order, result);

        if (order == TraversalOrder.PostOrder)
            result.Add(node.Value);
    }

    private enum TraversalOrder
    {
        InOrder,
        PreOrder,
        PostOrder
    }
}
