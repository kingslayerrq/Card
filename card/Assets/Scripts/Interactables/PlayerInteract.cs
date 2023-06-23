using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteract : Droppable
{
    public BasePlayer curPlayer;
    private void Awake()
    {
        curPlayer = GetComponent<BasePlayer>();
    }
    public override void OnDrop(PointerEventData eventData)
    {
        CardInteract c = eventData.pointerDrag.GetComponent<CardInteract>();
        BaseCard card = c.GetComponent<BaseCard>();
        if (card.cTarget == Target.Self)
        {
            //card.use(curPlayer);
            
            CardManager.Instance.dealCard(card, curPlayer);
        }
        else
        {
            Debug.Log("Not a valid card to use on Player!");
        }
        
    }

    
}
