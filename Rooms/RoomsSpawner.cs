using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] startRoomsPrefabs;
    [SerializeField]
    private GameObject[] enemyRoomsPrefabs;
    [SerializeField]
    private GameObject[] shopRoomsPrefabs;
    [SerializeField]
    private GameObject[] bossRoomsPrefabs;

    public DungeonRoom SpawnRoomForTile(MapTile mapTile) 
    {
        GameObject[] roomsPrefabs = ChooseRoomArrayByRole(mapTile.GetRole());

        int roomIndex = Random.Range(0, roomsPrefabs.Length);

        GameObject roomInstance = Instantiate(roomsPrefabs[roomIndex], Vector3.zero, Quaternion.identity);
        DungeonRoom dungRoom = roomInstance.GetComponent<DungeonRoom>();

        dungRoom.SetupRoom(mapTile.entries);

        return dungRoom;
    }

    private GameObject[] ChooseRoomArrayByRole(TileRole tileRole) 
    {
        switch (tileRole) 
        {
            default:
            case TileRole.START:
                return startRoomsPrefabs;
            case TileRole.ENEMY:
                return enemyRoomsPrefabs;
            case TileRole.SHOP:
                return shopRoomsPrefabs;
            case TileRole.BOSS:
                return bossRoomsPrefabs;
        }   
    }
}
