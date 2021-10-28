using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnergyBar
{
    [SerializeField] private float maxEnergy = 100;
    [SerializeField] private float rateOfGain = 25;
    [SerializeField] private Image energyBarImage;
    private float barWidth;
    private float barheight;
    private float currentEnergy = 50;

    public void Initialise()
    {
        barWidth = energyBarImage.rectTransform.rect.width;
        barheight = energyBarImage.rectTransform.rect.height;
        UpdateEnergyBar();
    }

    public bool HasEnergy()
    {
        return currentEnergy > 0;
    }

    public void Charge()
    {
        currentEnergy += rateOfGain * Time.deltaTime;
        UpdateEnergyBar();
    }

    public void Drain(float _rateOfDrain)
    {
        currentEnergy -= _rateOfDrain * Time.deltaTime;
        UpdateEnergyBar();
    }

    public void Spend(float _energyCost)
    {
        currentEnergy -= _energyCost;
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        float fraction = currentEnergy / maxEnergy;
        energyBarImage.rectTransform.sizeDelta = new Vector2(fraction * barWidth, barheight);
    }
}
