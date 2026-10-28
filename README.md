# Algorytmy i struktury danych

Zbiór samodzielnych implementacji podstawowych algorytmów i struktur danych w C#.

## Zawartość

| Plik | Algorytm / struktura | Najważniejsze operacje |
|---|---|---|
| `Algorithms/Kruskal.cs` | Algorytm Kruskala | minimalne drzewo rozpinające lub las dla grafu niespójnego |
| `Algorithms/Dijkstra.cs` | Algorytm Dijkstry | najkrótsze ścieżki w grafie z nieujemnymi wagami |
| `Algorithms/DoublyLinkedList.cs` | Lista dwukierunkowa | dodawanie, usuwanie, iterowanie i konwersja do tablicy |
| `Algorithms/BinarySearchTree.cs` | Binarne drzewo wyszukiwania | wstawianie, wyszukiwanie, usuwanie i przejścia po drzewie |
| `Algorithms/BubbleSort.cs` | Sortowanie bąbelkowe | `BubbleSort.Sort(values)` |
| `Algorithms/CountingSort.cs` | Sortowanie przez zliczanie | `CountingSort.Sort(values)` |
| `Algorithms/InsertionSort.cs` | Sortowanie przez wstawianie | `InsertionSort.Sort(values)` |
| `Algorithms/MergeSort.cs` | Sortowanie przez scalanie | `MergeSort.Sort(values)` |
| `Algorithms/SelectSort.cs` | Sortowanie przez wybieranie | `SelectSort.Sort(values)` |

## Wymagania

- .NET 10 SDK lub nowszy,
- C# z włączonym nullable reference types.

Pliki algorytmów nie wymagają Windows Forms ani innych zależności UI. Można je skopiować do własnego projektu konsolowego, biblioteki lub testów.

## Przykłady użycia

### Sortowanie

```csharp
using Algorithms;

int[] values = { 4, -1, 4, 0, 2 };
int[] sorted = BubbleSort.Sort(values);
// [-1, 0, 2, 4, 4]
```

### Lista dwukierunkowa

```csharp
using Algorithms;

var list = new DoublyLinkedList();
list.AddFirst(2);
list.AddLast(3);
list.AddFirst(1);

Console.WriteLine(list);
// [1, 2, 3]
```

### Binarne drzewo wyszukiwania

```csharp
using Algorithms;

var tree = new BinarySearchTree();
foreach (int value in new[] { 5, 3, 7, 2, 4 })
    tree.Insert(value);

tree.Remove(3);
IReadOnlyList<int> values = tree.InOrder();
// [2, 4, 5, 7]
```

### Kruskal

```csharp
using Algorithms;

var graph = new Graph();
var first = new Vertex(1);
var second = new Vertex(2);
var third = new Vertex(3);

graph.AddEdge(new Edge(first, second, 2));
graph.AddEdge(new Edge(second, third, 1));
graph.AddEdge(new Edge(first, third, 5));

Graph mst = Kruskal.FindMinimumSpanningForest(graph);
```

### Dijkstra

```csharp
using Algorithms;

var graph = new WeightedGraph();
var start = new DijkstraVertex(1);
var end = new DijkstraVertex(2);

graph.AddEdge(new DijkstraEdge(start, end, 4));
var paths = Dijkstra.FindShortestPaths(graph, start);
long distance = paths[end].Distance;
```

## Walidacja

Projekt w katalogu `Tests` pełni rolę prostego runnera testów. Sprawdza
dziewięć implementacji i kończy się kodem błędu, jeżeli którykolwiek test nie
przejdzie.

Uruchomienie:

```powershell
dotnet run --project .\Tests\run.csproj
```

Kompilacja:

```powershell
dotnet build .\Tests\run.csproj
```

## Założenia i walidacje

- Dijkstra odrzuca ujemne wagi, ponieważ algorytm nie obsługuje takich grafów.
- Kruskal obsługuje ujemne wagi i równoległe krawędzie.
- Dijkstra zgłasza błąd, gdy wierzchołek startowy nie należy do grafu.
- Algorytmy sortowania nie modyfikują tablic wejściowych i zwracają posortowane kopie.
- Operacje na pustych strukturach danych zwracają bezpieczne wyniki.
- Metody publiczne zgłaszają błędne argumenty zamiast ukrywać problem.
