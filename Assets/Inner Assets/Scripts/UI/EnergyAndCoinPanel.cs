using System;
using UnityEngine;
using UnityEngine.UI;

public class EnergyAndCoinPanel : MonoBehaviour
{
    public Text textCountCoins;
    public Text textCountEnergy;
    private Vector2 vector2ForEnergyProgressBar = new Vector2(0, 39);
    public RectTransform rectTransForEnergyProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        name = "energyAndCoinsPanel";
        DataManager.Instance.OnChangeEnergyCount += OnChangeEnergyCount;
        DataManager.Instance.OnChangeCoinsCount += OnChangeCoinsCount;

        vector2ForEnergyProgressBar.y = rectTransForEnergyProgressBar.sizeDelta.y;
        OnChangeEnergyCount();
        OnChangeCoinsCount();
    }

    private void OnDestroy()
    {
        DataManager.Instance.OnChangeEnergyCount -= OnChangeEnergyCount;
        DataManager.Instance.OnChangeCoinsCount -= OnChangeCoinsCount;
    }

    public void OnChangeEnergyCount()
    {
        int energyCount = DataManager.Instance.GetEnergyCount();
        int maxEnergyCount = DataManager.MAX_ENERGY_COUNT;
        if (energyCount > maxEnergyCount)
        {
            vector2ForEnergyProgressBar.x = 158;
            rectTransForEnergyProgressBar.sizeDelta = vector2ForEnergyProgressBar;
        }
        else if (energyCount > 0)
        {
            // Вычисление того на сколько осталось энергии в процентах
            float progressOnPercent = 100 / ((float)maxEnergyCount / energyCount);
            // Получение длины (3.65 это 1% длины прогресс бара)
            int energyProgressLenght = (int)Math.Round(1.58f * progressOnPercent);
            vector2ForEnergyProgressBar.x = energyProgressLenght;
            rectTransForEnergyProgressBar.sizeDelta = vector2ForEnergyProgressBar;
        }
        else
        {
            vector2ForEnergyProgressBar.x = 0;
            rectTransForEnergyProgressBar.sizeDelta = vector2ForEnergyProgressBar;
        }

        textCountEnergy.text = energyCount.ToString() + "/" + maxEnergyCount.ToString();
    }

    public void OnChangeCoinsCount()
    {
        textCountCoins.text = DataManager.Instance.GetCoinsCount().ToString();
    }
}
