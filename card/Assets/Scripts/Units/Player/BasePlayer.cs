using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    private void Awake()
    {
        loadUnitData(unitData);
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.setMaxHealth(unitHealth);
        energyBar = GetComponentInChildren<EnergyBar>();
        energyBar.setMaxEnergy(maxGauge);

        GameManager.onGameStateChanged += playerTurnUpdate;

    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= playerTurnUpdate; 
    }
    private void playerTurnUpdate(GameState state)
    {
        if (state == GameState.playerTurn)
        {
            curGauge = maxGauge;
        }
    }

    private void Update()
    {
        healthBar.setHealth(curHealth);
        energyBar.setEnergy(curGauge);
        checkDeath();
        
        
    }
    public override void checkDeath()
    {
        isDead = curHealth > 0 ? false : true;
        if (isDead)
        {
            Debug.Log("destroyed");
            UnitManager.Instance.availPlayers.Remove(this);
            this.DOKill();
            Destroy(this.gameObject);
            if (UnitManager.Instance.availPlayers.Count == 0)
            {
                GameManager.Instance.updateGameState(GameState.resultState);
            }
            //Application.Quit();
        }
    }


   
}
