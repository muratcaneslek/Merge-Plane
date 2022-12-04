using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f; 
    private int lastWaypointIndex;
    public bool carStatus;

    private float movementSpeed = 5.0f;  // public Level çarpaný gelicek
    private float rotationSpeed = 1.0f;


    void Start()
    {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        carStatus = false;
        
    }

    void Update()
    {
            carStatus = true;
            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            Vector2 directionToTarget = targetWaypoint.position - transform.position;
            if (directionToTarget != Vector2.zero)
            {
                var neededRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, rotationSpeed * Time.deltaTime);
            }

            if (carStatus == true)
            {
                float distance = Vector3.Distance(transform.position, targetWaypoint.position);
                CheckDistanceToWaypoint(distance);
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
            }
    }

    /// <param name="currentDistance">The enemys current distance from the waypoint</param>
    void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }

        targetWaypoint = waypoints[targetWaypointIndex];
    }
}
