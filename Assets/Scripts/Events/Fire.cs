using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;
    public float time = 2f;
    public int addedHitPoints = 3;

    // Update is called once per frame
    void Update()
    {
        if (EventHandler.Instance.eventId == 1)
        {
            Vector3Int location = new Vector3Int((int)UnityEngine.Random.Range(-7, 7), (int)UnityEngine.Random.Range(-4, 2), 0);
            SetFire(location);
            EventHandler.Instance.eventId = 0;
        }
        //get all burning trees
        for (int i = 0; i < EventHandler.Instance.nbTreeRows; i++)
        {
            for (int j = 0; j < EventHandler.Instance.nbTreeColumns; j++)
            {
                if (EventHandler.Instance.allTree[i, j].isBurning && EventHandler.Instance.allTree[i, j].isSpreading)
                {
                    EventHandler.Instance.allTree[i, j].isSpreading = false;
                    StartCoroutine(spreadFire(time, EventHandler.Instance.allTree[i, j]));
                }
            }
        }
    }

    public void SetFire(Vector3Int position)
    {
        Debug.Log(position);
        // Debug.Log(tile);  
        if (EventHandler.Instance.allTree[position.y + 4, position.x + 7].isHealthy)
        {
            tilemap.SetTile(position, tile);
            EventHandler.Instance.allTree[position.y + 4, position.x + 7].isBurning = true;
            EventHandler.Instance.allTree[position.y + 4, position.x + 7].SetUnhealthy(addedHitPoints);
            EventHandler.Instance.allTree[position.y + 4, position.x + 7].isSpreading = true;
        }
    }

    IEnumerator spreadFire(float delayTime, Arbre a_burningTree)
    {
        yield return new WaitForSeconds(delayTime);
        if (a_burningTree.isBurning)
        {
            if (a_burningTree.GetPos().x + 7 > 0)
            {
                SetFire(a_burningTree.GetPos() + Vector3Int.left);
                Debug.Log("left treeburned by tree at pos" + a_burningTree.GetPos());
            }
            if (a_burningTree.GetPos().x + 7 < EventHandler.Instance.nbTreeColumns - 1)
            {
                SetFire(a_burningTree.GetPos() + Vector3Int.right);
                Debug.Log("right treeburned by tree at pos" + a_burningTree.GetPos());
            }
            if (a_burningTree.GetPos().y + 4 > 0)
            {
                SetFire(a_burningTree.GetPos() + Vector3Int.down);
                Debug.Log("down treeburned by tree at pos" + a_burningTree.GetPos());
            }
            if (a_burningTree.GetPos().y + 4 < EventHandler.Instance.nbTreeRows - 1)
            {
                SetFire(a_burningTree.GetPos() + Vector3Int.up);
                Debug.Log("up treeburned by tree at pos" + a_burningTree.GetPos());
            }
            a_burningTree.isSpreading = true;
        }
    }
}
