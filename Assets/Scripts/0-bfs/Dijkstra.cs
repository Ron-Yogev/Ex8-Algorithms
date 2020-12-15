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
        //Queue<NodeType> openQueue = new Queue<NodeType>();
        Dictionary<NodeType, bool> visited = new Dictionary<NodeType, bool>();
        Dictionary<NodeType, NodeType> prev = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, int> distance = new Dictionary<NodeType, int>();
        distance.Add(startNode, 0);
        //openQueue.Enqueue(startNode);
        NodeType v = startNode;
        
        Debug.Log("Key Enqueue:" + startNode);
        int i = 0;
        while(!v.Equals(endNode) && i < maxiterations)
        {
            
            foreach (var neighbor in graph.Neighbors(v))
            {
                int dis = distance[v] + graph.GetWeight(neighbor);
                if ((visited.ContainsKey(neighbor) && distance[neighbor] > dis) || (!visited.ContainsKey(neighbor)))
                {
                    distance[neighbor] = dis;
                    prev[neighbor] = v;
                }
            }
            visited[v] = true;
            v = ExtractMin(distance, visited);
            /*for (i = 0; i < maxiterations; ++i)
            { // After maxiterations, stop and return an empty path
                if (openQueue.Count == 0)
                {
                    break;
                }
                else
                {
                    v = ExctractMin(distance, visited);
                    Debug.Log("Key  min neigbor Dequeue:" + v);

                    if (searchFocus.Equals(endNode))
                    {
                        // We found the target -- now construct the path:
                        outputPath.Add(endNode);
                        while (previous.ContainsKey(searchFocus))
                        {
                            searchFocus = previous[searchFocus];
                            outputPath.Add(searchFocus);
                        }
                        outputPath.Reverse();
                        break;
                    }
                    else
                    {
                        Dictionary<NodeType, int> neighbor_dis = new Dictionary<NodeType, int>();

                        // We did not found the target yet -- develop new nodes.
                        foreach (var neighbor in graph.Neighbors(searchFocus))
                        {
                            if (closedSet.Contains(neighbor))
                            {
                                continue;
                            }


                            int dis = distance[searchFocus] + graph.GetWeight(neighbor);


                            if (!distance.ContainsKey(neighbor))
                            {
                                distance[neighbor] = dis;
                                neighbor_dis[neighbor] = dis;
                            }
                            else
                            {
                                if(distance[neighbor] > dis)
                                {
                                    distance[neighbor] = dis;
                                    neighbor_dis[neighbor] = dis;
                                }
                            }

                            previous[neighbor] = searchFocus; // father of the negibor
                        }
                        NodeType n = GetMinNeighbor(neighbor_dis);
                        openQueue.Enqueue(n);
                        Debug.Log("Key min neighbor Enqueue:" + n);
                        closedSet.Add(searchFocus); // is black
                    }
                }
            }*/
        }

    /*private static NodeType GetMinNeighbor<NodeType>(Dictionary<NodeType, int> neighbor_dis)
    {
        
        ICollection<NodeType> c = neighbor_dis.Keys;
        NodeType[] narray = new NodeType[neighbor_dis.Count];
        c.CopyTo(narray, 0);
        Debug.Log("array length:" +narray.Length);
        NodeType node = narray[0];
        int min = neighbor_dis[node];
        foreach (var neighbor in neighbor_dis)
        {   
            if(min > neighbor.Value)
            {
                min = neighbor.Value;
                node = neighbor.Key;
            }
        }
        return node;
    }*/

    static List<NodeType> GetPath<NodeType>(WeightedGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000)
    {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }

}

    private static NodeType ExtractMin<NodeType>(Dictionary<NodeType, int> distance, Dictionary<NodeType, bool> visited)
    {
        ICollection<NodeType> keys = distance.Keys;
        NodeType[] keysArray = new NodeType[distance.Count];
        keys.CopyTo(keysArray, 0);
        ICollection<int> values = distance.Values;
        int[] valuesArray = new int[distance.Count];
        keys.CopyTo(keysArray, 0);
        values.CopyTo(valuesArray, 0);
        Debug.Log("array length:" + keysArray.Length);
        NodeType node = keysArray[0];
        int min = valuesArray[0];
        foreach(var v in distance)
        {
            if(!visited[v.Key] && v.Value< min)
            {
                node = v.Key;
            }
        }
        return node;
    }
}
