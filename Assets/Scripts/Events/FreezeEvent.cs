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
        hasFrozen = FreezeTile(EventHandler.Instance.allTree[y, x]);
        hasFrozen = FreezeTile(EventHandler.Instance.allTree[y + 1, x]) || hasFrozen;
        hasFrozen = FreezeTile(EventHandler.Instance.allTree[y, x + 1]) || hasFrozen;
        hasFrozen = FreezeTile(EventHandler.Instance.allTree[y + 1, x + 1]) || hasFrozen;
        return hasFrozen;
    }

    bool FreezeTile(Arbre target)
    {
        if (target.isHealthy)
        {
            target.SetUnhealthy(addedHitPoints);
            tilemap.SetTile(target.GetPos(), frozenTile);
			Debug.Log("Tree freezed at " + target.GetPos());
            return true;
        }
        return false;
    }
}
