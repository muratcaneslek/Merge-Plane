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
    [SerializeField] private LayerMask whatIsGround;

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
        var ground = Physics2D.OverlapCircle(transform.position, 0.3f, whatIsGround);
        if(hit) // if plane is on start position
        {
            StartMove();
        }
        else if(ground && ground.TryGetComponent<PlaneGround>(out var planeGround) && planeGround.IsEmpty()) // if it is a ground and it is empty
        {
            planeGround.SetPlane(this);
        }
        else    // if not turn back to ground
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

    public bool IsMoving()
    {
        return planeMovement.move;
    }

    IEnumerator GetBackToGround()
    {
        var timer = 0f;
        while(transform.position != planeGround.transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, planeGround.transform.position, timer);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, Vector3.zero, timer));
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
    }

    public void SetGround(PlaneGround planeGround)
    {
        this.planeGround = planeGround;
    }
}
