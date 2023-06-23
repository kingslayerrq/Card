using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C002 : BaseCard
{
    [SerializeField] private int healNum;
    
    private void Start()
    {
        healNum = 3;
    }
    public override void use(BaseUnit player)
    {
        //Debug.Log("previous health: " + player.curHealth);
        player.curHealth = Mathf.Min(player.curHealth + healNum, player.unitHealth);
        //Debug.Log("After healing: " + player.curHealth);
    }
}
