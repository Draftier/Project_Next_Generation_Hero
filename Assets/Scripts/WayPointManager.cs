using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WayPointManager : MonoBehaviour
{
    public GameObject waypointPrefab;
    public float spawnRangeX;
    public float spawnRangeY;
    private Vector3 screenBounds; 
    public GameObject[] waypoints;
    private bool isHidden = false;


    private void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    private Vector2 GenerateSpawnPosition(Vector2 previousPosition)
    {
        float spawnX = Random.Range(previousPosition.x-spawnRangeX, previousPosition.x + spawnRangeX);
        float spawnY = Random.Range(previousPosition.y-spawnRangeY, previousPosition.y +spawnRangeY);

        spawnX = Mathf.Clamp(spawnX, -screenBounds.x, screenBounds.x);
        spawnY = Mathf.Clamp(spawnY, -screenBounds.y, screenBounds.y);
        return new Vector2(spawnX, spawnY);
    }

    public void SpawnWayPoint(GameObject waypoint, Vector2 initialPosition)
    {
        Vector2 previousPosition = GenerateSpawnPosition(initialPosition);
        waypoint.transform.position = previousPosition;

        WayPoint wayPointComponent = waypoint.GetComponent<WayPoint>();
        
        waypoint.SetActive(true);
    }

    public Vector2 GetCurrentWayPoint(int currentWaypointIndex)
    {
        List<GameObject> activeWaypoints = waypoints.Where(waypoint => waypoint != null)
        .OrderBy(waypoint => waypoint.GetComponent<WayPoint>().priority)
        .ToList();

        if(currentWaypointIndex < activeWaypoints.Count)
        {
            return activeWaypoints[currentWaypointIndex].transform.position;
        }
        else
        {
            return activeWaypoints[0].transform.position;
        }
    }

    public void HideWayPoints()
    {
        isHidden = !isHidden;
        if(isHidden == true)
        {
            foreach (GameObject waypoint in waypoints)
            {
                if (waypoint != null)
                {
                waypoint.SetActive(false);
                }
            }
        }
        else if(isHidden == false)
        {
            foreach (GameObject waypoint in waypoints)
            {
                if (waypoint != null)
                {
                    waypoint.SetActive(true);
                }
            }
        }   
    }
}
