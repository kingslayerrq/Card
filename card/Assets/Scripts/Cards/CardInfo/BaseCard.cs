using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BaseCard : MonoBehaviour
{
    
    
    public ScriptableCards cardData;
    public string cId;
    public string cName;
    public string cDescription;
    public int cCost;
    public Rarity cRarity;
    public Type cType;
    public Target cTarget;
    public curStatus cStatus;
    public BaseCard cPrefab;
    public bool isShown;                 // is displayed in hand?
    public bool isCurCard;
    public int curIndexInHand;
    private int initSibIndex;
    


    
    public void loadCardData(ScriptableCards cardData)
    {
        this.cardData = cardData;
        this.cId = cardData.cId;
        this.cName = cardData.cName;
        this.cDescription = cardData.cDescription;
        this.cCost = cardData.cCost;
        this.cRarity = cardData.cRarity;
        this.cType = cardData.cType;
        this.cTarget = cardData.cTarget;
        this.cStatus = cardData.cStatus;
        this.cPrefab = cardData.cPrefab;
        this.isShown = cardData.isShown;                             // update once shown
        this.isCurCard = false;                                       // update on begin drag
        this.curIndexInHand = cardData.curIndexInHand;
        

    }

   
    
    

    public virtual void use(BaseUnit unit)
    {

    }
 
    



    

    

    
}
