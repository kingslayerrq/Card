using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAndData 
{
    private BaseCard card;
    private ScriptableCards cardData;

    public CardAndData(BaseCard card, ScriptableCards cardData)
    {
        this.card = null;
        this.cardData = cardData;
    }

    public void setCardData(ScriptableCards newData)
    {
        this.cardData = newData;
    }
    public ScriptableCards getCardData()
    {
        return this.cardData;
    }

    public void setCard(BaseCard newCard)
    {
        if (this.card == null)
        {
            this.card = newCard;
        }
    }
    public BaseCard getCard()
    {
        if (card != null)
        {
            return card;
        }
        return null;
    }
}
