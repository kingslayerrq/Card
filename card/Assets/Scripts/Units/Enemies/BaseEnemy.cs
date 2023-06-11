using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    protected Target target;
    protected BaseUnit targetUnit;
    public Transform playerPanel;
    protected BasePlayer[] availFoes ;

    private void Awake()
    {
        target = Target.None;
        targetUnit = null;
        loadUnitData(unitData);
        Debug.Log(unitData.unitName);
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.setMaxHealth(unitHealth);
        healthBar.setHealth(curHealth);

        Debug.Log(curHealth);
        playerPanel = UnitManager.Instance.playerPanel;

        availFoes = playerPanel.GetComponentsInChildren<BasePlayer>().Where(p => !p.isDead).ToArray();        //load available foes
        GameManager.onGameStateChanged += enemyAction;

    }


    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= enemyAction;
    }
    

    private void Update()
    {
        healthBar.setHealth(curHealth);
        checkDeath();
    }
    public override void checkDeath()
    {
        isDead = curHealth > 0 ? false : true;
        if (isDead)
        {
            Debug.Log("Enemy destroyed");
            Destroy(this.gameObject);
        }
    }
    private void enemyAction(GameState state)
    {
        if (state == GameState.enemyTurn)
        {
            takeAction();
        }
        
    }

    protected virtual void takeAction()
    {
        
    }

    protected virtual void endTurnForEnemy()
    {
        target = Target.None;                                                   // reset target
        targetUnit = null;
        GameManager.Instance.updateGameState(GameState.drawState);
    }

    public enum Target
    {
        Self = 0,
        Player = 1,
        Ally = 2,
        None = 3
    }
    
}
