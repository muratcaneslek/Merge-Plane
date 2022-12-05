using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlaneGround : MonoBehaviour
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
    [SerializeField] private Transform planeTransform;

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
            triggerBox.enabled = false;
            plane.transform.position = planeTransform.position;
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
        return plane;
    }


    // on player touch
    // try to unlock if ground locked
    // try to call plane back if ground unlocked and has a plane
    public void OnTouched()
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
                if(true) // check is plane moving
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

}
