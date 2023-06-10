using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    private void Update()
    {
        checkUnitStatus();
    }
    public override void checkUnitStatus()
    {
        isDead = curHealth > 0 ? false : true;
        if (isDead)
        {
            Debug.Log("Player destroyed");
            Destroy(this.gameObject);
        }
    }
}
