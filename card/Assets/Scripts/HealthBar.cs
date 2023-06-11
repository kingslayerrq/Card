using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;

    private void Start()
    {
        
    }
    public void setMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        
    }

    public void setHealth(int curHealth)
    {
        slider.value = curHealth;
    }

}
