using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour
{
  
    [SerializeField] private float touchRadius;
    [SerializeField] private LayerMask whatIsDragable;
    public GameObject itemBeingDragged;


    void Update()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if(touch.phase == TouchPhase.Began) // player started touch
            {
                
                var hit = Physics2D.OverlapCircle(touchPosition, touchRadius, whatIsDragable);
                
                if(hit.TryGetComponent<IDragable>(out var dragable))
                {
                    dragable.OnTouch(this);
                }

            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            var clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapCircle(clickPosition, touchRadius, whatIsDragable);
            if(hit)
            {
                if(hit.TryGetComponent<IDragable>(out var dragable))
                {
                    dragable.OnTouch(this);
                }
            }
        }
        if(Input.GetMouseButton(0))
        {
            if(itemBeingDragged)
            {
                var clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                itemBeingDragged.transform.position = new Vector3(clickedPos.x, clickedPos.y, 0);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(itemBeingDragged)
            {
                itemBeingDragged.GetComponent<IDragable>().OnTouchEnd(this);
                itemBeingDragged = null;
            }
        }
    }
}
