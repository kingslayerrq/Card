using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Droppable : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        CardInteract c = eventData.pointerDrag.GetComponent<CardInteract>();
        if (c != null)
        {
            c.parentToReturn = this.transform;
        }
        
    }
}
