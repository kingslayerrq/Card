using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    protected ScriptableCards cardData;
    public string cId;
    public string cName;
    public string cDescription;
    public Rarity cRarity;
    public Type cType;
    public Target cTarget;
    public curStatus cStatus;
    public BaseCard cPrefab;
    public bool isShown;
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
    }


    private void OnMouseEnter()
    {
        
    }

}
