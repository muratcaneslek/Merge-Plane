using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Plane : MonoBehaviour, IDragable
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
    private PlaneMovement planeMovement;

    [SerializeField] private LayerMask whatIsStart;

    public UnityEvent OnLevelUp;

    void Awake()
    {
        planeMovement = GetComponent<PlaneMovement>();
    }



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
        planeMovement.StartMovement();
        if(planeGround)
        {
            planeGround.OnPlaneMovementStart();
        }
    }
    private void StopMove()
    {
        planeMovement.StopMovement();
    }

    public void OnTouch(DragHandeler dragHandeler) // on mouse click or touch
    {
        if(!planeMovement.move) // if plane is not moving
        {
            dragHandeler.itemBeingDragged = gameObject; // set clicked object to this gameobject
        }
    }

    public void OnTouchEnd(DragHandeler dragHandeler) // on mouse click or touch ENDED
    {
        var hit = Physics2D.OverlapCircle(transform.position, 0.3f, whatIsStart); // check last position
        if(hit) // if plane is on start position
        {
            StartMove();
        }
        else // if not turn back to ground
        {
            MoveBackToGround();
        }
    }

    public void MoveBackToGround() // stops movement and moves back to ground
    {
        if(planeGround)
        {
            StopMove();
            StartCoroutine(GetBackToGround());
        }
    }

    IEnumerator GetBackToGround()
    {
        var destination = planeGround.transform.position - transform.position;
        while(destination.magnitude > 0.1f)
        {
            transform.position += destination.normalized * Time.deltaTime * 5;
            yield return new WaitForSeconds(0);
        }
    }
}
