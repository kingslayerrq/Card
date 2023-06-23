using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public void setMaxEnergy(int maxEnergy)
    {
        slider.maxValue = maxEnergy;

    }

    public void setEnergy(int curEnergy)
    {
        slider.value = curEnergy;
    }
}
