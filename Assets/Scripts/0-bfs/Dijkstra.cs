using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A generic implementation of the BFS algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public class Dijkstra
{

    public static void FindPath<NodeType>(
            WeightedGraph<NodeType> graph,
            NodeType startNode, NodeType endNode,
            List<NodeType> outputPath, int maxiterations = 1000)
    {

        LinkedList<NodeType> visited = new LinkedList<NodeType>();
        Dictionary<NodeType, NodeType> prev = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, int> distance = new Dictionary<NodeType, int>();
        distance.Add(startNode, 0);
        NodeType v = startNode;
        visited.AddLast(startNode);
        Debug.Log("Key Enqueue:" + startNode);
        int i = 0;
        while (!v.Equals(endNode) && i < maxiterations)
        {
            v = ExtractMin(distance, visited);
            foreach (var neighbor in graph.Neighbors(v))
            {
                int dis = distance[v] + graph.GetWeight(neighbor);
                if ((visited.Contains(neighbor) && distance[neighbor] > dis) || (!visited.Contains(neighbor)))
                {
                    distance[neighbor] = dis;
                    prev[neighbor] = v;
                }
            }
            visited.AddFirst(v);
            distance.Remove(v);
                       // v = ExtractMin(distance, visited);

            Debug.Log(v.ToString());
            i++;
        }
        outputPath = CreatePath(prev, startNode, endNode);
        
    }

    private static List<NodeType> CreatePath<NodeType>(Dictionary<NodeType, NodeType> prev, NodeType startNode, NodeType endNode) 
    {
        List<NodeType> ans = new List<NodeType>();
        NodeType temp = endNode;
        int i = 0;
        while(!prev[temp].Equals(startNode))
        {
            ans.Add(temp);
            temp = prev[temp];
            Debug.Log("Create path "+i++);
        }
        ans.Reverse();
        return ans;
    }

    public static List<NodeType> GetPath<NodeType>(WeightedGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000)
    {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }

    private static NodeType ExtractMin<NodeType>(Dictionary<NodeType, int> distance, LinkedList<NodeType> visited)
    {
        //ICollection<NodeType> keys = distance.Keys;
        //NodeType[] keysArray = new NodeType[distance.Count];
        //keys.CopyTo(keysArray, 0);
        //ICollection<int> values = distance.Values;
        //int[] valuesArray = new int[distance.Count];
        //keys.CopyTo(keysArray, 0);
        //values.CopyTo(valuesArray, 0);
        //Debug.Log("array length:" + keysArray.Length);
        NodeType node = visited.First.Value;
        int min = 1000000;
        foreach(var v in distance)
        {
            if(!visited.Contains(v.Key) && v.Value<= min)
            {
                node = v.Key;
            }
            Debug.Log("Length " +v.Key.ToString());
        }
        Debug.Log("Length out of for" + node.ToString());
        return node;
    }
}
