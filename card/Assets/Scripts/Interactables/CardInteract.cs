using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteract : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private int siblingIndex;
    public Transform parentToReturn = null;
    private Vector3 dragDiff;
    [SerializeField] private Vector3 enlargeratio;
    //private bool isActive = false;
    private Vector3 scale;
    private BaseCard card;
    private void Awake()
    {
        //RectTransform rectTransform = GetComponent<RectTransform>();
        card = GetComponent<BaseCard>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.playerTurn)
        {
            var initPos = transform.position;
            dragDiff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - initPos;
            parentToReturn = this.transform.parent;
            this.transform.SetParent(this.transform.root, false);          // set the parent to canvas for draggin
            card.isCurCard = true;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        } 
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.playerTurn)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragDiff;
        }
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.playerTurn)
        {
            card.isCurCard = false;
            this.transform.SetParent(parentToReturn, false);
            transform.localScale = Vector3.one;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //scale = this.transform.localScale;
        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        //transform.SetSiblingIndex(siblingIndex);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
    }
}
