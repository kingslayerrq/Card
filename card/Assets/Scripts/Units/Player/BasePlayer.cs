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
    }

    private void Start()
    {
        
        
        
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
            Debug.Log("destroyed");
            Destroy(this.gameObject);
            //Application.Quit();
        }
    }


   
}
