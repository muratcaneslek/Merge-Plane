using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlaneGround : MonoBehaviour, IDragable
{
    public Plane plane;

    [SerializeField] private BoxCollider2D triggerBox;

    [Header("Lock")]
    [SerializeField] private bool isLocked;
    [SerializeField] private float unlockPrice;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite lockedSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Position")]
    [SerializeField] private SpriteRenderer ghostPlaneSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RefreshSprite();
    }


    // set new plane to ground
    public void SetPlane(Plane plane)
    {
        if(!isLocked)
        {
            plane.SetGround(this);
            triggerBox.enabled = false;
            plane.transform.position = transform.position;
            ghostPlaneSprite.sprite = plane.planeData.sprite;
            this.plane = plane;
        }
        
    }

    public void OnPlaneMovementStart()
    {
        ghostPlaneSprite.enabled = true;
        triggerBox.enabled = true;
    }


    // set ground plane empty
    public void RemovePlane()
    {
        if(!isLocked)
        {
            plane = null;
            triggerBox.enabled = true;
        }
    }

    // stop plane movement and reset position
    private void CallBackPlane()
    {
        ghostPlaneSprite.enabled = false;
        triggerBox.enabled = false;
        plane.MoveBackToGround();
    }

    // return ground has a plane or not
    public bool IsEmpty()
    {
        return !plane;
    }

    public bool IsLocked()
    {
        return isLocked;
    }


    // on player touch
    // try to unlock if ground locked
    // try to call plane back if ground unlocked and has a plane
    private void OnTouched()
    {
        if(isLocked)
        {
            if(true)// try to buy 
            {
                isLocked = false;
                RefreshSprite();
            }
        }
        else
        {
            if(!IsEmpty())
            {
                if(plane.IsMoving()) // check is plane moving
                {
                    CallBackPlane();
                }
            }
        }   
    }
    

    // Refresh sprite by is ground locked
    private void RefreshSprite()
    {
        if(isLocked)
        {
            spriteRenderer.sprite = lockedSprite;
        }
        else
        {
            spriteRenderer.sprite = unlockedSprite;
        }
    }

    public void OnTouch(DragHandeler dragHandeler)
    {
        OnTouched();
    }

    public void OnTouchEnd(DragHandeler dragHandeler)
    {
        
    }
}
