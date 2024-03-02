using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    private MapTile currentTile;
    private int posX, posY;

    private MapTilesGridChecker mapTilesChecker;
    private MapTilesGrid grid;
    private PlayerSpawner playerSpawner;

    private void OnEnable()
    {
        mapTilesChecker = MapTilesGridChecker.instance;
        grid = GetComponent<MapTilesGrid>();
        playerSpawner = GetComponent<PlayerSpawner>();

        SetNewCurrentTile(posX, posY);
        playerSpawner.enabled = true;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            MoveHorizontally(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveHorizontally(1);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            MoveVertically(1);
        }
        else if (Input.GetKeyDown(KeyCode.S)) 
        {
            MoveVertically(-1);
        }*/
    }

    public void MoveHorizontally(int dirX) 
    {
        bool hasConnectionOnRight = mapTilesChecker.HasEntryInPosition(posX + dirX, posY, 3);
        bool canMoveRight = dirX > 0 && currentTile.entries[1] == 1 && hasConnectionOnRight;
        bool hasConnectionOnLeft = mapTilesChecker.HasEntryInPosition(posX + dirX, posY, 1);
        bool canMoveLeft = dirX < 0 && currentTile.entries[3] == 1 && hasConnectionOnLeft;

        if (canMoveRight
            || canMoveLeft) 
        {
            posX += dirX;
            SetNewCurrentTile(posX, posY);
            playerSpawner.SpawnPlayerFromDirection(currentTile, dirX, 0);
        }
    }

    public void MoveVertically(int dirY) 
    {
        bool hasConnectionOnTop = mapTilesChecker.HasEntryInPosition(posX, posY + dirY, 2);
        bool canMoveUp = dirY > 0 && currentTile.entries[0] == 1 && hasConnectionOnTop;

        bool hasConnectionOnBottom = mapTilesChecker.HasEntryInPosition(posX, posY + dirY, 0);
        bool canMoveDown = dirY < 0 && currentTile.entries[2] == 1 && hasConnectionOnBottom;

        if (canMoveUp
            || canMoveDown)
        {
            posY += dirY;
            SetNewCurrentTile(posX, posY);
            playerSpawner.SpawnPlayerFromDirection(currentTile, 0, dirY);
        }
    }

    public bool CanMoveHorizontally(int dirX)
    {
        bool result = false;

        if (dirX > 0)
        {
            bool hasConnectionOnRight = mapTilesChecker.HasEntryInPosition(posX + dirX, posY, 3);
            result = currentTile.entries[1] == 1 && hasConnectionOnRight;
        }
        else if (dirX < 0)
        {
            bool hasConnectionOnLeft = mapTilesChecker.HasEntryInPosition(posX + dirX, posY, 1);
            result = currentTile.entries[3] == 1 && hasConnectionOnLeft;
        }

        return result;
    }

    public bool CanMoveVertically(int dirY)
    {
        bool result = false;

        if (dirY > 0)
        {
            bool hasConnectionOnTop = mapTilesChecker.HasEntryInPosition(posX, posY + dirY, 2);
            result = currentTile.entries[0] == 1 && hasConnectionOnTop;
        }
        else if (dirY < 0) 
        {
            bool hasConnectionOnBottom = mapTilesChecker.HasEntryInPosition(posX, posY + dirY, 0);
            result = currentTile.entries[2] == 1 && hasConnectionOnBottom;
        }

        return result;
    }

    private void MoveToTile(int _posX, int _posY) 
    {
        SetNewCurrentTile(_posX, _posY);
    }

    private MapTile SetNewCurrentTile(int _posX, int _posY) 
    {
        MapTile previousTile = currentTile;

        currentTile = grid.tilesGrid[_posX + (grid.gridLength / 2), _posY + (grid.gridLength / 2)];
        if (currentTile == null)
            currentTile = previousTile;
        else 
        {
            currentTile.ToggleSelected();
            if(previousTile)
                previousTile.ToggleSelected();
        }

        return currentTile;
    }

    public MapTile GetCurrentTile() 
    {
        return currentTile;
    }
}
