using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public SpriteRenderer spriteRenderer;

    public static int TouchedEnemy = 0;
    public static int enemyCount = 0;
    public static int destroyed = 0;

    private int hitCount;
    public GameObject PlaneManager;
    public Color spriteColor;
    public GameObject player;

    public WayPointManager wayPointManager;
    private int currentWaypoint = 0;

    public static bool sequential = true;
    public static string waypointMode = "Sequential";

    private void Awake()
    {
        hitCount = 0;
        enemyCount++;
    }

    private void OnDestroy()
    {
        enemyCount--;
        destroyed++;
    }


    void Start()
    {
        spriteColor = spriteRenderer.color;
        wayPointManager = Object.FindFirstObjectByType<WayPointManager>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
        if(sequential == true)
        {
            waypointMode = "Sequential";
        }
        else
        {
            waypointMode = "Random";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            health = 0;
            Destroy(gameObject);
            TouchedEnemy++;
        }
        else if (other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        hitCount++;
        health *= 0.8f;
        if (hitCount == 4)
        {
            Destroy(gameObject);
        }
        spriteColor.a *= 0.8f;
        spriteRenderer.color = spriteColor;
    }

    private void MoveTowardsWaypoint()
    {
        
        Vector2 targetPosition = wayPointManager.GetCurrentWayPoint(currentWaypoint);

        
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; 
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        
        float rotationSpeed = 120f; 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float speed = 25f; 
        transform.position += transform.up * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, targetPosition) <= 30f)
        {
            if (sequential == true)
            {
                currentWaypoint++;
                if (currentWaypoint >= wayPointManager.waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            else
            {
                int genIndex = Random.Range(0, wayPointManager.waypoints.Length);
                while (genIndex == currentWaypoint)
                {
                    genIndex = Random.Range(0, wayPointManager.waypoints.Length);
                }
                currentWaypoint = genIndex;
            }
        }

    }
}
