using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject playerGO;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Vector3 initialPosition;
    [SerializeField]
    private CinemachineVirtualCamera gameplayCamera;
    [SerializeField]
    private float delayToSpawnPlayer = 0.5f;

    private void OnEnable()
    {
        playerGO = Instantiate(playerPrefab, initialPosition, Quaternion.identity);
        gameplayCamera.Follow = playerGO.transform;
    }

    public void SpawnPlayerFromDirection(MapTile newCurrentTile, int dirX, int dirY) 
    {
        int entryToChoose = 0;

        if (dirX != 0)
        {
            if (dirX > 0)
            {
                //Right Direction -> Left Entry;
                entryToChoose = 3;
            }
            else if (dirX < 0)
            {
                //Left Direction -> Right Entry
                entryToChoose = 1;
            }
        }
        else if (dirY != 0) 
        {
            if (dirY > 0)
            {
                //Up Direction -> Bottom Entry;
                entryToChoose = 2;
            }
            else if (dirY < 0)
            {
                //Down Direction -> Top Entry
                entryToChoose = 0;
            }
        }

        StartCoroutine(SpawnPlayerWithDelay(newCurrentTile.tileRoom.GetRoomEntries()[entryToChoose].transform.position));
    }

    private IEnumerator SpawnPlayerWithDelay(Vector3 positionToSpawn) 
    {
        playerGO.SetActive(false);

        yield return new WaitForSeconds(delayToSpawnPlayer);

        playerGO.SetActive(true);
        playerGO.transform.position = positionToSpawn;
    }
}
