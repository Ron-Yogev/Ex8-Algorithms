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

    private static Dictionary<NodeType, NodeType> DijekstraBuilder<NodeType>(WeightedGraph<NodeType> graph, NodeType source)
    {
        Dictionary<NodeType, int> graphCurrW = new Dictionary<NodeType, int>();
        Dictionary<NodeType, NodeType> Fathers = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, bool> visted = new Dictionary<NodeType, bool>();

        List<NodeType> Mins = new List<NodeType>();
        Mins.Add(source);
        graphCurrW.Add(source, 0);
        while (Mins.Count != 0)
        {
            NodeType currNode = Mins[0];
            if (!visted.ContainsKey(currNode))
            {
                visted.Add(currNode, true);
                Mins.RemoveAt(0);
                foreach (var neighbor in graph.Neighbors(currNode))
                {
                    if (graphCurrW.ContainsKey(neighbor))
                    {
                        // Debug.Log("27");
                        if (graphCurrW[neighbor] > graphCurrW[currNode] + graph.GetWeight(neighbor))
                        {
                            //   Debug.Log("30");
                            graphCurrW[neighbor] = graphCurrW[currNode] + graph.GetWeight(neighbor);
                            // Debug.Log("32");
                            Fathers[neighbor] = currNode;
                            if (!visted.ContainsKey(neighbor))
                            {
                                //  Debug.Log("36");
                                insertNeighbour(Mins, graphCurrW[neighbor], neighbor, graph);
                                //Mins.Insert(getPosInArray(Mins, graphCurrW[neighbor], graph), neighbor);
                            }

                        }
                    }
                    else
                    {
                        //  Debug.Log("44");
                        graphCurrW[neighbor] = graphCurrW[currNode] + graph.GetWeight(neighbor);
                        // Debug.Log("46");
                        Fathers[neighbor] = currNode;
                        if (!visted.ContainsKey(neighbor))
                        {
                            //   Debug.Log("50");
                            insertNeighbour(Mins, graphCurrW[neighbor], neighbor, graph);
                        }
                    }
                }
            }
            else
            {
                Mins.RemoveAt(0);
            }
        }
        return Fathers;
    }

    /*public static void FindPath<NodeType>(
            WeightedGraph<NodeType> graph,
            NodeType startNode, NodeType endNode,
            List<NodeType> outputPath, int maxiterations = 1000)
    {

        LinkedList<NodeType> visited = new LinkedList<NodeType>();
        Dictionary<NodeType, NodeType> prev = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, int> distance = new Dictionary<NodeType, int>();
        List<NodeType> sortedNeighbours = new List<NodeType>();

        distance.Add(startNode, 0);
        sortedNeighbours.Add(startNode);
        visited.AddLast(startNode);
        int i = 0;

        while (i < maxiterations && sortedNeighbours.Count != 0)
        {
            NodeType curr = sortedNeighbours[0];
            Debug.Log(curr.ToString());
            foreach (var neighbor in graph.Neighbors(curr))
            {
                int dis = distance[curr] + graph.GetWeight(neighbor);
                if ((visited.Contains(neighbor) && distance[neighbor] > dis) || (!visited.Contains(neighbor)))
                {
                    distance[neighbor] = dis;
                    prev[neighbor] = curr;
                    if (!visited.Contains(neighbor))
                    {
                        insertNeighbour(sortedNeighbours, distance[neighbor], neighbor, graph);
                    }
                }

            }
            visited.AddFirst(curr);
            sortedNeighbours.RemoveAt(0);
            i++;
        }
        outputPath = CreatePath(prev, startNode, endNode);

    }*/

    public static List<NodeType> GetPath<NodeType>(WeightedGraph<NodeType> graph, NodeType source, NodeType dest)
    {
        List<NodeType> path = new List<NodeType>();
        Dictionary<NodeType, NodeType> route = DijekstraBuilder(graph, source);
        if (route.ContainsKey(dest))
        {
            NodeType tempD = dest;
            path.Insert(0, tempD); //

            while (!path[0].Equals(source))
            {
                //Debug.Log("END?");
                tempD = route[tempD];
                path.Insert(0, tempD);
            }
            // Debug.Log("return");
        }
        return path;
    }

    private static void insertNeighbour<NodeType>(List<NodeType> sortedNeighbours, int w, NodeType neighbor, WeightedGraph<NodeType> graph)
    {

        int minIndex = 0;
        int maxIndex = sortedNeighbours.Count - 1;
        int middle = minIndex + (maxIndex - minIndex) / 2;

        while (minIndex <= maxIndex)
        {
            if (graph.GetWeight(sortedNeighbours[middle]) == w)
            {
                sortedNeighbours.Insert(middle, neighbor);
                break;
            }
            if (w > graph.GetWeight(sortedNeighbours[middle]))
            {
                minIndex = middle + 1;
            }
            if (w < graph.GetWeight(sortedNeighbours[middle]))
            {
                maxIndex = middle - 1;
            }
            middle = minIndex + (maxIndex - minIndex) / 2;
        }
        /*
        int i = 0;
        int index=-1;
        while (i < sortedNeighbours.Count)
        {
            if (w > graph.GetWeight(sortedNeighbours[i])){
                index = i;
                break;
            }
            i++;
        }
        */
   
    }

   /*private static List<NodeType> CreatePath<NodeType>(Dictionary<NodeType, NodeType> prev, NodeType startNode, NodeType endNode) 
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
   */

    public static List<NodeType> GetPath<NodeType>(WeightedGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000)
    {
        // = new List<NodeType>();
        DijekstraBuilder(graph, startNode/*, endNode, path, maxiterations*/);
        List<NodeType> path = GetPath(graph, startNode, endNode);
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
