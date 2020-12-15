using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

     Dictionary<TileBase, int> tilesWeight = null;
    [SerializeField] List<TileBase> _Keys;
    [SerializeField] List<int> _Values;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget = true;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget) {
        if (targetInWorld != newTarget) {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    //private TilemapGraph tilemapGraph = null;
    private TilemapWeightedGraph tilemapWeightedGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start() {
        //tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        tilesWeight = new Dictionary<TileBase, int>();
        for (int i =0; i< _Keys.Count; i++)
        {
            Debug.Log("mcksdmcksdmkcsdmkcsd");
            Debug.Log("Key:" + _Keys[i] + ", Value:" + _Values[i]);
            tilesWeight.Add(_Keys[i], _Values[i]);
            Debug.Log(tilesWeight[_Keys[i]]);
        }
        tilemapWeightedGraph = new TilemapWeightedGraph(tilemap, allowedTiles.Get(),tilesWeight);
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget() {
        for(;;) {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = Dijkstra.GetPath(tilemapWeightedGraph, startNode, endNode, maxIterations);
        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));
        if (shortestPath.Count >= 2) {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        } else {
            atTarget = true;
        }
    }
}
