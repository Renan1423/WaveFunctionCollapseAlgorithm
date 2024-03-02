using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTilesSpawner : MonoBehaviour
{
    private MapTilesContainer container;
    private MapTilesGrid grid;
    private MiniMapPlayer miniMapPlayer;

    [Header("Dungeon Settings")]
    [SerializeField]
    private int maxRoomsCount = 10;
    private int roomsCount = 0;
    private List<MapTile> mapTilesList;

    [Space(5)]
    [SerializeField]
    private bool spawnOneEntryTileAtEnd;

    [Space(5)]
    [SerializeField]
    private int shopsAmount = 2;

    private void Start()
    {
        container = GetComponent<MapTilesContainer>();
        grid = GetComponent<MapTilesGrid>();
        miniMapPlayer = GetComponent<MiniMapPlayer>();

        StartDungeonGeneration();
    }

    private void StartDungeonGeneration() 
    {
        roomsCount = 0;
        mapTilesList = new List<MapTile>();
        SpawnTileAtPosition(container.GetFirstTile(), 0, 0);
        Invoke(nameof(FinishDungeonGeneration), 0.5f);
    }

    private void SpawnTileAtPosition(GameObject tile, int x, int y) 
    {
        if (grid.IsPositionOccupied(x, y))
            return;

        MapTile mapTileRef = tile.GetComponent<MapTile>();
        if (!mapTileRef)
            return;

        roomsCount++;

        GameObject tileToSpawn = Instantiate(tile, new Vector3(transform.position.x + x, transform.position.y + y, 0f), Quaternion.identity);
        tileToSpawn.transform.SetParent(this.transform);
        grid.tilesGrid[x + (grid.gridLength / 2), y + (grid.gridLength / 2)] = tileToSpawn.GetComponent<MapTile>();

        mapTilesList.Add(tileToSpawn.GetComponent<MapTile>());

        StartCoroutine(TrySpawnNextTileWithDelay(mapTileRef, x, y));
        //TrySpawnNextTile(mapTileRef, x, y);
    }

    private IEnumerator TrySpawnNextTileWithDelay(MapTile mapTile, int currentPosX, int currentPosY) 
    {
        yield return new WaitForSeconds(0.05f);

        TrySpawnNextTile(mapTile, currentPosX, currentPosY);
    }

    private void TrySpawnNextTile(MapTile mapTile, int currentPosX, int currentPosY) 
    {
        for (int i = 0; i < mapTile.entries.Length; i++) 
        {
            if (mapTile.entries[i] == 0)
                continue;

            if (roomsCount >= maxRoomsCount) 
            {
                if(spawnOneEntryTileAtEnd)
                    SpawnTileAtPosition(ChooseOneEntryTile(i),
                        GetNextXPosition(i, currentPosX),
                        GetNextYPosition(i, currentPosY));
                break;
            }

            SpawnTileAtPosition(ChooseTile(i), 
                GetNextXPosition(i, currentPosX), 
                GetNextYPosition(i, currentPosY));
        }
    }

    private GameObject ChooseTile(int entry) 
    {
        GameObject theChosenOne = null;
        int index;
        List<GameObject> entryTiles = new List<GameObject>();

        switch (entry) 
        {
            case 0:
                //Top -> Bottom
                entryTiles = container.bottomEntryMapTiles;
                break;
            case 1:
                //Right -> Left
                entryTiles = container.leftEntryMapTiles;
                break;
            case 2:
                //Bottom -> Top
                entryTiles = container.topEntryMapTiles;
                break;
            case 3:
                //Left -> Right
                entryTiles = container.rightEntryMapTiles;
                break;
        }

        index = Random.Range(0, entryTiles.Count);
        theChosenOne = entryTiles[index];

        return theChosenOne;
    }

    private GameObject ChooseOneEntryTile(int entry)
    {
        GameObject theChosenOne = null;

        theChosenOne = container.oneEntryTiles[entry];

        return theChosenOne;
    }


    private int GetNextXPosition(int entry, int posX) 
    {
        switch (entry)
        {
            //Right
            case 1:
                posX += 1;
                break;
            //Left
            case 3:
                posX -= 1;
                break;
        }

        return posX;
    }

    private int GetNextYPosition(int entry, int posY) 
    {
        switch (entry)
        {
            //Top
            case 0:
                posY += 1;
                break;
            //Bottom
            case 2:
                posY -= 1;
                break;
        }

        return posY;
    }

    private void FinishDungeonGeneration() 
    {
        //Reseting incomplete dungeon
        if (roomsCount < maxRoomsCount)
            ResetDungeonGeneration();
        else 
        {
            miniMapPlayer.enabled = true;
            SetupTilesRoles();
        }
    }

    private void SetupTilesRoles() 
    {
        mapTilesList[0].SetNewRole(TileRole.START);
        mapTilesList[^1].SetNewRole(TileRole.BOSS);

        //Shop roles
        int shopCount = 0;

        while (shopCount < shopsAmount) 
        {
            foreach (MapTile mapTile in mapTilesList)
            {
                if (mapTile.GetRole() != TileRole.ENEMY ||
                    shopCount >= shopsAmount) 
                    continue;

                int rand = Random.Range(0, 101);
                if (rand <= 25)
                {
                    shopCount++;
                    mapTile.SetNewRole(TileRole.SHOP);
                }
            }

            if (shopCount >= shopsAmount)
                break;
        }
    }

    private void ResetDungeonGeneration() 
    {
        Debug.Log("MapTilesSpawner: Number of spawned rooms is lesser than the expected(" + roomsCount + "). Reseting...");
        grid.InitializeGrid();
        StopAllCoroutines();
        StartDungeonGeneration();
    }
}
