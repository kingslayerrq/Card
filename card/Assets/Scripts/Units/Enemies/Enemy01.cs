using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;

public class Enemy01 : BaseEnemy
{
    [SerializeField] int atkPower = 2;
    [SerializeField] int powerUpValue = 1;
    private bool healedLastTurn = false;
    [SerializeField] int healNum = 2;
    private bool isPoweringUp = false;
    [SerializeField] int powerUpTurn;
    
    private void Start()
    {
        powerUpTurn = 0;
        
    }
    protected override void takeAction()
    {
        Debug.Log("taking action");
        // increase atkpower if ispowerup
        if (isPoweringUp)
        {
            powerUp();
        }
        targetUnit = selectTarget();
        switch(target)
        {
            case Target.Self:
                handleActionOnSelf();
                break;
            case Target.Player:
                attack(targetUnit);
                break;
            default:
                break;

        }

        endTurnForEnemy();
        

    }

    private BaseUnit selectTarget()
    {
        var rand = Random.Range(0, 1);
        if (healedLastTurn && isPoweringUp)
        {
            target = Target.Player;
            return availFoes.Where(f => !f.isDead).OrderBy(o => UnityEngine.Random.value).First();
        }
        if (curHealth < unitHealth / 4 && !healedLastTurn)
        {
            target = Target.Self;
            return this;
        }
        else if (curHealth > unitHealth / 2 && !isPoweringUp)
        {
            rand = Random.Range(0, 4);
            target = rand > 0 ? Target.Self : Target.Player;
            return rand > 0 ? this : availFoes.Where(f => !f.isDead).OrderBy(o => UnityEngine.Random.value).First();        
        }
        else
        {
            foreach (BasePlayer player in availFoes)
            {
                if (atkPower >= player.curHealth)
                {
                    target = Target.Player;
                    return player;
                }  
            }
        }
        
        target = rand == 0? Target.Self : Target.Player;
        return Random.Range(0,1) == 0? this : availFoes.Where(f => !f.isDead).OrderBy(o => UnityEngine.Random.value).First();


    }

    private void attack(BaseUnit unit)
    {
        healedLastTurn = false;
        Debug.Log("attacking" + unit.unitName);
        unit.curHealth -= atkPower;

        
    }

  
    private void heal(BaseUnit unit)
    {
        Debug.Log("Healing" + unit.unitName);
        unit.curHealth = Mathf.Min(unit.curHealth + healNum, unit.unitHealth);
        
    }
    private void powerUp()
    {
        Debug.Log("im powering up");
        if (!isPoweringUp)
        {
            isPoweringUp = true;
            healedLastTurn = false;
        }
        powerUpTurn++;
        atkPower += powerUpTurn * powerUpValue;
    }

    private void doNothing()
    {
        Debug.Log("im not doing anything this turn!");
    }

    private void handleActionOnSelf()
    {
        if (curHealth <= unitHealth/4 || (!healedLastTurn && curHealth + healNum <= unitHealth))
        {
            heal(this);
        }
        else
        {
            if (!isPoweringUp)
            {
                powerUp();
            }
            else
            {
                doNothing();
            }
        }
    }
}
