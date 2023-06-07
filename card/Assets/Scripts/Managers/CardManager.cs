using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
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
    [SerializeField] private int maxHandSize, curHandSize;
    [SerializeField] private List<CardSlot> cardSlots = new List<CardSlot>();
    

    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCards>("Cards").ToList();

        curHandSize = 0;

        
        for (int i = 0; i < maxHandSize; i++)
        {
            
            var csInstance = new CardSlot(0, 0, i, true);
            cardSlots.Add(csInstance);
        }
        
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
                    curHandSize++;
                    cardToDrawData.curIndexInHand = curHandSize - 1;         
                    cardToDrawData.cStatus = curStatus.inHand;     
                }
                else
                {
                    discardCard(cardToDrawData);               
                }
            }
        }
        fitCards(handDeck);
    }
    
    
    public void instantiateCardData(List<ScriptableCards> cardData)
    {
        //Debug.Log("inst");

        if (cardData.Any())
        {
            for (int i = 0; i < cardData.Count; i++)
            {
                var cData = cardData[i];
                var cs = cardSlots.Where(cs=> cs.isEmpty).First();
                cs.setCardSlotPos(updateCardPos(cData.curIndexInHand));  // set cardSlot transform position
                cs.setCSIndex(cData.curIndexInHand);                                  //set cardSlot(index) with the card's curIndex in hand
                var cardInstance = Instantiate(cData.cPrefab, GameManager.Instance.mainCanvas.transform);
                cardInstance.transform.position = cs.getCardSlotPos();
                cardInstance.transform.localScale = new Vector3(10, 10, 0);
                cs.isEmpty = false;                                                   // update cardSlot.empty
                cData.cSlot = cs;                                                     // set the card's cardSlot to cs
                cardInstance.loadCardData(cData);
                handDeck.Add(cardInstance);                                           // add to handdeck<BaseCard object>
                cardInstance.isShown = true;
                cardData.Remove(cData);
            }
        }
        
    }
    // return position nased on total handdeckdata number && the card's curindex;
    public Vector3 updateCardPos(int curIndex)
    {
        
        if (curHandSize!= 0)
        {
            float CardPosition = 16 / curHandSize;
            float x = -14 + CardPosition / 2 + (curIndex) * CardPosition;
            float y = 0;
            float z = 0;
            return new Vector3(x, y, z);
        }
        return new Vector3(1,1,1);
    }

    // fit/updating each card's position
    public void fitCards(List<BaseCard> cardList)
    {
        foreach (BaseCard card in cardList)
        {
            card.transform.position = updateCardPos(card.curIndexInHand);
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
