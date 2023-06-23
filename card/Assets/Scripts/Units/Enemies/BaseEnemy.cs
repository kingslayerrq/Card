using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    protected Target target;
    protected BaseUnit targetUnit;
    //public Transform playerPanel;
    protected List<BasePlayer> availFoes ;

    private void Awake()
    {
        target = Target.None;
        targetUnit = null;
        loadUnitData(unitData);
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.setMaxHealth(unitHealth);
        healthBar.setHealth(curHealth);
        //playerPanel = UnitManager.Instance.playerPanel;

        //availFoes = playerPanel.GetComponentsInChildren<BasePlayer>().Where(p => !p.isDead).ToArray();        //load available foes
        availFoes = UnitManager.Instance.availPlayers;
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
            UnitManager.Instance.availEnemies.Remove(this);
            Debug.Log("Enemy destroyed");
            Destroy(this.gameObject);
            Debug.Log(UnitManager.Instance.availEnemies.Count);
            if (UnitManager.Instance.availEnemies.Count == 0)
            {
                GameManager.Instance._winner = GameManager.Instance.activePlayer;
                GameManager.Instance.updateGameState(GameState.resultState);
            }
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
