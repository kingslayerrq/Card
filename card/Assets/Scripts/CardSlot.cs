using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot 
{
    [SerializeField] private float xPos, yPos;
    
    public bool isEmpty;
    private int csIndex;

    public CardSlot(float xPos, float yPos, int csIndex, bool isEmpty)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.isEmpty = isEmpty;
        this.csIndex = csIndex;
    }

    public void setCardSlotPos(Vector3 v)
    {
        this.xPos = v.x;
        this.yPos = v.y;
    }

    public Vector3 getCardSlotPos()
    {
        return new Vector3(this.xPos, this.yPos, 0);
    }

    public void setCSIndex(int csIndex)
    {
        this.csIndex = csIndex;
    }
    
    public int getCSIndex()
    {
        return this.csIndex;
    }
    private void Awake()
    {
        
    }

    private void Start()
    {

        //transform.SetParent(GameManager.Instance.mainCanvas.transform, false);           //set the parent to the canvas, while the worldposstays set to false
        //transform.position = new Vector3(xPos, yPos, 0);
    }

}
