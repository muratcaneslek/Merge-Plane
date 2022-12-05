using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    

    public static WayPoint Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }




    public List<Transform> GetWaypoints()
    {
        return waypoints;
    }

    public int GetNextIndex(int currentIndex)
    {
        if(currentIndex == waypoints.Count-1)
        {
            return 0;
        }
        else
        {
            return currentIndex+1;
        }
    }

    public Transform GetStartPoint()
    {
        return waypoints[0];
    }

}
