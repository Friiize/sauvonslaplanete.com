using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arbre : MonoBehaviour
{
    public bool isHealthy = true;
    public bool isBurning = false;
    public bool isSpreading = false;
    private int hitPoints = 0;
    private Vector3Int treePos;

    public void SetUnhealthy(int a_hitPoints)
    {
        isHealthy = false;
        hitPoints = a_hitPoints;
    }

    public void GetHit(Tilemap tilemap, Tile tile)
    {
        if (--hitPoints <= 0)
        {
            hitPoints = 0;
            isHealthy = true;
            isBurning = false;
            tilemap.SetTile(treePos, tile);
        }
    }

    public void SetPos(int a_xPos, int a_yPos)
    {
        treePos = new Vector3Int(a_xPos, a_yPos, 0);
    }

    public Vector3Int GetPos()
    {
        return treePos;
    }
}
