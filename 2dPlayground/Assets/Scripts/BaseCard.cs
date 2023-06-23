using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard 
{
    
    public string cId;
    public string cName;
    public string cDescription;
    public Rarity cRarity;
    public Type cType;
    public Target cTarget;
    public Image cImage;
    //public Sprite cSprite;
    public curStatus cStatus;
    public bool isShown = false;
    public int curIndexInHand;
    public GameObject cPrefab;
    public BaseCard(ScriptableCards cd) 
    {
        
        this.cId = cd.cId;
        this.cName = cd.cName;
        this.cDescription = cd.cDescription;
        this.cRarity = cd.cRarity;
        this.cType = cd.cType;
        this.cTarget = cd.cTarget;
        
        //this.cImage.sprite = cd.cSprite;
        this.cStatus = cd.cStatus;
        this.isShown = cd.isShown;
        this.curIndexInHand = cd.curIndexInHand;
        this.cPrefab = cd.cPrefab;
        this.cPrefab.GetComponent<Image>().sprite = cd.cSprite;
    }

}
