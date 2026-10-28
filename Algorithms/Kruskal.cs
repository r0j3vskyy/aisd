using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms;

public sealed record Vertex(int Value);

public sealed record Edge
{
    public Edge(Vertex start, Vertex end, int weight = 1)
    {
        ArgumentNullException.ThrowIfNull(start);
        ArgumentNullException.ThrowIfNull(end);
        Start = start;
        End = end;
        Weight = weight;
    }

    public Vertex Start { get; }
    public Vertex End { get; }
    public int Weight { get; }
}

public sealed class Graph
{
    private readonly List<Vertex> vertices = new();
    private readonly List<Edge> edges = new();

    public IReadOnlyList<Vertex> Vertices => vertices;
    public IReadOnlyList<Edge> Edges => edges;

    public void AddVertex(Vertex vertex)
    {
        ArgumentNullException.ThrowIfNull(vertex);

        if (!vertices.Contains(vertex))
            vertices.Add(vertex);
    }

    public void AddEdge(Edge edge)
    {
        ArgumentNullException.ThrowIfNull(edge);

        AddVertex(edge.Start);
        AddVertex(edge.End);
        edges.Add(edge);
    }
}

public static class Kruskal
{
    public static Graph FindMinimumSpanningForest(Graph graph)
    {
        ArgumentNullException.ThrowIfNull(graph);

        var result = new Graph();
        var parent = graph.Vertices.ToDictionary(vertex => vertex, vertex => vertex);
        var rank = graph.Vertices.ToDictionary(vertex => vertex, _ => 0);

        foreach (var vertex in graph.Vertices)
            result.AddVertex(vertex);

        foreach (var edge in graph.Edges.OrderBy(edge => edge.Weight))
        {
            var startRoot = FindRoot(parent, edge.Start);
            var endRoot = FindRoot(parent, edge.End);

            if (startRoot == endRoot)
                continue;

            result.AddEdge(edge);
            Union(parent, rank, startRoot, endRoot);
        }

        return result;
    }

    private static Vertex FindRoot(Dictionary<Vertex, Vertex> parent, Vertex vertex)
    {
        if (parent[vertex] != vertex)
            parent[vertex] = FindRoot(parent, parent[vertex]);

        return parent[vertex];
    }

    private static void Union(
        Dictionary<Vertex, Vertex> parent,
        Dictionary<Vertex, int> rank,
        Vertex first,
        Vertex second)
    {
        if (rank[first] < rank[second])
            parent[first] = second;
        else if (rank[first] > rank[second])
            parent[second] = first;
        else
        {
            parent[second] = first;
            rank[first]++;
        }
    }
}
