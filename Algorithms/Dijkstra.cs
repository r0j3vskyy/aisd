using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms;

public sealed record DijkstraVertex(int Value);

public sealed record DijkstraEdge
{
    public DijkstraEdge(DijkstraVertex start, DijkstraVertex end, int weight)
    {
        ArgumentNullException.ThrowIfNull(start);
        ArgumentNullException.ThrowIfNull(end);
        if (weight < 0)
            throw new ArgumentOutOfRangeException(
                nameof(weight),
                "Dijkstra's algorithm does not support negative weights.");

        Start = start;
        End = end;
        Weight = weight;
    }

    public DijkstraVertex Start { get; }
    public DijkstraVertex End { get; }
    public int Weight { get; }
}

public sealed class WeightedGraph
{
    private readonly List<DijkstraVertex> vertices = new();
    private readonly List<DijkstraEdge> edges = new();

    public IReadOnlyList<DijkstraVertex> Vertices => vertices;
    public IReadOnlyList<DijkstraEdge> Edges => edges;

    public void AddVertex(DijkstraVertex vertex)
    {
        ArgumentNullException.ThrowIfNull(vertex);

        if (!vertices.Contains(vertex))
            vertices.Add(vertex);
    }

    public void AddEdge(DijkstraEdge edge)
    {
        ArgumentNullException.ThrowIfNull(edge);

        AddVertex(edge.Start);
        AddVertex(edge.End);

        edges.Add(edge);
    }
}

public sealed record ShortestPath(
    DijkstraVertex Vertex,
    long Distance,
    DijkstraVertex? Previous)
{
    public bool IsReachable => Distance != long.MaxValue;
}

public static class Dijkstra
{
    public static IReadOnlyDictionary<DijkstraVertex, ShortestPath> FindShortestPaths(
        WeightedGraph graph,
        DijkstraVertex start)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(start);

        if (!graph.Vertices.Contains(start))
            throw new ArgumentException("The start vertex does not belong to the graph.", nameof(start));

        var distances = graph.Vertices.ToDictionary(vertex => vertex, _ => long.MaxValue);
        var previous = graph.Vertices.ToDictionary<DijkstraVertex, DijkstraVertex, DijkstraVertex?>(
            vertex => vertex,
            _ => null);
        var unvisited = new HashSet<DijkstraVertex>(graph.Vertices);
        distances[start] = 0;

        while (unvisited.Count > 0)
        {
            var current = unvisited.MinBy(vertex => distances[vertex]);
            if (current is null || distances[current] == long.MaxValue)
                break;

            unvisited.Remove(current);

            foreach (var edge in graph.Edges.Where(edge =>
                edge.Start == current || edge.End == current))
            {
                var neighbour = edge.Start == current ? edge.End : edge.Start;
                if (!unvisited.Contains(neighbour))
                    continue;

                long candidateDistance = distances[current] + edge.Weight;
                if (candidateDistance < distances[neighbour])
                {
                    distances[neighbour] = candidateDistance;
                    previous[neighbour] = current;
                }
            }
        }

        return graph.Vertices.ToDictionary(
            vertex => vertex,
            vertex => new ShortestPath(vertex, distances[vertex], previous[vertex]));
    }
}
