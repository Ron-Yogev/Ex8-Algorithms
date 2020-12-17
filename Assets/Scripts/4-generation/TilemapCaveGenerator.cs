using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;


/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class TilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;
    [SerializeField] TileBase secondFloorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 100;

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    private CaveGenerator caveGenerator;

    void Start()  {
        //To get the same random numbers each time we run the script
        Random.InitState(100);
        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();
                
        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());
            
        //Start the simulation
        StartCoroutine(SimulateCavePattern());
    }


    //Do the simulation in a coroutine so we can pause and see what's going on
    private IEnumerator SimulateCavePattern()  {
        for (int i = 0; i < simulationSteps; i++)   {
            yield return new WaitForSeconds(pauseTime);

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        }


        Debug.Log("Simulation completed!");
    }



    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        twoTilesChanger(data);
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++)
            {
                var position = new Vector3Int(x, y, 0);
                //var tile = data[x, y] == 1 ? wallTile : floorTile;
                var tile = wallTile;
                if (data[x, y] ==0) tile = floorTile;
                if (data[x, y] != 1) {
                    if(!neighbours(data, x, y, data[x, y]))
                    {
                        if (data[x, y] == 0) data[x, y] = 2;
                        else data[x, y] = 0;
                        if (data[x, y] == 2)
                        {
                            tile = secondFloorTile;
                        }
                    }
                    else
                    {
                        if (data[x, y] == 0) tile = floorTile;
                        else tile = secondFloorTile;
                    }
                }
                else
                {
                    if (neighbours(data, x, y, data[x, y])){
                        tile = wallTile;
                    }
                }
                tilemap.SetTile(position, tile);
            }
        }
    }

    // checking if the till git neighbour that is the same tile
    public bool neighbours(int[,] data,int x,int y, int value)
    {
        if (y + 1 < gridSize && x + 1 < gridSize && x-1>=0 && y-1>=0 && 
            (data[x + 1, y] == value ||
             data[x - 1, y] == value ||
             data[x, y + 1] == value ||
             data[x, y - 1] == value)){
            return true;
        }
        return false;
    }


    //this function changer the floor randomly(p(1/2)) two floors 
    private void twoTilesChanger(int[,] data)
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (data[x, y] == 0)
                {
                    int random = Random.Range(1, 3);
                    data[x, y] = (random == 1) ? 2 : 0;
                }
            }
        }
    }
}
