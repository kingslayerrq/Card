using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    private List<ScriptableCards> _cards; // all cards info
    private List<ScriptableCards> availCards = new List<ScriptableCards>();
    private List<BaseCard> handDeck = new List<BaseCard>();
    private Dictionary<ScriptableCards, int> cardDict;
    [SerializeField] private int maxHandSize;
    [SerializeField] private List<Transform> cardSlotPos;

    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCards>("Cards").ToList();

        //cardSlotPos = new List<Transform>(maxHandSize);

    }

    private void Update()
    {
        displayHand();
    }
    // populate the drawPile
    public void initDrawPile()
    {
        foreach(ScriptableCards card in _cards)
        {
            for (int i = 0; i < 5 - (int)card.cRarity; i++)     // for now have (5-rarity)# copies of cards
            {
                var cardCopy = Instantiate(card);
                availCards.Add(cardCopy);
                cardCopy.cStatus = curStatus.inDraw;
                
            }
        }
        

    }

    public void drawCards(int num)
    {
        if (GameManager.Instance.gameState == GameState.drawState)
        {
            for (int i = 0 ; i < num; i++)
            {
                var cardToDrawData = getRandomCardFromList<BaseCard>(availCards, curStatus.inDraw);
                if (handDeck.Count < maxHandSize)
                {          
                    cardToDrawData.cStatus = curStatus.inHand;
                    var cardInstance = Instantiate(cardToDrawData.cPrefab);
                    cardInstance.loadCardData(cardToDrawData);
                    handDeck.Add(cardInstance);
                }
                else
                {
                    cardToDrawData.cStatus = curStatus.inDiscard;
                }
            }
        }
        
        
    }
    public void setCardSlotPos(int cardNum)
    {
        
    }

    public void fitCards()
    {
        
    }
    public void displayHand()
    {
        for (int i = 0; i < handDeck.Count; i++)
        {
            if (!handDeck[i].isShown)
            {
                //var cardToShow = Instantiate(handDeck[i].cPrefab);
                handDeck[i].transform.position = cardSlotPos[i].position;
                handDeck[i].isShown = true;
            }
            
        }
    }

    public void discardCard()
    {

    }

    // get random cardData
    private ScriptableCards getRandomCardFromList<T>(List<ScriptableCards> cardList, curStatus cardStatus) where T:BaseCard
    {

        return cardList.Where(c=> c.cStatus == cardStatus).OrderBy(o => Random.value).First();
    }
}
