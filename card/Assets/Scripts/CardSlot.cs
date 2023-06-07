using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private float xPos, yPos;
    
    public bool isEmpty;

    private void Awake()
    {
        this.isEmpty = true;
    }

    private void Start()
    {

        transform.SetParent(GameManager.Instance.mainCanvas.transform, false);           //set the parent to the canvas, while the worldposstays set to false
        transform.position = new Vector3(xPos, yPos, 0);
    }

}
