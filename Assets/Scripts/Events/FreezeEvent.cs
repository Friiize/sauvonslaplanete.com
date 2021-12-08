using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreezeEvent : MonoBehaviour
{
    private int activate = 2;
    public Tilemap tilemap;
    public TileBase frozenTile;
    public int addedHitPoints = 3;

    // Update is called once per frame
    void Update()
    {
        if (EventHandler.Instance.eventId == activate)
        {
            int looptimes = 0;
            bool hasFrozen = false;
            while (!hasFrozen && looptimes < 100)
            {
                hasFrozen = FreezeTiles();
                looptimes++;
            }
            if (!hasFrozen)
                Debug.LogError("Zone non-gelée non trouvée.");
            EventHandler.Instance.eventId = 0;
        }
    }

    bool FreezeTiles()
    {
        bool hasFrozen = false;
        int x = (int)Random.Range(0, EventHandler.Instance.nbTreeColumns - 2);
        int y = (int)Random.Range(0, EventHandler.Instance.nbTreeRows - 2);
        hasFrozen = FreezeTile(x, y);
        hasFrozen = FreezeTile(x + 1, y);
        hasFrozen = FreezeTile(x, y + 1);
        hasFrozen = FreezeTile(x + 1, y + 1);
        return hasFrozen;
    }

    bool FreezeTile(int x, int y)
    {
        if (EventHandler.Instance.allTree[y, x].isHealthy)
        {
            EventHandler.Instance.allTree[y, x].SetUnhealthy(addedHitPoints);
            tilemap.SetTile(EventHandler.Instance.allTree[y, x].GetPos(), frozenTile);
            return true;
        }
        return false;
    }
}
