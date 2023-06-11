using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public static CardPool Instance;
    public List<BaseCard> cardPool;

    private void Awake()
    {
        //Instance = this;
    }

    private void Start()
    {
        //cardPool = new List<BaseCard>();

    }


}
