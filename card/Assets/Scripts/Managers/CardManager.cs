using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    private List<ScriptableCards> _cards; // all cards info
    private List<ScriptableCards> availCards = new List<ScriptableCards>();
    private List<ScriptableCards> discardDeckData = new List<ScriptableCards>();
    public List<CardAndData> handCardAndData = new List<CardAndData>();
    
    [SerializeField] private int maxHandSize, curHandSize;

    [SerializeField] private float destroyDelay = 2f;
    public Transform playerHand;                         // playerhand panel
    public Transform p;                                  // panel for discarded card/ burnt card
    
        
    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCards>("Cards").ToList();

        curHandSize = 0;

        
    }

    private void Update()
    {
        
        instantiateCardData(handCardAndData);

        
    }
    // populate the drawPile
    public void initDrawPile()
    {
        foreach(ScriptableCards card in _cards)
        {
            for (int i = 0; i < 6 - (int)card.cRarity; i++)     // for now have (5-rarity)# copies of cards
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
        
        for (int i = 0; i < num; i++)
        {
            var cardToDrawData = getRandomCardFromList<BaseCard>(availCards, curStatus.inDraw);
            if (cardToDrawData != null)
            {
                availCards.Remove(cardToDrawData);
                if (handCardAndData.Count < maxHandSize)
                {
                    CardAndData cd = new CardAndData(null, cardToDrawData);
                    curHandSize = curHandSize < maxHandSize ? curHandSize + 1 : maxHandSize;
                    cardToDrawData.curIndexInHand = curHandSize - 1;
                    cardToDrawData.cStatus = curStatus.inHand;
                    handCardAndData.Add(cd);

                }
                else
                {
                    discardCard(cardToDrawData);
                }
            }
            else
            {
                int needToDrawMore = num - i;
                shuffleCard(discardDeckData, availCards);
                drawCards(needToDrawMore);
                return;
            }
            
        }

    }
    
    
    // find card this is null (not instantiated), instantiate && LoadCardData, update isShown -> true
    public void instantiateCardData(List<CardAndData> cardAndData)
    {
        var cardNotInstantiated = cardAndData.Find(cd => cd.getCard() == null);
        if (cardNotInstantiated != null)                                                  // if there's still card not instantiated
        {
            var cardInstance = Instantiate(cardNotInstantiated.getCardData().cPrefab, playerHand);
            cardNotInstantiated.getCardData().isShown = true;
            cardInstance.loadCardData(cardNotInstantiated.getCardData());
            cardNotInstantiated.setCard(cardInstance);
        }
        
    }


    // just destroying card basically
    public void dealCard(BaseCard cardDealt)
    {
        var card = handCardAndData.Find(cd => cd.getCard().Equals(cardDealt));
        if (card != null)
        {
            Debug.Log("dealt");
            curHandSize--;
            discardCard(handCardAndData, cardDealt);
        }
 
    }

    public void shuffleCard(List<ScriptableCards> shuffleFrom, List<ScriptableCards> shuffleInto)
    {
        int count = shuffleFrom.Count;
        for (int i = 0; i < count; i++)
        {
            var c = shuffleFrom.OrderBy(o => UnityEngine.Random.value).FirstOrDefault();
            if (c != null)
            {
                c.cStatus = curStatus.inDraw;
                shuffleFrom.Remove(c);
                shuffleInto.Add(c);
            }
            
        }
        Debug.Log("shuffled");
    }
    

    public void discardCard(ScriptableCards discarded)
    {
        discarded.cStatus = curStatus.inDiscard;
        discardDeckData.Add(discarded);
        var dc = Instantiate(discarded.cPrefab, p);
        StartCoroutine(DestroyAfterDelayCoroutine(dc, destroyDelay));
    }

    IEnumerator DestroyAfterDelayCoroutine(BaseCard obj, float delay)
    {
        Debug.Log("handfull...destroying");
        yield return new WaitForSeconds(delay);
        Destroy(obj.gameObject);
    }

    public void discardCard(List<CardAndData> cdList, BaseCard card)
    {
        var cd = cdList.Where(cd => cd.getCard() == card).FirstOrDefault();
        if (cd != null)
        {
            cd.getCardData().cStatus = curStatus.inDiscard;                           // set the carddata to inDiscard, card obj will be destroyed
            
            discardDeckData.Add(cd.getCardData());
            Destroy(card.gameObject);
            cdList.Remove(cd);

        }
        
        
    }

    // get random cardData, or null
    private ScriptableCards getRandomCardFromList<T>(List<ScriptableCards> cardList, curStatus cardStatus) where T:BaseCard
    {

        return cardList.Where(c=> c.cStatus == cardStatus).OrderBy(o => UnityEngine.Random.value).FirstOrDefault();
    }

    
}
