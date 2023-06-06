using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableCard")]
public class ScriptableCards : ScriptableObject
{
    public string cId;
    public string cName;
    public string cDescription;
    public Rarity cRarity;
    public Type cType;
    public Target cTarget;
    public BaseCard cardPrefab;
    public curStatus cStatus;


}
public enum Rarity {
    Common = 0,
    Rare = 1,
    Epic = 2,
    Legendary = 3,
    Sacred = 4
}

public enum Type
{
    Atk = 0,
    Heal = 1,
    StatusEffect = 2
}
public enum Target
{
    Self = 0,
    Enemy = 1,
    Ally = 2
}
public enum curStatus
{
    inDraw = 0,
    inHand = 1,
    inDiscard = 2,
    inPlay = 3,
    destroyed = 4
}
