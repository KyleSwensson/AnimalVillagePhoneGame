using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public int height = 10; // height of ground
    public int width = 120; // how many tiles wide ground is
    public GameObject grassTile;
    public GameObject dirtTile;
    public GameObject genericNPC;
    public GameObject fenceTile;
    public GameObject[] pickupArray;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                // this is creating a list of spaces where walls pickups etc can be placed
                gridPositions.Add(new Vector3(x, y, 0f));
                // the loops dont go from 0 to rows and instead from 1 to columns - 1 is because
                // we want a clear loop around the outside of the screen

            }
        }
    }


    void BoardSetup()
    {
        // sets up outer wall and floor of game
        boardHolder = new GameObject("Board").transform;


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // these go to cols + 1 because the edge is around the outer edge of the game tiles
                //prepares to instantiate a floortile using a random floorTile from floorTiles
                GameObject toInstantiate = dirtTile;
                if (y == height - 1)
                {
                    // if tile is on top edge instantiate grass tile instead
                    toInstantiate = grassTile;

                }


                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }

        //This is to spawn the two fence tiles to keep everyone in the same place
        // to spawn first fence tile
        Vector3 fenceVector = new Vector3(5, height);
        GameObject instanceF = Instantiate(fenceTile, fenceVector, Quaternion.identity) as GameObject;
        instanceF.transform.SetParent(boardHolder);
        // for second fence tile
        Vector3 fenceVector2 = new Vector3(width - 5, height);
        GameObject instanceF2 = Instantiate(fenceTile, fenceVector2, Quaternion.identity) as GameObject;
        instanceF2.transform.SetParent(boardHolder);
    }


    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1); // how many objects to spawn

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);

        }
    }
    void spawnPickups()
    {
        int pickupCount = Random.Range(4, 9);

        for (int i = 0; i < pickupCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(10, width - 10), 10f);
            GameObject pickupToInst = pickupArray[Random.Range(0, pickupArray.Length)];
            Instantiate(pickupToInst, randomPosition, Quaternion.identity);
        }
    }


    void LayoutNPCs(GameObject npc, int minNum, int maxNum)
    {
        //lays a number between min and max of given npc, later you should make many different types of npcs and put into a npc array

        int npcCount = Random.Range(minNum, maxNum + 1); // how many to spawn

        for (int i = 0; i < npcCount; i++)
        {
            Vector3 npcPos = new Vector3(Random.Range(10, width - 10), 10f);
            GameObject npcToSpawn = npc;
            Instantiate(npcToSpawn, npcPos, Quaternion.identity);
        }
    }

    public void SetupScene()
    {
        BoardSetup();
        InitializeList();
        LayoutNPCs(genericNPC, 3, 3);
        spawnPickups();
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        //LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        //int enemyCount = (int)Mathf.Log(level, 2f);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        //Instantiate(exit, new Vector3(columns - 1, rows - 1), Quaternion.identity);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}