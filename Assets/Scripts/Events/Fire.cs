using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    private int activate = 1;
    public Tilemap tilemap;
    public Tile tile;
    public float time = 3f;
    public int addedHitPoints = 3;

    // Update is called once per frame
    void Update()
    {
        if (EventHandler.Instance.eventId == activate)
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
            bool fireSpread = false;
            int spreadPossibility = 4;
            int randomSpread = UnityEngine.Random.Range(0, 4);
            while (fireSpread == false && spreadPossibility >0)
            {
                
                switch (randomSpread)
                {
                    case 0:
                        if (a_burningTree.GetPos().x + 7 > 0)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y+4,a_burningTree.GetPos().x+6].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.left);
                                fireSpread = true;
                                Debug.Log("left treeburned by tree at pos" + a_burningTree.GetPos());
                            }
                            else
                            {
                                spreadPossibility--;
                            }
                            
                        }
                        break;
                    case 1:
                        if (a_burningTree.GetPos().x + 7 < EventHandler.Instance.nbTreeColumns - 1)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y + 4, a_burningTree.GetPos().x + 8].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.right);
                                fireSpread = true;
                                Debug.Log("right treeburned by tree at pos" + a_burningTree.GetPos());
                            }
                            else
                            {
                                spreadPossibility--;
                            }
                        }
                        break;
                    case 2:
                        if (a_burningTree.GetPos().y + 4 > 0)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y +3, a_burningTree.GetPos().x +7].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.down);
                                fireSpread = true;
                                Debug.Log("down treeburned by tree at pos" + a_burningTree.GetPos());
                            }
                            else
                            {
                                spreadPossibility--;
                            }
                        }
                        break;
                    case 3:
                        if (a_burningTree.GetPos().y + 4 < EventHandler.Instance.nbTreeRows - 1)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y + 5, a_burningTree.GetPos().x + 7].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.up);
                                Debug.Log("up treeburned by tree at pos" + a_burningTree.GetPos());
                                fireSpread = true;
                            }
                            {
                                spreadPossibility--;
                            }
                        }
                        break;
                }

                randomSpread = UnityEngine.Random.Range(0, 4);
            }
            
            a_burningTree.isSpreading = true;
        }
    }
}
