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
        DataManager.Instance.OnChangeCoinsCount += OnChangeCoinsCount;

        vector2ForEnergyProgressBar.y = rectTransForEnergyProgressBar.sizeDelta.y;
        OnChangeCoinsCount();
    }

    private void OnDestroy()
    {
        DataManager.Instance.OnChangeCoinsCount -= OnChangeCoinsCount;
    }


    public void OnChangeCoinsCount()
    {
        textCountCoins.text = DataManager.Instance.GetCoinsCount().ToString();
    }
}
