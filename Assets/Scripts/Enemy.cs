using TMPro;
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            health = 0;
            Destroy(gameObject);
            TouchedEnemy++;
        }
        else if(other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        hitCount++;
        // health -= damage;
        // switch(health)
        // {
        //     case 80:
        //         spriteColor.a = 0.8f;
        //         spriteRenderer.color = spriteColor;
        //         break;
        //     case 60:
        //         spriteColor.a = 0.6f;
        //         spriteRenderer.color = spriteColor;
        //         break;
        //     case 40:
        //         spriteColor.a = 0.4f;
        //         spriteRenderer.color = spriteColor;
        //         break;
        //     case 20:
        //         spriteColor.a = 0.2f;
        //         spriteRenderer.color = spriteColor;
        //         break;
        //     case 0:
        //         spriteColor.a = 0f;
        //         Destroy(gameObject);
        //         break;

        // }
        if(hitCount == 4)
        {
            Destroy(gameObject);
        }
        spriteColor.a *= 0.8f;
        spriteRenderer.color = spriteColor;
    }
}
