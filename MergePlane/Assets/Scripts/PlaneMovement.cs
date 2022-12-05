using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public bool move {get; private set;}
    private int lastWaypointIndex = 0;
    private Vector3 targetPos;
    private float tolerance = 0.3f;

    
    public float speed = 2;
    

    public void StartMovement()
    {
        // on start movement, find start position
        targetPos = WayPoint.Instance.GetStartPoint().position;
        move = true;
    }
    public void StopMovement()
    {
        move = false;
        // on stop movement, reset waypoint index
        lastWaypointIndex = 0;
    }


    void Update()
    {
        if(WayPoint.Instance)
        {
            if(move) // hareket ediyorsa
            {
                if((transform.position - targetPos).magnitude > tolerance) // hedeften uzaksa
                {
                    // find target rotation
                    var targetRot = Quaternion.LookRotation(Vector3.forward, (targetPos-transform.position).normalized);
                    // rotate plane to target
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speed);
                    // move plane to target
                    transform.position += (targetPos-transform.position).normalized * speed * Time.deltaTime;
                }
                else // hedefe geldiğinde
                {
                    // find next waypoint index
                    lastWaypointIndex = WayPoint.Instance.GetNextIndex(lastWaypointIndex);
                    // set target to next waypoint position
                    targetPos = WayPoint.Instance.GetWaypoints()[lastWaypointIndex].position;
                }
            }
        }
    }
}
