using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class EventHandler : MonoBehaviour
{
    [HideInInspector]
    public static EventHandler Instance;        
    public float increase;
    [HideInInspector]
    public int eventId = 0;
    public Tilemap tilemap;
    public Tile healthyTile;
    [HideInInspector]
    public int nbTreeRows = 6;
    [HideInInspector]
    public int nbTreeColumns = 14;
    public Arbre[,] allTree = new Arbre[6, 14];
    private bool isGameEnded = false;

    private void Awake()
    {
        if (Instance) throw new NotImplementedException("Plus d'une fois le script dans la scène !");
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < nbTreeRows; i++)
        {
            for (int j = 0; j < nbTreeColumns; j++)
            {
                allTree[i, j] = new Arbre();
                allTree[i, j].isBurning = false;
                allTree[i, j].SetPos(j - 7, i - 4);
            }
        }
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        while (!isGameEnded)
        {
            yield return new WaitForSeconds(increase);
            NextEvent();
        }
    }
    private void NextEvent()
    {
        eventId = Random.Range(1, 3);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            allTree[tilemapPos.y + 4, tilemapPos.x + 7].GetHit(tilemap, healthyTile);
        }
    }
    
    public void Stop()
    {
        isGameEnded = true;
    }
}

