using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
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
            Debug.Log("Enemy destroyed");
            Destroy(this.gameObject);
        }
    }
}
