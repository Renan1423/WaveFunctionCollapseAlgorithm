using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [Tooltip("Use the following sequence: TOP, RIGHT, BOTTOM, LEFT")]
    [SerializeField]
    private GameObject[] entries = new GameObject[4];

    public void SetupRoom(int[] mapTileEntries)
    {
        for (int i = 0; i < mapTileEntries.Length; i++) 
        {
            if (entries[i] == null)
                continue;

            if (mapTileEntries[i] == 0)
            {
                entries[i].SetActive(false);
                continue;
            }
        }
    }

    public GameObject[] GetRoomEntries() 
    {
        return entries;
    }
}
