using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms;

var tests = new (string Name, Action Test)[]
{
    ("BubbleSort", TestBubbleSort),
    ("CountingSort", TestCountingSort),
    ("InsertionSort", TestInsertionSort),
    ("MergeSort", TestMergeSort),
    ("SelectSort", TestSelectSort),
    ("DoublyLinkedList", TestDoublyLinkedList),
    ("BinarySearchTree", TestBinarySearchTree),
    ("Kruskal", TestKruskal),
    ("Dijkstra", TestDijkstra)
};

int passed = 0;
foreach (var (name, test) in tests)
{
    try
    {
        test();
        Console.WriteLine($"PASS  {name}");
        passed++;
    }
    catch (Exception exception)
    {
        Console.WriteLine($"FAIL  {name}: {exception.Message}");
    }
}

Console.WriteLine($"\nPassed: {passed}/{tests.Length}");
if (passed != tests.Length)
    Environment.ExitCode = 1;

static void TestBubbleSort() => TestSortAlgorithm(BubbleSort.Sort);

static void TestCountingSort()
{
    TestSortAlgorithm(CountingSort.Sort, new[] { -5, 0, 3, 3, -1, 2 });
    AssertThrows<ArgumentException>(() =>
        CountingSort.Sort(new[] { int.MinValue, int.MaxValue }));
}

static void TestInsertionSort() => TestSortAlgorithm(InsertionSort.Sort);

static void TestMergeSort() => TestSortAlgorithm(MergeSort.Sort);

static void TestSelectSort() => TestSortAlgorithm(SelectSort.Sort);

static void TestSortAlgorithm(
    Func<int[], int[]> sort,
    int[]? additionalValues = null)
{
    var cases = new[]
    {
        Array.Empty<int>(),
        new[] { 1 },
        new[] { 5, 4, 3, 2, 1 },
        new[] { -2, 7, 0, -2, 7, 1 },
        new[] { 4, 4, 4, 4 }
    };

    foreach (var values in cases.Append(additionalValues ?? Array.Empty<int>()))
    {
        var original = (int[])values.Clone();
        var expected = values.OrderBy(value => value).ToArray();
        var actual = sort(values);

        AssertSequenceEqual(expected, actual);
        AssertSequenceEqual(original, values);
    }

    AssertThrows<ArgumentNullException>(() => sort(null!));
}

static void TestDoublyLinkedList()
{
    var list = new DoublyLinkedList();
    AssertSequenceEqual(Array.Empty<int>(), list.ToArray());

    list.AddFirst(2);
    list.AddFirst(1);
    list.AddLast(3);
    AssertSequenceEqual(new[] { 1, 2, 3 }, list.ToArray());
    AssertEqual("[1, 2, 3]", list.ToString());
    AssertEqual(3, list.Count);

    Assert(list.RemoveFirst(2), "Existing value should be removed.");
    Assert(!list.RemoveFirst(99), "Missing value should not be removed.");
    AssertSequenceEqual(new[] { 1, 3 }, list.ToArray());
}

static void TestBinarySearchTree()
{
    var tree = new BinarySearchTree();
    foreach (int value in new[] { 5, 3, 7, 2, 4, 6, 8, 7 })
        tree.Insert(value);

    Assert(tree.Contains(4), "Inserted value should be found.");
    Assert(!tree.Contains(99), "Missing value should not be found.");
    AssertSequenceEqual(new[] { 2, 3, 4, 5, 6, 7, 7, 8 }, tree.InOrder());
    Assert(tree.Remove(5), "Root should be removable.");
    Assert(!tree.Remove(99), "Missing value should not be removable.");
    AssertSequenceEqual(new[] { 2, 3, 4, 6, 7, 7, 8 }, tree.InOrder());
}

static void TestKruskal()
{
    var graph = new Graph();
    var vertices = Enumerable.Range(1, 4).Select(value => new Vertex(value)).ToArray();
    graph.AddEdge(new Edge(vertices[0], vertices[1], 1));
    graph.AddEdge(new Edge(vertices[1], vertices[2], 2));
    graph.AddEdge(new Edge(vertices[2], vertices[3], 3));
    graph.AddEdge(new Edge(vertices[0], vertices[3], 10));
    graph.AddEdge(new Edge(vertices[0], vertices[1], -1));

    var forest = Kruskal.FindMinimumSpanningForest(graph);
    AssertEqual(3, forest.Edges.Count);
    AssertEqual(4, forest.Edges.Sum(edge => edge.Weight));
}

static void TestDijkstra()
{
    var graph = new WeightedGraph();
    var start = new DijkstraVertex(1);
    var middle = new DijkstraVertex(2);
    var end = new DijkstraVertex(3);
    var isolated = new DijkstraVertex(4);
    graph.AddEdge(new DijkstraEdge(start, middle, 4));
    graph.AddEdge(new DijkstraEdge(middle, end, 3));
    graph.AddEdge(new DijkstraEdge(start, end, 10));
    graph.AddVertex(isolated);

    var paths = Dijkstra.FindShortestPaths(graph, start);
    AssertEqual(7L, paths[end].Distance);
    AssertEqual(middle, paths[end].Previous);
    Assert(!paths[isolated].IsReachable, "Isolated vertex should be unreachable.");
    AssertThrows<ArgumentOutOfRangeException>(() =>
        new DijkstraEdge(start, end, -1));
}

static void Assert(bool condition, string message)
{
    if (!condition)
        throw new InvalidOperationException(message);
}

static void AssertEqual<T>(T expected, T actual)
{
    if (!EqualityComparer<T>.Default.Equals(expected, actual))
        throw new InvalidOperationException($"Expected '{expected}', got '{actual}'.");
}

static void AssertSequenceEqual<T>(
    IEnumerable<T> expected,
    IEnumerable<T> actual)
{
    if (!expected.SequenceEqual(actual))
    {
        throw new InvalidOperationException(
            $"Expected [{string.Join(", ", expected)}], got [{string.Join(", ", actual)}].");
    }
}

static void AssertThrows<TException>(Action action)
    where TException : Exception
{
    try
    {
        action();
    }
    catch (TException)
    {
        return;
    }

    throw new InvalidOperationException(
        $"Expected exception {typeof(TException).Name}.");
}
