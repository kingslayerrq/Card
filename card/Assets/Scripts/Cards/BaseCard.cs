using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    private Renderer cardRenderer;
    

    private void Start()
    {
        
        cardRenderer = cPrefab.transform.GetComponent<Renderer>();
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
        this.cardRenderer = this.cPrefab.transform.GetComponent<Renderer>();
    }


   
    private void bringCardToFront()
    {
        
        cardRenderer.sortingOrder = 999;
    }

    private void setCardToBack()
    {
        cardRenderer.sortingOrder = 1;
    }
   
    
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(1);
        this.isActive = true;
        if (isActive)
        {
            Debug.Log("isactive");
            bringCardToFront();
            transform.localScale = cardRenderScale * 2f;
        }
        //throw new System.NotImplementedException();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (isActive)
        {
            setCardToBack();
            transform.localScale = cardRenderScale;
            this.isActive = false;
        }
        //throw new System.NotImplementedException();
    }
}
