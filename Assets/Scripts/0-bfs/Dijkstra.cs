using System;
using System.Collections.Generic;

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
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> closedSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, int> distance = new Dictionary<NodeType, int>();
        distance[startNode] = 0;
        openQueue.Enqueue(startNode);
        int i; for (i = 0; i < maxiterations; ++i)
        { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0)
            {
                break;
            }
            else
            {
                NodeType searchFocus = openQueue.Dequeue();

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
                    NodeType n = neighbor_dis;
                    GetMinNeighbor(neighbor_dis, n);
                    openQueue.Enqueue(n);
                    closedSet.Add(searchFocus); // is black
                }
            }
        }
    }

    private static void GetMinNeighbor<NodeType>(Dictionary<NodeType, int> neighbor_dis, NodeType node)
    {
        int min = Int32.MaxValue;
        foreach (var neighbor in neighbor_dis)
        {   
            if(min > neighbor.Value)
            {
                min = neighbor.Value;
                node = neighbor.Key;
            }
        }
    }

    public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000)
    {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }

}