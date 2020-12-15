using System.Collections.Generic;
using UnityEngine.Tilemaps;

/**
 * An abstract graph.
 * It does not use any memory.
 * It only has a single abstract function Neighbors, that returns the neighbors of a given node.
 * T = type of node in graph.
 * @author Erel Segal-Halevi
 * @since 2020-12
 */
public interface WeightedGraph<T>:IGraph<T>
{
    int GetWeight(T node1);
    Tilemap GetTilemap();
    }
