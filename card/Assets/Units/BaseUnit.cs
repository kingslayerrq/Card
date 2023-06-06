using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    protected string unitName;
    protected int unitHealth;
    protected int curGauge;
    protected int maxGauge;
    protected bool isDead;
    public BaseUnit(string unitName, int unitHealth, int curGauge, int maxGauge, bool isDead)
    {
        this.unitName = unitName;
        this.unitHealth = unitHealth;
        this.curGauge = curGauge;
        this.maxGauge = maxGauge;
        this.isDead = isDead;
    }
}
