using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    
    public static Vector3 cardRenderScale = new Vector3(10, 10, 0);
    protected ScriptableCards cardData;
    public string cId;
    public string cName;
    public string cDescription;
    public Rarity cRarity;
    public Type cType;
    public Target cTarget;
    public curStatus cStatus;
    public BaseCard cPrefab;
    public bool isShown;                 // is displayed in hand?
    public bool isActive;
    public int curIndexInHand;
    public CardSlot cSlot;
    private SpriteRenderer cardRenderer;
    private int initSibIndex;
    private Vector3 startPos;
    private Vector3 dragDiff;
    private Collider2D collide;

    private void Awake()
    {
        cardRenderer = GetComponent<SpriteRenderer>();
        collide = GetComponent<Collider2D>();
    }

    private void Start()
    {
        
    }
    public void loadCardData(ScriptableCards cardData)
    {
        this.cId = cardData.cId;
        this.cName = cardData.cName;
        this.cDescription = cardData.cDescription;
        this.cRarity = cardData.cRarity;
        this.cType = cardData.cType;
        this.cTarget = cardData.cTarget;
        this.cStatus = cardData.cStatus;
        this.cPrefab = cardData.cPrefab;
        this.isShown = cardData.isShown;                             // update once shown
        this.isActive = false;
        this.curIndexInHand = cardData.curIndexInHand;
        this.cSlot = cardData.cSlot;

    }

    
 
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointenter");
        this.isActive = true;
        if (isActive)
        {     
            bringCardToFront();
            transform.localScale = cardRenderScale * 2f;
        }
        //throw new System.NotImplementedException();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointexit");
        if (isActive)
        {
            restoreCardToBack();
            transform.localScale = cardRenderScale;
            isActive = false;
        }
        //throw new System.NotImplementedException();
    }



    private void bringCardToFront()
    {
        initSibIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        cardRenderer.sortingLayerName = "activeCards";
        cardRenderer.sortingOrder = 999;
    }

    private void restoreCardToBack()
    {
        cardRenderer.sortingLayerName = "cards";
        transform.SetSiblingIndex(initSibIndex);
        cardRenderer.sortingOrder = 1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begindrag");
        GameManager.Instance.mainRaycaster.enabled = false;
        dragDiff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cSlot.getCardSlotPos();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(cSlot.getCardSlotPos());
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // maintain the difference between object and dragPostion
        transform.position = mousePos - dragDiff;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");
        transform.position = cSlot.getCardSlotPos();
        isActive = false;
        GameManager.Instance.mainRaycaster.enabled = true;
        dragDiff = new Vector3(0, 0);
        restoreCardToBack();
        transform.localScale = cardRenderScale;
       
    }

    
}
