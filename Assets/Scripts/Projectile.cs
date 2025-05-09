using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 screenBounds;
    public static int projectileCount = 0;
    private float speed = 40.0f; // Speed of the object
    Vector3 lastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        projectileCount++;
    }

    private void OnDestroy()
    {
        projectileCount--;
    }
    void Start()
    {
        lastPosition = transform.position;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, lastPosition);
        Debug.Log("Speed this frame: " + distance / Time.deltaTime);
        lastPosition = transform. position;
        if(transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y 
        || transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("WayPoint"))
        {
            Destroy(gameObject);
        }
    }
}
