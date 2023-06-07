using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    private List<ScriptableCards> _cards; // all cards info
    private List<ScriptableCards> availCards = new List<ScriptableCards>();
    private List<ScriptableCards> handDeckData = new List<ScriptableCards>();
    private List<ScriptableCards> discardDeckData = new List<ScriptableCards>();
    private List<BaseCard> handDeck = new List<BaseCard>(); //contains basecard objects
    private Dictionary<ScriptableCards, int> cardDict;
    [SerializeField] private int maxHandSize;
    [SerializeField] private List<CardSlot> cardSlotPos;

    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCards>("Cards").ToList();

        //cardSlotPos = new List<Transform>(maxHandSize);

    }

    private void Update()
    {
        instantiateCardData(handDeckData);
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

    // draw cardData to handDeckData
    public void drawCards(int num)
    {
        if (GameManager.Instance.gameState == GameState.drawState)
        {
            for (int i = 0 ; i < num; i++)
            {
                var cardToDrawData = getRandomCardFromList<BaseCard>(availCards, curStatus.inDraw);
                availCards.Remove(cardToDrawData);
                if (handDeckData.Count < maxHandSize)
                {          
                    handDeckData.Add(cardToDrawData);
                    cardToDrawData.cStatus = curStatus.inHand;     
                }
                else
                {
                    discardCard(cardToDrawData);               
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
    public void instantiateCardData(List<ScriptableCards> cardData)
    {
        //Debug.Log("inst");

        if (cardData.Any())
        {
            foreach (var card in cardData)
            {
                var cs = cardSlotPos.Where(cs=> cs.isEmpty).First();
                var cardInstance = Instantiate(card.cPrefab, GameManager.Instance.mainCanvas.transform);
                cardInstance.transform.position = cs.transform.position;
                cardInstance.transform.localScale = new Vector3(10, 10, 0);
                cs.isEmpty = false;
                cardInstance.loadCardData(card);
                cardInstance.isShown = true;
                cardData.Remove(card);
            }
            
        }
        
    }

    public void discardCard(ScriptableCards discarded)
    {
        discarded.cStatus = curStatus.inDiscard;
        Destroy(discarded.cPrefab);
        discardDeckData.Add(discarded);
    }

    // get random cardData
    private ScriptableCards getRandomCardFromList<T>(List<ScriptableCards> cardList, curStatus cardStatus) where T:BaseCard
    {

        return cardList.Where(c=> c.cStatus == cardStatus).OrderBy(o => Random.value).First();
    }
}
