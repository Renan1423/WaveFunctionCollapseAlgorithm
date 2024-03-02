using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTilesContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mapTiles;
    [HideInInspector]
    public List<GameObject> topEntryMapTiles, rightEntryMapTiles, bottomEntryMapTiles, leftEntryMapTiles;
    public List<GameObject> oneEntryTiles;

    private void Awake()
    {
        GenerateMapTilesLists();
    }

    private void GenerateMapTilesLists() 
    {
        GetMapTilesByEntry(0, out topEntryMapTiles);
        GetMapTilesByEntry(1, out rightEntryMapTiles);
        GetMapTilesByEntry(2, out bottomEntryMapTiles);
        GetMapTilesByEntry(3, out leftEntryMapTiles);
    }

    //Use the following sequence: TOP, RIGHT, BOTTOM, LEFT
    private void GetMapTilesByEntry(int entry, out List<GameObject> newList)
    {
        newList = new List<GameObject>();

        foreach (GameObject mapTile in mapTiles)
        {
            if (mapTile.TryGetComponent<MapTile>(out MapTile mt))
            {
                bool shouldAdd;

                switch (entry)
                {
                    default:
                    case 0:
                        shouldAdd = HasTopEntry(mt.entries);
                        break;
                    case 1:
                        shouldAdd = HasRightEntry(mt.entries);
                        break;
                    case 2:
                        shouldAdd = HasBottomEntry(mt.entries);
                        break;
                    case 3:
                        shouldAdd = HasLeftEntry(mt.entries);
                        break;
                }

                if (shouldAdd)
                    newList.Add(mapTile);
            }
        }
    }

    public bool HasTopEntry(int[] entries)
    {
        return entries[0] == 1;
    }

    public bool HasRightEntry(int[] entries)
    {
        return entries[1] == 1;
    }

    public bool HasBottomEntry(int[] entries)
    {
        return entries[2] == 1;
    }

    public bool HasLeftEntry(int[] entries)
    {
        return entries[3] == 1;
    }

    public GameObject GetFirstTile() 
    {
        return mapTiles[0];
    }
}
