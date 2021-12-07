using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreezeEvent : MonoBehaviour
{
    private int activate = 2;
    public Tilemap tilemap;
    public TileBase frozenTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EventHandler.Instance.eventId == activate)
        {
            Debug.Log("Gel activé");
            int looptimes = 0;
            Vector3Int v = new Vector3Int((int)UnityEngine.Random.Range(-7, 6), (int)UnityEngine.Random.Range(-4, 1), 0);
            while (tilemap.GetTile(v) == frozenTile && tilemap.GetTile(v + Vector3Int.right) == frozenTile && tilemap.GetTile(v + Vector3Int.up) == frozenTile && tilemap.GetTile(v + Vector3Int.right + Vector3Int.up) == frozenTile && looptimes < 100)
            {
                v.x = (int)UnityEngine.Random.Range(-7, 6);
                v.y = (int)UnityEngine.Random.Range(-4, 1);
                looptimes++;
            }
            if (looptimes < 100)
            {
                tilemap.SetTile(v + Vector3Int.right, frozenTile);
                tilemap.SetTile(v + Vector3Int.up, frozenTile);
                tilemap.SetTile(v + Vector3Int.up + Vector3Int.right, frozenTile);
                tilemap.SetTile(v, frozenTile);
            }
            else
                Debug.LogError("Zone non-gelée non trouvée.");
            EventHandler.Instance.eventId = 0;
        }
    }
}
