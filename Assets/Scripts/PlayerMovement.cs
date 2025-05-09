using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject projectilePrefab;
    public bool mouseDrive;
    public static string driveMode = "Mouse";
    public bool isShooting = false;
    public float projectileSpeed = 0.2f;
    // public int TouchedEnemy = 0;
    private float speed = 13.0f;
    private float horizontalInput;
    private float rotationSpeed = 60.0f;
    public WayPointManager waypointManager;
    public Image leftBar;
    public Image rightBar;

    float charge = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void switchDriveMode()
    {
        mouseDrive = !mouseDrive;
        driveMode = mouseDrive ? "Mouse" : "Key";
    }

    private IEnumerator FireProjectile()
    {
        isShooting = true;

        while (Input.GetKey(KeyCode.Space))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            charge = 1f;
            leftBar.fillAmount = charge;
            rightBar.fillAmount = charge;
            yield return new WaitForSeconds(projectileSpeed);
        }
        isShooting = false;
    }

    void Start()
    {
        mouseDrive = true; // Start with mouse drive mode
        leftBar.fillAmount = 0;
        rightBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            switchDriveMode();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(FireProjectile());
            
        }


        if (isShooting && charge > 0)
        {
            charge -= Time.deltaTime / projectileSpeed;
            charge = Mathf.Clamp01(charge);

            leftBar.fillAmount = charge;
            rightBar.fillAmount = charge;
        }
        else if (!isShooting && charge > 0)
        {

            charge -= Time.deltaTime;
            charge = Mathf.Clamp01(charge);

            leftBar.fillAmount = charge;
            rightBar.fillAmount = charge;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Enemy.sequential = !Enemy.sequential;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            waypointManager.HideWayPoints();
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -(horizontalInput * rotationSpeed * Time.deltaTime));

        if (mouseDrive)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
        else
        {
            Vector3 forward = transform.up;
            transform.Translate(forward * speed * Time.deltaTime, Space.World);
        }
    }
}
