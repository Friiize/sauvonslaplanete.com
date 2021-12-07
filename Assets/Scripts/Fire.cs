using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    public Tilemap map;
    public Tile tile;
    public Vector3Int tilePosition;
    public static int nbTreeRow=6;
    public static int nbTreeColumn=14;
    public static Tree[,] allTree = new Tree[nbTreeRow,nbTreeColumn];
    public int Test = 0;
    public float time = 2f;
    public List<Tree> BurningTreeList = new List<Tree>();
    public Tree burningTree;

    // Start is called before the first frame update
    void Start()
    {
        burningTree = new Tree();
        
        for (int i = 0; i < nbTreeRow; i++)
        {
            for (int j = 0; j < nbTreeColumn; j++)
            {

                allTree[i, j] = new Tree();
                allTree[i, j].setBurn(false);
                allTree[i, j].setPos(j-7,i-4);                
               
               
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int location = new Vector3Int((int)UnityEngine.Random.Range(-7, 7), (int)UnityEngine.Random.Range(-4, 2), 0);
            
            
            SetFire(location);
        }
        //get all burning trees
        for (int i = 0; i < nbTreeRow; i++)
        {
            for (int j = 0; j < nbTreeColumn; j++)
            {
                if (allTree[i, j].GetBurn() &&allTree[i,j].GetSpread()==false)               {

                        allTree[i, j].setSpread(true);
                        StartCoroutine(spreadFire(3f,allTree[i,j]));
                    

                }
               
            }
        }
        
    }


    public void SetFire(Vector3Int position)
    {
        
        
        Debug.Log(position);
       // Debug.Log(tile);  
       if (allTree[position.y + 4, position.x + 7].GetBurn() == false)
        {
            map.SetTile(position, tile);
            allTree[position.y + 4, position.x + 7].setBurn(true);
            allTree[position.y + 4, position.x + 7].setSpread(false);
        }

        

    }
IEnumerator spreadFire( float delayTime,Tree a_burningTree)
{
    yield return new WaitForSeconds(delayTime);
    if (a_burningTree.GetPos().x + 7 > 0)
    {
        SetFire(a_burningTree.GetPos() + Vector3Int.left);
        Debug.Log("left treeburned by tree at pos" + a_burningTree.GetPos());
    }
    if (a_burningTree.GetPos().x + 7 < nbTreeColumn-1)
    {
        SetFire(a_burningTree.GetPos() + Vector3Int.right);
        Debug.Log("right treeburned by tree at pos" + a_burningTree.GetPos());
    }
    if (a_burningTree.GetPos().y + 4 > 0)
    {
        SetFire(a_burningTree.GetPos() + Vector3Int.down);
        Debug.Log("down treeburned by tree at pos" + a_burningTree.GetPos());
    }
    if (a_burningTree.GetPos().y + 4 < nbTreeRow-1)
    {
        SetFire(a_burningTree.GetPos() + Vector3Int.up);
        Debug.Log("up treeburned by tree at pos" + a_burningTree.GetPos());
    }
}
    
}
