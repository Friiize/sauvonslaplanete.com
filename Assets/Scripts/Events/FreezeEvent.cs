using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreezeEvent : MonoBehaviour
{
    private int activate = 2;
    public Tilemap tilemap;
    public TileBase frozenTile;
    public int addedHitPoints = 1;

    // Update is called once per frame
    void Update()
    {
        if (EventHandler.Instance.eventId == activate)
        {
            bool hasFrozen = false;
            while (!hasFrozen && EventHandler.Instance.GetHealthyTrees() > 0)
                hasFrozen = FreezeTiles();
            EventHandler.Instance.eventId = 0;
        }
    }

    bool FreezeTiles()
    {
        bool hasFrozen = false;
        int x = (int)Random.Range(0, EventHandler.Instance.nbTreeColumns - 1);
        int y = (int)Random.Range(0, EventHandler.Instance.nbTreeRows - 1);
        hasFrozen = FreezeTile(x, y);
        hasFrozen = FreezeTile(x + 1, y) || hasFrozen;
        hasFrozen = FreezeTile(x, y + 1) || hasFrozen;
        hasFrozen = FreezeTile(x + 1, y + 1) || hasFrozen;
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
