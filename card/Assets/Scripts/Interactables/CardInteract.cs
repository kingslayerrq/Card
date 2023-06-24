using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInteract : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private int siblingIndex;
    public Transform parentToReturn = null;                      // for drag and drop return
    private Vector3 dragDiff;
    [SerializeField] private Vector3 enlargeratio;
    //private bool isActive = false;
    //private Vector3 scale;
    private BaseCard card;
    [SerializeField] private Transform _hand;
    
    private Vector3 origPos;
    private int origSibIndex;
    private Transform origParent;                                // for hover return
    private bool activeCard = false;
    private void Awake()
    {
        //RectTransform rectTransform = GetComponent<RectTransform>();
        card = GetComponent<BaseCard>();
        
        
        //origParent = transform.parent;
        //Debug.Log(origParent.parent);
        //movedOutOfHands = new List<Transform>();
        
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.playerTurn)
        {

            var initPos = transform.position;
            dragDiff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - initPos;
            parentToReturn = transform.parent;
            origSibIndex = transform.GetSiblingIndex();
            transform.SetParent(transform.root, false);          // set the parent to canvas for draggin
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
            transform.SetParent(parentToReturn, false);
            transform.SetSiblingIndex(origSibIndex);
            transform.localScale = Vector3.one;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.DOScale(new Vector3(2, 2, 2), 0.5f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.5f);
   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //isHovering = false;
        //transform.localScale = Vector3.one;
    }

    
}
