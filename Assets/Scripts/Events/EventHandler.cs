using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class EventHandler : MonoBehaviour
{
    [HideInInspector]
    public static EventHandler Instance;        
    public float increase;
    public int eventId = 0;
    public Tilemap tilemap;
    public TileBase healthyTile;

    private void Awake()
    {
        if (Instance) throw new NotImplementedException("Plus d'une fois le script dans la scène !");
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("NextEvent", 2f, increase);
    }

    private void NextEvent()
    {
        eventId = Random.Range(1, 2);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tilemap.SetTile(tilemapPos, healthyTile);
        }
    }
}