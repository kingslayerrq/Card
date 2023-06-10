using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public string unitName;
    public int unitHealth;
    public int curHealth;
    public int curGauge;
    public int maxGauge;
    public bool isDead;
    protected ScriptableUnit unitData;

    

    public virtual void loadUnitData(ScriptableUnit unitData)
    {
        this.unitName = unitData.unitName;
        this.unitHealth = unitData.unitHealth;
        this.curHealth = unitData.curHealth;
        this.curGauge = unitData.curGauge;
        this.maxGauge = unitData.maxGauge;
        this.isDead = unitData.isDead;
    }

    public virtual void checkUnitStatus()
    {
        
    }
}
