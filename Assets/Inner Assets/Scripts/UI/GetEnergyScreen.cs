using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GetEnergyScreen : MonoBehaviour
{
    private DataManager DataManager;
    private EnergyManager EnergyManager;
    private AudioManager AudioManager;

    public Text textCountEnergy;
    public Text textEnergyTimer;
    public Button buttonGetEnergyCont;

    private bool adIsShowed = false;

    public Sprite sprite_forNotActiveGetEnergyButton;
    public Sprite sprite_forActiveGetEnergyButton;

    private void Start()
    {
        DataManager = DataManager.Instance;
        AudioManager = AudioManager.Instance;
        InitGetEnergyCont();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            // Если фокус потерян не по причине показа рекламы
            if (adIsShowed == false) Destroy(gameObject);
    }

    public void OnClick_BuyEnergyButton()
    {
        adIsShowed = true;
        AudioManager.PlayAudioSource(AudioManager.AudioSourcesIds.GET_ENERGY_SCREEN_BUTTON_GET, 1.5f);
        // SayKit.showRewarded((state) =>
        // {
        //     adIsShowed = false;
        //
        //     if (state == true)
        //     {
        //         StartCoroutine(GiveEnergyAfferAdWatch());
        //     }
        // });
    }

    public void OnClick_CloseButton()
    {
        AudioManager.PlayAudioSource(AudioManager.AudioSourcesIds.GET_ENERGY_SCREEN_CLOSE_SCREEN);
        StopCoroutine("EnergyTimerCounting");
        StartCoroutine(CloseScreen());
    }

    private IEnumerator CloseScreen()
    {
        gameObject.GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    #region Область энергии
    private TimeSpan ts_forEnergyTimer;
    public void InitGetEnergyCont()
    {
        //Если реклама не доступна то кнопка тоже
        // if (SayKit.isRewardedAvailable() == false)
        // {
        //     buttonGetEnergyCont.GetComponent<Image>().sprite = sprite_forNotActiveGetEnergyButton;
        //     buttonGetEnergyCont.interactable = false;
        // }

    }
    #endregion
}
