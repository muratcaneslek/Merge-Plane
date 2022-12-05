using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    public void OnTouch(DragHandeler dragHandeler);

    public void OnTouchEnd(DragHandeler dragHandeler);
}
