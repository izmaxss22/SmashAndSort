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
        EnergyManager = EnergyManager.Instance;
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

        textCountEnergy.text = DataManager.GetEnergyCount() + "/" + DataManager.MAX_ENERGY_COUNT;
        ts_forEnergyTimer = new TimeSpan(0, 0, 0, EnergyManager.count_secondsForNextEnergyTimer);
        textEnergyTimer.text = ts_forEnergyTimer.Minutes.ToString() + ":" + ts_forEnergyTimer.Seconds.ToString();
        StartCoroutine("EnergyTimerCounting");
    }

    public IEnumerator EnergyTimerCounting()
    {
        TimeSpan ts = new TimeSpan(0, 0, 0, -1);
        int count_maxEnergy = DataManager.MAX_ENERGY_COUNT;
        while (true)
        {
            ts_forEnergyTimer = ts_forEnergyTimer.Add(ts);
            //Если полностью отсчитал
            if (ts_forEnergyTimer.TotalSeconds == 0)
            {
                textCountEnergy.text = DataManager.GetEnergyCount() + 1 + "/" + count_maxEnergy;
                // Если энергия полная то заканчиваеться корутина
                if (DataManager.GetEnergyCount() == count_maxEnergy)
                {
                    textEnergyTimer.text = "00:00";
                    yield break;
                }
                // Иначе отсчет начинаеться заного
                else
                {
                    ts_forEnergyTimer = new TimeSpan(0, 0, 0, EnergyManager.energyTimerLenhgt);
                    textEnergyTimer.text = ts_forEnergyTimer.Minutes.ToString() + ":" + ts_forEnergyTimer.Seconds.ToString();
                }
            }
            else
            {
                textEnergyTimer.text = ts_forEnergyTimer.Minutes.ToString() + ":" + ts_forEnergyTimer.Seconds.ToString();
            }

            yield return new WaitForSeconds(1);
        }
    }


    private IEnumerator GiveEnergyAfferAdWatch()
    {
        EnergyManager.Disable_EnergAddingTimer();
        DataManager.Set_EnergyCount(DataManager.MAX_ENERGY_COUNT);

        AudioManager.PlayAudioSource(AudioManager.AudioSourcesIds.GET_ENERGY_SCREEN_AFFTER_AD_WATCH);

        gameObject.GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
    #endregion
}
