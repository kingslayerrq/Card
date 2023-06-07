using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    
    public static Vector3 cardRenderScale = new Vector3(0.2f, 0.2f, 0);
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
    private Renderer cardRenderer;

    private void Start()
    {
        
        Renderer cardRenderer = cPrefab.transform.GetComponent<Renderer>();
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
        this.isShown = false;                             // update once shown
        this.isActive = false;
        this.cardRenderer = this.cPrefab.transform.GetComponent<Renderer>();
    }


    private void OnMouseEnter()
    {
        this.isActive = true;
        if (isActive)
        {
            Debug.Log("isactive");
            bringCardToFront();
            transform.localScale = cardRenderScale*2;
        }
        
        
        
    }
    private void OnMouseExit()
    {
        if (isActive)
        {
            setCardToBack();
            transform.localScale = cardRenderScale;
            this.isActive = false;
        }
        

    }
    private void bringCardToFront()
    {
        
        cardRenderer.sortingOrder = 999;
    }

    private void setCardToBack()
    {
        cardRenderer.sortingOrder = 1;
    }
    
}
