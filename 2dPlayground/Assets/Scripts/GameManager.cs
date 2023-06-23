using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScriptableCards cardData;
    //[SerializeField] private GameObject cardPrefab;
    [SerializeField] private HorizontalLayoutGroup panel;
    private List<BaseCard> cards = new List<BaseCard>();
    private void Start()
    {
        var card = new BaseCard(cardData);
        cards.Add(card);
        
        instCard.transform.SetParent(panel.transform);
        Debug.Log(instCard);
   
    }
}
