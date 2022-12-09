using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHolder : MonoBehaviour
{
    [SerializeField] private PlaneGround[] grounds;

    public static GroundHolder Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public PlaneGround[] GetGrounds()
    {
        return grounds;
    }

    public PlaneGround GetGround(int index)
    {
        return grounds[index];
    }

    public bool TryGetEmptyGround(out PlaneGround ground)
    {
        foreach(var planeGround in grounds)
        {
            if(planeGround.IsEmpty() && !planeGround.IsLocked())
            {
                ground = planeGround;
                return true;
            }
        }
        ground = null;
        return false;
    }
}
