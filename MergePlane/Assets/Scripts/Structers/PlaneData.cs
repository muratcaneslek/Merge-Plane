using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlaneData
{
    [Header("General")]
    public int level;
    public Sprite sprite;
    public string name;
    [Header("Market")]
    public Image marketImage;
    public int goldPrice;
    public int diamondPrice;
}
