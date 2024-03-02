using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomTeleport : MonoBehaviour, IInteractable
{
    private MiniMapPlayer miniMapPlayer;
    private bool firstEnabled;

    [SerializeField]
    private int dirX, dirY;

    private void OnEnable()
    {
        if (!firstEnabled) 
        {
            firstEnabled = true;
            return;
        }

        miniMapPlayer = FindObjectOfType<MiniMapPlayer>();

        if (!IsMovementPossible())
        {
            gameObject.SetActive(false);
        }
        else 
        {
            EnableMapTileBridge();
        }

    }

    private bool IsMovementPossible()
    {
        bool canTeleport = false;

        if (dirX != 0)
        {
            canTeleport = miniMapPlayer.CanMoveHorizontally(dirX);
        }
        else if (dirY != 0) 
        {
            canTeleport = miniMapPlayer.CanMoveVertically(dirY);
        }
        
        return canTeleport;
    }

    private void EnableMapTileBridge() 
    {
        MapTile currentTile = miniMapPlayer.GetCurrentTile();
        List<GameObject> bridges = currentTile.GetTileBridges();

        int index = 0;
        if(dirX != 0)
            index = (dirX > 0) ? 1 : 3;
        else if (dirY != 0)
            index = (dirY > 0) ? 0 : 2;

        bridges[index].SetActive(true);
    }

    public void SetOnFocus(bool focused)
    {
        
    }

    public void OnInteract(GameObject interactionOwner)
    {
        if (dirX != 0)
            miniMapPlayer.MoveHorizontally(dirX);
        else if (dirY != 0)
            miniMapPlayer.MoveVertically(dirY);
    }
}
