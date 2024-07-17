using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Graph
{
    private static List<Vector2Int> neighbours4directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1),  // UP
        new Vector2Int(1, 0),  // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0)  // LEFT
    };

    private static List<Vector2Int> neighbours8directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1),  // UP
        new Vector2Int(1, 0),  // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0), // LEFT
        new Vector2Int(1, 1),  // Diagonal
        new Vector2Int(1, -1), // Diagonal
        new Vector2Int(-1, 1), // Diagonal
        new Vector2Int(-1, -1) // Diagonal
    };

    List<Vector2Int> graph;

    public Graph(IEnumerable<Vector2Int> verticles)
    {
        graph = new List<Vector2Int>(verticles);
    }

    public List<Vector2Int> GetNeighbours4Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours4directions);
    }
    public List<Vector2Int> GetNeighbours8Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours8directions);
    }

    private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        foreach (var neighbourDirection in neighboursOffsetList)
        {
            Vector2Int potentialNeoghbour = startPosition + neighbourDirection;
            if (graph.Contains(potentialNeoghbour))
                neighbours.Add(potentialNeoghbour);
        }
        return neighbours;
    }

}
