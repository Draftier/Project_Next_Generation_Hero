using UnityEngine;
using UnityEngine.Rendering;

public class WayPoint : MonoBehaviour
{
    public static int wayPointCount = 0;
    public WayPointManager wayPointManager;
    public SpriteRenderer spriteRenderer;
    private Color spriteColor;
    private int hitCount;
    public float Health = 100.0f;
    public int priority;

    void Awake()
    {

        hitCount = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
        wayPointCount++;
        wayPointManager = Object.FindFirstObjectByType<WayPointManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        hitCount++;
       if(hitCount == 4)
        {
            wayPointManager.SpawnWayPoint(gameObject, gameObject.transform.position);
            hitCount = 0;
            Health = 100;
            spriteColor.a = 1.0f;
        }
        spriteColor.a *= 0.8f;
        Health *= 0.75f;
        spriteRenderer.color = spriteColor;
    }
}
