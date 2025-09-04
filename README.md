# Network_Library

Network_Library is a comprehensive C# library for working with networks, graphs, and graph algorithms. It is designed for students, researchers, and developers who need efficient and flexible tools for graph theory tasks, including modeling, analysis, and algorithmic operations.

## Features

- Create and manipulate oriented and weighted graphs.
- Work with vertices, edges, and custom graph structures.
- Serialize and deserialize graphs to/from JSON.
- Run classic graph algorithms: BFS, DFS, shortest paths, topological sorting, strongly connected components, and more.
- Handle graph-specific exceptions and edge cases.
- Extend graph functionality with custom delegates and extension methods.

## Technologies Used

- C# (.NET 7.0)
- Object-oriented design with interfaces and extensions
- Custom exception handling
- JSON serialization via custom converters
- Unit testing with .NET test framework

## Example Usage

```csharp
using GraphLibrary.Graphs;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;

// Create a new oriented graph
var graph = new OrientedGraph<string>();

// Add vertices
graph.AddVertex("A");
graph.AddVertex("B");

// Add an edge
graph.AddEdge(new OrientedEdge<string>("A", "B"));

// Run BFS
var bfsResult = Algorithms.Bfs(graph, "A");
```

## Running Tests

To run the tests, use the following command in the project root:

```bash
dotnet test GraphLibraryTests/GraphLibraryTests.csproj
```

## How to Run

To use the library in your own project, reference the `GraphLibrary` project in your solution. You can build the library with:

```bash
dotnet build GraphLibrary/GraphLibrary.csproj
```
