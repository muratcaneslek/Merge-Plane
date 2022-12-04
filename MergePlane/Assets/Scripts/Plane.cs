using UnityEngine;
using UnityEngine.Events;

public class Plane : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    // movement component

    # region Plane Datas/Infos

    public PlaneData planeData {get;  private set;} 
    public float speed {get;  private set;} 
    public float goldPerTour {get;  private set;} 

    # endregion


    // The Ground that The Plane is currently on
    private PlaneGround planeGround;

    public UnityEvent OnLevelUp;



    // Refresh plane datas (like speed, sprite, etc.) by parameters
    public void SetDatas(PlaneData data, float speed, float goldPT)
    {
        this.planeData = planeData;
        this.speed = speed;
        this.goldPerTour = goldPT;

        spriteRenderer.sprite = planeData.sprite;
        // set movement speed
    }

    // Refresh plane datas (like speed, sprite, etc.) by using next level datas
    public void Upgrade()
    {
        var level = planeData.level;
        SetDatas(PlaneInfos.Instance.GetPlaneData(level+1),
         PlaneInfos.Instance.GetSpeed(level+1),
          PlaneInfos.Instance.GetGoldPerTour(level+1));
        OnLevelUp.Invoke();
    }

    public void StartMove()
    {
        // movement.move;

        // WayPoint.move = true;
    }

}
