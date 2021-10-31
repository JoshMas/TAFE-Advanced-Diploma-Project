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

    private float maxDenyGain = 0;
    private float denyGainTimer = 0;
    [SerializeField] private Image denialImage;
    private float denyWidth;
    private float denyHeight;

    public void Initialise()
    {
        barWidth = energyBarImage.rectTransform.rect.width;
        barheight = energyBarImage.rectTransform.rect.height;
        UpdateEnergyBar();

        denyWidth = denialImage.rectTransform.rect.width;
        denyHeight = denialImage.rectTransform.rect.height;
        UpdateDenialBar();
    }

    public bool HasEnergy()
    {
        return currentEnergy > 0;
    }

    public void Charge()
    {
        if(denyGainTimer <= 0)
        {
            currentEnergy += rateOfGain * Time.deltaTime;
            UpdateEnergyBar();
        }
    }

    /// <summary>
    /// Use this method to make the player take damage.
    /// It removes a chunk of health, and they cannot regain health by crouching for some time.
    /// If they take a hit at zero health, it's game over
    /// </summary>
    /// <param name="_damage">The amount of energy deducted</param>
    /// <param name="_time">The amount of time the player cannot regenerate energy</param>
    /// <returns>If the player has died, this returns true</returns>
    public bool TakeDamage(float _damage, float _time)
    {
        if(currentEnergy <= 0)
        {
            return true;
        }
        currentEnergy -= _damage;
        UpdateEnergyBar();
        if(_time > denyGainTimer)
        {
            denyGainTimer = _time;
        }
        maxDenyGain = denyGainTimer;
        UpdateDenialBar();
        return false;
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

    public void CountDownDenial()
    {
        if (denyGainTimer == 0)
            return;
        denyGainTimer -= Time.deltaTime;
        denyGainTimer = Mathf.Clamp(denyGainTimer, 0, maxDenyGain);
        UpdateDenialBar();
    }

    private void UpdateDenialBar()
    {
        float fraction = denyGainTimer / maxDenyGain;
        denialImage.rectTransform.sizeDelta = new Vector2(denyWidth, fraction * denyHeight);
    }
}
