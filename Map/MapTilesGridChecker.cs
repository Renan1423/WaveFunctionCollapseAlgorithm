using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTilesGridChecker : MonoBehaviour
{
    public static MapTilesGridChecker instance;
    private MapTilesGrid grid;

    private void Awake()
    {
        instance = this;
        grid = GetComponent<MapTilesGrid>();
    }

    public bool HasEntryInPosition(int _posX, int _posY, int entryToCheck)
    {
        int offset = (grid.gridLength / 2);

        if (!(grid.tilesGrid[_posX + offset, _posY + offset]))
        {
            return false;
        }

        return (grid.tilesGrid[_posX + offset, _posY + offset].entries[entryToCheck] == 1);
    }
}
