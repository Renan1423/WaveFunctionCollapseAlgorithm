using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTilesGrid : MonoBehaviour
{
    public MapTile[,] tilesGrid;
    public int gridLength = 30;

    private void Awake()
    {
        InitializeGrid();
    }

    public void InitializeGrid() 
    {
        if (tilesGrid != null)
            ClearGrid();

        tilesGrid = new MapTile[gridLength, gridLength];
    }

    private void ClearGrid() 
    {
        for (int i = 0; i < tilesGrid.GetLength(0); i++)
        {
            for (int j = 0; j < tilesGrid.GetLength(1); j++)
            {
                if (tilesGrid[i, j] != null) 
                {
                    tilesGrid[i, j].DestroyTileRoom();
                    Destroy(tilesGrid[i, j].gameObject);
                }
            }
        }
    }

    public bool IsPositionOccupied(int x, int y)
    {
        if (x >= -gridLength/2 && x < tilesGrid.GetLength(0) && y >= -gridLength / 2 && y < tilesGrid.GetLength(1))
        {
            return tilesGrid[x + (gridLength/2), y + (gridLength/2)] != null;
        }
        
        return true;
    }
}
