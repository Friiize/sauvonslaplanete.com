using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arbre //: MonoBehaviour
{
    public bool isHealthy = true;
    public bool isBurning = false;
    public bool isFrozen = false;
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
        if (hitPoints > 0)
        {
            hitPoints--;
            EventHandler.Instance.score += 1;
            if (isBurning)
                AudioManager.Instance.Play("Tap-Feu");
            else if (isFrozen)
                AudioManager.Instance.Play("Tap-Glace");
            if (hitPoints == 0)
            {
                Debug.Log("Tree Healed at " + treePos);
                isHealthy = true;
                tilemap.SetTile(treePos, tile);
                if (isBurning)
                {
                    isBurning = false;
                    if (!EventHandler.Instance.AreBurningTrees())
                        AudioManager.Instance.Stop("Son-Feu");
                }
                else if (isFrozen)
                {
                    isFrozen = false;
                    if (!EventHandler.Instance.AreFrozenTrees())
                        AudioManager.Instance.Stop("Son-Glace");
                }
                EventHandler.Instance.SetBiodiversitySound();
            }
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
