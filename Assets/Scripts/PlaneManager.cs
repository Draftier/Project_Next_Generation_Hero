using System;
using TMPro;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public GameObject projectilePrefab;
    public TextMeshProUGUI TextMeshPro;
    private float spawnRangeX;
    private float spawnRangeY;

    private Vector2 GenerateSpawnPosition()
    {
        float spawnX = UnityEngine.Random.Range(-spawnRangeX, spawnRangeX);
        float spawnY = UnityEngine.Random.Range(-spawnRangeY, spawnRangeY);
        return new Vector2(spawnX, spawnY);
    }

    void SpawnEnemy(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector2 spawnPosition = GenerateSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void UpdateScore()
    {
        TextMeshPro.text = "WAYPOINTS: (" + Enemy.waypointMode + ") HERO: Driver(" + PlayerMovement.driveMode + ") TouchedEnemy(" + Enemy.TouchedEnemy   
        + ") EGG OnScreen(" + Projectile.projectileCount + ") ENEMY: Count(" + Enemy.enemyCount + ")" + " Destroyed(" + Enemy.destroyed + ")";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spawnRangeX = screenBounds.x * 0.9f; 
        spawnRangeY = screenBounds.y * 0.9f; 

        SpawnEnemy(Enemy.enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.enemyCount < 10)
        {
            SpawnEnemy(1);
        }
        UpdateScore();
    }
}
