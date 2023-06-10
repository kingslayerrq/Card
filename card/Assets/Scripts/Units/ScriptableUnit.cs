using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableUnit")]
public class ScriptableUnit : ScriptableObject
{
    public BaseUnit unitPrefab;
    public Faction unitFaction;
    public string unitName;
    public int unitHealth;
    public int curHealth;
    public int curGauge;
    public int maxGauge;
    public bool isDead;

    
}

public enum Faction
{
    Player = 0,
    Enemy = 1,
    Ally = 2
}

