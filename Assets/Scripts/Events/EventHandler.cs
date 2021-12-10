using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance;
    public int score = 0;
    public GameObject text;
    
    [HideInInspector]
    public int eventId = 0;
    public float spawnRate = 1;
    public MeteoHandler meteoHandler;
    [HideInInspector]
    public bool isGameEnded = false;

    public Tilemap tilemap;
    public Tile healthyTile;

    [HideInInspector]
    public int nbTreeRows = 6;
    [HideInInspector]
    public int nbTreeColumns = 14;
    public Arbre[,] allTree = new Arbre[6, 14];
    

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

    private void FixedUpdate()
    {
        if (!isGameEnded)
        {
            score++;
            text.GetComponent<UnityEngine.UI.Text>().text = "Score : " + score;
        }
    }

    private IEnumerator Coroutine()
    {
        while (!isGameEnded)
        {
            spawnRate = Mathf.Round(10 + meteoHandler.temperature * 20) / 10;
            yield return new WaitForSeconds(5 / spawnRate);
            NextEvent();
        }
    }

    private void NextEvent()
    {
        eventId = Random.Range(1, 3);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameEnded)
        {
            Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (tilemapPos.x < 7 && tilemapPos.x > -8 && tilemapPos.y < 2 && tilemapPos.y > -5)
                allTree[tilemapPos.y + 4, tilemapPos.x + 7].GetHit(tilemap, healthyTile);
        }
    }

    public int GetHealthyTrees()
    {
        int healthyTrees = 0;
        for (int i = 0; i < nbTreeRows; i++)
            for (int j = 0; j < nbTreeColumns; j++)
                if (allTree[i, j].isHealthy)
                    healthyTrees++;
        return healthyTrees;
    }

    public bool AreBurningTrees()
    {
        for (int i = 0; i < nbTreeRows; i++)
            for (int j = 0; j < nbTreeColumns; j++)
                if (allTree[i, j].isBurning)
                    return true;
        return false;
    }

    public bool AreFrozenTrees()
    {
        for (int i = 0; i < nbTreeRows; i++)
            for (int j = 0; j < nbTreeColumns; j++)
                if (allTree[i, j].isFrozen)
                    return true;
        return false;
    }

    public void SetBiodiversitySound()
    {
        int healthyTrees = GetHealthyTrees();
        if (healthyTrees > 63)
        {
            AudioManager.Instance.Stop("Biodiversité-Moyenne");
            AudioManager.Instance.Play("Biodiversité-Haute");
        }
        else if (healthyTrees > 42)
        {
            AudioManager.Instance.Stop("Biodiversité-Faible");
            AudioManager.Instance.Stop("Biodiversité-Haute");
            AudioManager.Instance.Play("Biodiversité-Moyenne");
        }
        else if (healthyTrees > 21)
        {
            AudioManager.Instance.Stop("Biodiversité-Moyenne");
            AudioManager.Instance.Play("Biodiversité-Faible");
        }
        else
            AudioManager.Instance.Stop("Biodiversité-Faible");
    }

    public void Stop()
    {
        isGameEnded = true;
    }
}

