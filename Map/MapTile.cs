using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileRole 
{
    ENEMY,
    START,
    SHOP,
    BOSS,
}

public class MapTile : MonoBehaviour
{
    [Tooltip("Use the following sequence: TOP, RIGHT, BOTTOM, LEFT")]
    public int[] entries = new int[4];

    [HideInInspector]
    public DungeonRoom tileRoom;

    private TileRole role = TileRole.ENEMY;

    private Animator anim;
    [SerializeField]
    private SpriteRenderer gfx;

    [Space(5)]
    [Header("Roles")]
    private Dictionary<TileRole, GameObject> rolesImgs;
    [SerializeField]
    [Tooltip("Use the following sequence: ENEMY, START, SHOP and BOSS")]
    private List<GameObject> rolesImgsRefs;

    [Space(5)]
    [Header("Bridges")]
    [SerializeField]
    [Tooltip("Use the following sequence: TOP, RIGHT, BOTTOM, LEFT")]
    private List<GameObject> bridges;

    private void Awake()
    {
        SetupRolesGO();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        gfx.color = new Color(gfx.color.r, gfx.color.g, gfx.color.b, 0.25f);

        SpawnRoom();
    }

    private void SpawnRoom() 
    {
        RoomsSpawner roomsSpawner = FindObjectOfType<RoomsSpawner>();
        tileRoom = roomsSpawner.SpawnRoomForTile(this);
        tileRoom.gameObject.SetActive(false);
    }

    private void SetupRolesGO() 
    {
        rolesImgs = new Dictionary<TileRole, GameObject>();
        for (int i = 0; i < rolesImgsRefs.Count; i++)
        {
            rolesImgs[(TileRole)i] = rolesImgsRefs[i];
            if(rolesImgsRefs[i])
                rolesImgsRefs[i].SetActive(false);
        }
    }

    public void ToggleSelected() 
    {
        anim.SetTrigger("Toggle");
        bool active = !tileRoom.gameObject.activeInHierarchy;
        if (role == TileRole.START)
            tileRoom.gameObject.SetActive(active);
        else
            StartCoroutine(ActivateTileRoomWithDelay(active));
        
        ShowTile();
    }

    private IEnumerator ActivateTileRoomWithDelay(bool active) 
    {
        yield return new WaitForSeconds(0.5f);

        tileRoom.gameObject.SetActive(active);
    }

    public void SetNewRole(TileRole newRole) 
    {
        role = newRole;
        SetupRolesGO();

        rolesImgs[role].SetActive(true);
    }

    public void DestroyTileRoom() 
    {
        Destroy(tileRoom.gameObject);
    }

    public TileRole GetRole() 
    {
        return role;
    }

    private void ShowTile() 
    {
        gfx.color = new Color(gfx.color.r, gfx.color.g, gfx.color.b, 1f);
    }

    public List<GameObject> GetTileBridges() 
    {
        return bridges;
    }
}
