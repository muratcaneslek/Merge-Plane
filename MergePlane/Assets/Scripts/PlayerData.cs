using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private long gold;

    public long Gold()
    {
        return gold;
    }

    public void AddGold(long delta)
    {
        gold += delta;
    }

    public void ReduceGold(long delta)
    {
        gold -= delta;
    }
}
