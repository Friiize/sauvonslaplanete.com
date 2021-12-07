using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool IsBurning = false;
    public bool IsSpreading = false;
    public Fire fire;
    Vector3Int position;
    private Vector3Int treePos;
    private int yPos = 0;
    private int xPos = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBurning)
        {
            fire.SetFire(position);
        }
    }
    public void setBurn(bool a_burn)
    {
        IsBurning = a_burn;
    }
    public bool GetBurn()
    {
        return IsBurning;
    }
    public void setSpread(bool a_spread)
    {
        IsSpreading = a_spread;
    }
    public bool GetSpread()
    {
        return IsSpreading;
    }
    public void setPos(int a_xPos,int a_yPos)    {

        treePos = new Vector3Int(a_xPos, a_yPos, 0);
        xPos = a_xPos;
        yPos = a_yPos;
    }
    public Vector3Int GetPos()
    {

        return treePos;
    }
    public Tree()
    {

    }
}
