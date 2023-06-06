using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    public BasePlayer(string unitName, int unitHealth, int curGauge, int maxGauge, bool isDead) : base(unitName, unitHealth, curGauge, maxGauge, isDead)
    {

    }
}
