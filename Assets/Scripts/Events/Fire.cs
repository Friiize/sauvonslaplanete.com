using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour
{
    private int activate = 1;
    public Tilemap tilemap;
    public Tile tile;
    private float time = 0;
    public int addedHitPoints = 3;

    // Update is called once per frame
    void Update()
    {
        if (EventHandler.Instance.eventId == activate)
        {
            Vector3Int location = new Vector3Int((int)UnityEngine.Random.Range(-7, 7), (int)UnityEngine.Random.Range(-4, 2), 0);
            time = Random.Range(3f, 10f);
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
        Debug.Log("Burned tree at " + position);
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
            bool[] spreadPossibility = new bool[4] {true, true, true, true};
            int randomSpread;
            while (!fireSpread && (spreadPossibility[0] || spreadPossibility[1] || spreadPossibility[2] || spreadPossibility[3]))
            {
                randomSpread = UnityEngine.Random.Range(0, 4);
                switch (randomSpread)
                {
                case 0:
                    if (spreadPossibility[0])
                    {
                        spreadPossibility[0] = false;
                        if (a_burningTree.GetPos().x + 7 > 0)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y + 4, a_burningTree.GetPos().x + 6].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.left);
                                fireSpread = true;
                                Debug.Log("Burned tree left to tree at pos" + a_burningTree.GetPos());
                            }
                        }
                    }
                    break;
                case 1:
                    if (spreadPossibility[1])
                    {
                        spreadPossibility[1] = false;
                        if (a_burningTree.GetPos().x + 7 < EventHandler.Instance.nbTreeColumns - 1)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y + 4, a_burningTree.GetPos().x + 8].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.right);
                                fireSpread = true;
                                Debug.Log("Bruned tree right to by tree at pos" + a_burningTree.GetPos());
                            }
                        }
                    }
                    break;
                case 2:
                    if (spreadPossibility[2])
                    {
                        spreadPossibility[2] = false;
                        if (a_burningTree.GetPos().y + 4 > 0)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y + 3, a_burningTree.GetPos().x + 7].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.down);
                                fireSpread = true;
                                Debug.Log("Burned tree below tree at pos" + a_burningTree.GetPos());
                            }
                        }
                    }
                    break;
                case 3:
                    if (spreadPossibility[3])
                    {
                        spreadPossibility[3] = false;
                        if (a_burningTree.GetPos().y + 4 < EventHandler.Instance.nbTreeRows - 1)
                        {
                            if (EventHandler.Instance.allTree[a_burningTree.GetPos().y + 5, a_burningTree.GetPos().x + 7].isHealthy)
                            {
                                SetFire(a_burningTree.GetPos() + Vector3Int.up);
                                Debug.Log("Burned tree above tree at pos" + a_burningTree.GetPos());
                                fireSpread = true;
                            }
                        }
                    }
                    break;
                }
            }
            a_burningTree.isSpreading = true;
        }
    }
}
