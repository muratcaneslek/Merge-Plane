using UnityEngine;

public class PlaneInfos : MonoBehaviour
{

    [SerializeField] private PlaneData[] planeDatas;
    [SerializeField] private GameObject planePrefab;


    // public static Instance for easy access to plane datas;

    # region Instance

    public static PlaneInfos Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("There are more than one PlaneInfos class !!!");
            Destroy(gameObject);
        }
    }

    # endregion


    // Returns plane datas by level
    public PlaneData GetPlaneData(int level)
    {
        if(level < planeDatas.Length)
        {
            return planeDatas[level];
        }
        else
        {
            Debug.Log("End of the planes !!");
            return new PlaneData();
        }
    }

    // Spawn New Plane, Set values of that plane And Return that gameobject
    public GameObject SpawnPlane(int level)
    {
        var plane = Instantiate(planePrefab, transform.position, planePrefab.transform.rotation);
        // send datas to plane
        return plane;
    }

    public float GetSpeed(int level)
    {
        return level * 2;
    }

    public float GetGoldPerTour(int level)
    {
        return level * 5;
    }


    

}
