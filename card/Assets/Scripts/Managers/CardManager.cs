using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
//using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Pool;
using DG.Tweening;
using System.Threading.Tasks;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    private List<ScriptableCards> _cards; // all cards info
    private List<ScriptableCards> availCards = new List<ScriptableCards>();
    private List<ScriptableCards> discardDeckData = new List<ScriptableCards>();
    public List<CardAndData> handCardAndData = new List<CardAndData>();
    //private ObjectPool<BaseCard> cardPool;

    [SerializeField] private Transform initCardShow;
    [SerializeField] private Transform nextCardShow;
    [SerializeField] private int maxHandSize, curHandSize;

    
    public Transform playerHand;                         // playerhand panel
    public Transform discardPanel;                                  // panel for discarded card/ burnt card
    
        
    private void Awake()
    {
        Instance = this;

        _cards = Resources.LoadAll<ScriptableCards>("Cards").ToList();

        curHandSize = 0;
        
    }

    
    // populate the drawPile
    public void initDrawPile()
    {
 
        foreach (ScriptableCards card in _cards)
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
    public async Task drawCards(int num)
    {
        var drawed = availCards.Count;
        if (num <= availCards.Count)
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
                        await showCardAnim(cd);
                    }
                    else
                    {
                        await discardAnim(cardToDrawData);
                    }
                }
            }
        }
        else
        {
            shuffleCard(discardDeckData, availCards);
            for (int i = 0; i < num - drawed; i++)
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
                        await showCardAnim(cd);
                    }
                    else
                    {
                        await discardAnim(cardToDrawData);
                    }
                }
            }
        }
        //for (int i = 0; i < num; i++)
        //{
        //    var cardToDrawData = getRandomCardFromList<BaseCard>(availCards, curStatus.inDraw);
        //    if (cardToDrawData != null)
        //    {
        //        availCards.Remove(cardToDrawData);
        //        if (handCardAndData.Count < maxHandSize)
        //        {
        //            CardAndData cd = new CardAndData(null, cardToDrawData);
        //            curHandSize = curHandSize < maxHandSize ? curHandSize + 1 : maxHandSize;
        //            cardToDrawData.curIndexInHand = curHandSize - 1;
        //            cardToDrawData.cStatus = curStatus.inHand;
        //            handCardAndData.Add(cd);
        //            await showCardAnim(cd);
        //        }
        //        else
        //        {
        //            discardCard(cardToDrawData);
        //        }
        //    }
        //    else
        //    {
        //        int needToDrawMore = num - i;
        //        shuffleCard(discardDeckData, availCards);
        //        drawCards(needToDrawMore);
                
        //    } 
        //}
        //await Task.Yield ();

    }
    
    
    // find card this is null (not instantiated), instantiate && LoadCardData, update isShown -> true
    public void instantiateCardData(List<CardAndData> cardAndData)
    {
        var cardNotInstantiated = cardAndData.Find(cd => cd.getCard() == null);
        if (cardNotInstantiated != null)                                                  // if there's still card not instantiated
        {
            //var cardInstance = Instantiate(cardNotInstantiated.getCardData().cPrefab, initCardShow);
            //cardInstance.transform.localScale = Vector3.zero;
            
            
            //cardNotInstantiated.getCardData().isShown = true;
            //cardInstance.loadCardData(cardNotInstantiated.getCardData());
            //cardNotInstantiated.setCard(cardInstance);

            //StartCoroutine(showCardAnim(cardNotInstantiated));
        }
        
    }

    private async Task showCardAnim(CardAndData cd)
    {
        var card = Instantiate(cd.getCardData().cPrefab, initCardShow);
        cd.getCardData().isShown = true;
        card.loadCardData(cd.getCardData());
        cd.setCard(card);
        card.transform.localScale = Vector3.zero;
        card.transform.DOScale(new Vector3(2,2,2), 0.5f);
        
        await card.transform.DOMove(nextCardShow.position, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            //await Task.Delay(100);
            card.transform.DOScale(Vector3.one, 0.1f);
            card.transform.SetParent(playerHand, false);

        }).AsyncWaitForCompletion();
        
    }
    
    public void dealCard(BaseCard cardDealt, BaseUnit target)
    {
        if (GameManager.Instance.activePlayer.curGauge >= cardDealt.cCost)
        {
            cardDealt.use(target);
            GameManager.Instance.activePlayer.curGauge -= cardDealt.cCost;
            curHandSize--;
            cardDealt.transform.DOKill();                                                           //kill the tween before destroy the obj
            discardCard(handCardAndData, cardDealt);
        }
        else
        {
            Debug.Log("doesn't have enough energy");
        }
        //var card = handCardAndData.Find(cd => cd.getCard().Equals(cardDealt));
        //if (card != null)
        //{
            //Debug.Log("dealt");
            //curHandSize--;
            //discardCard(handCardAndData, cardDealt);
        //}
 
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
    

   

    private async Task discardAnim(ScriptableCards dc)
    {
        discardDeckData.Add(dc);
        var card = Instantiate(dc.cPrefab, initCardShow);
        dc.cStatus = curStatus.inDiscard;
        card.transform.localScale = Vector3.zero;
        card.transform.DOScale(new Vector3(2, 2, 2), 0.1f);
        Debug.Log(card.transform.parent);
        await card.transform.DOMove(nextCardShow.position, 1f).SetEase(Ease.OutQuart).AsyncWaitForCompletion();
        //card.transform.DOMove()
        //card.transform.SetParent(discardPanel,false);
        await Task.Delay(1000);
        card.transform.DOKill();
        Destroy(card.gameObject);
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

    #region CardPool Methods


    
    #endregion

}
