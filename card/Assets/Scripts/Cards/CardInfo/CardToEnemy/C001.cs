using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C001 : BaseCard
{
    [SerializeField] private int slashDmg;
    private void Start()
    {
        slashDmg = 5;
    }

    public override void use(BaseUnit enemy)
    {
        Debug.Log("enemy health before: " + enemy.curHealth);
        enemy.curHealth -= slashDmg;
        Debug.Log("enemy health after: " + enemy.curHealth);
    }
}
