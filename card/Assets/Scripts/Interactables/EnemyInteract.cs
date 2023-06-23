using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyInteract : Droppable, IPointerEnterHandler
{
    public BaseEnemy curEnemy;

    private void Awake()
    {
        curEnemy = GetComponent<BaseEnemy>();

    }
    public override void OnDrop(PointerEventData eventData)
    {
        CardInteract c = eventData.pointerDrag.GetComponent<CardInteract>();
        BaseCard card = c.GetComponent<BaseCard>();

        if (card.cTarget == Target.Enemy)
        {
            
            CardManager.Instance.dealCard(card, curEnemy);
        }
        else
        {
            Debug.Log("Not a valid card to use on Enemy!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("enter");
    }
}
