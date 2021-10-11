using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReviveScreen : MonoBehaviour
{
    private int levelNumber;
    private GameObject gameManager;
    public Text textForLevelNumber;
    public GameObject textForLose;
    public GameObject textForSucces;

    public GameObject buttonHome;
    public GameObject buttonNext;
    public GameObject buttonAgain;

    public GameObject contForRevive;
    public Button buttonReviveForAd;
    public Button buttonReviveForCoins;

    public Sprite spriteForButtonReviveForAddEnabled;
    public Sprite spriteForButtonReviveForCoinsEnabled;

    public GameObject contForLockItems;


    public void InitScreen(GameObject gameManager, int levelNumber, bool isLose, bool reviveIsUsed)
    {
        this.levelNumber = levelNumber;
        this.gameManager = gameManager;
        textForLevelNumber.text = "LEVEL " + (levelNumber + 1).ToString();
        if (isLose)
        {
            textForLose.SetActive(true);
            buttonAgain.SetActive(true);

            if (!reviveIsUsed)
            {
                contForRevive.SetActive(true);
                InitReviveButtons();
            }
        }
        else
        {
            UIParticlesManager.Instance.PlayVictoryParticle();
            textForSucces.SetActive(true);
            buttonNext.SetActive(true);
        }

        // todo
        //StartCoroutine(ShowUssualAd(isLose));
    }

    private void InitReviveButtons()
    {
        // if (SayKit.isRewardedAvailable())
        // {
        //     buttonReviveForAd.interactable = true;
        //     buttonReviveForAd.GetComponent<Image>().sprite = spriteForButtonReviveForAddEnabled;
        //     buttonReviveForAd.GetComponent<Animator>().enabled = true;
        // }
        if (DataManager.Instance.GetCoinsCount() >= 150)
        {
            buttonReviveForCoins.interactable = true;
            buttonReviveForCoins.GetComponent<Image>().sprite = spriteForButtonReviveForCoinsEnabled;
        }
    }

    #region СОБЫТИЯ
    public void OnClickButtonReviveForAd()
    {
        // SayKit.showRewarded((state) =>
        // {
        //     if (state == true)
        //     {
        //         StartCoroutine(Revive());
        //     }
        // });
    }

    public void OnClcikButtonReviveForCoins()
    {
        int coinsCount = DataManager.Instance.GetCoinsCount();
        DataManager.Instance.SetCoinsCount(coinsCount - 150);
        buttonReviveForAd.interactable = false;
        buttonReviveForCoins.interactable = false;
        StartCoroutine(Revive());
    }

    public void OnClickButtonHome()
    {
        buttonHome.GetComponent<Button>().interactable = false;
        StartCoroutine(ToHomeScreen());
    }

    public void OnClickButtonNext()
    {
        // Проверка на то последний ли это доступный в игре уровень
        if (DataManager.Instance.dataForLevelGrouper.Length > levelNumber + 1)
        {

            var energyCount = DataManager.Instance.GetEnergyCount();
            if (energyCount == 0)
            {
                AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.SHOW_ENERGY_SCREEN);
                VibrationManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Failure);

                WindowsManager.Instance.FromAfterGameToGetEnergyScreen(gameObject);
            }
            else
            {
                buttonNext.GetComponent<Button>().interactable = false;
                DataManager.Instance.Set_EnergyCount(--energyCount);

                StartCoroutine(SwitchToNextLevel());
            }
        }
        else
        {
            WindowsManager.Instance.CreateAllLevelsNoticeScreen();
        }
    }

    public void OnClickButtonAgain()
    {
        var energyCount = DataManager.Instance.GetEnergyCount();
        if (energyCount == 0)
        {
            AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.SHOW_ENERGY_SCREEN);
            VibrationManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Failure);

            WindowsManager.Instance.FromAfterGameToGetEnergyScreen(gameObject);
        }
        else
        {
            buttonNext.GetComponent<Button>().interactable = false;
            buttonAgain.GetComponent<Button>().interactable = false;
            DataManager.Instance.Set_EnergyCount(--energyCount);
            StartCoroutine(StartLevelAgain());
        }
    }

    #endregion
    // Организация перехода в домашнее окно
    private IEnumerator ToHomeScreen()
    {
        GetComponent<Animator>().SetTrigger("hideForUIPhase1");
        yield return new WaitForSeconds(0.2F);
        Destroy(gameManager);
        var mainCanvas = WindowsManager.Instance.mainCanvas;
        var mainScreen = WindowsManager.Instance.prefabForMainScreen;
        var go = Instantiate(mainScreen, mainCanvas);
        go.transform.SetSiblingIndex(go.transform.GetSiblingIndex() - 1);
        GetComponent<Animator>().SetTrigger("hideForUIPhase2");
        yield return new WaitForSeconds(0.7F);
        Destroy(gameObject);
        yield break;
    }

    // Организация перехода обратно в игру после возрождения
    private IEnumerator Revive()
    {
        var panel = GameObject.Find("energyAndCoinsPanel");
        panel.GetComponent<Animator>().SetTrigger("hide");
        GetComponent<Animator>().SetTrigger("hideForGame");
        yield return new WaitForSeconds(0.7f);
        gameManager.GetComponent<GameManager>().ShowUI();
        gameManager.GetComponent<GameManager>().OnGameRevive();
        Destroy(gameObject);
        Destroy(panel);
        yield break;
    }

    // Организация перехода в игру на новый уровень
    private IEnumerator SwitchToNextLevel()
    {
        int localLevelNumber = levelNumber + 2;

        GetComponent<Animator>().SetTrigger("hideForUIPhase1");
        yield return new WaitForSeconds(0.2F);
        Destroy(gameManager);
        var panel = GameObject.Find("energyAndCoinsPanel");
        // Содаю новый gm с новым уровнем
        var gameManagerNew = WindowsManager.Instance.prefabForGameManager;
        var gm = Instantiate(gameManagerNew);
        gm.transform.SetSiblingIndex(3);
        var levels = DataManager.Instance.dataForLevelGrouper[levelNumber + 1];
        gm.GetComponent<GameManager>().InitGame(levels);

        panel.GetComponent<Animator>().SetTrigger("hide");
        GetComponent<Animator>().SetTrigger("hideForUIPhase2");
        yield return new WaitForSeconds(0.7F);
        gm.GetComponent<GameManager>().ShowUI();
        Destroy(panel);
        Destroy(gameObject);
        yield break;
    }

    // Организация перехода в игру на уровень на котором проиграл
    private IEnumerator StartLevelAgain()
    {
        int localLevelNumber = levelNumber;

        GetComponent<Animator>().SetTrigger("hideForUIPhase1");
        yield return new WaitForSeconds(0.2F);
        Destroy(gameManager);
        var panel = GameObject.Find("energyAndCoinsPanel");
        // Содаю новый gm со старым уровнем
        var gameManagerNew = WindowsManager.Instance.prefabForGameManager;
        var gm = Instantiate(gameManagerNew);
        gm.transform.SetSiblingIndex(3);
        var levels = DataManager.Instance.dataForLevelGrouper[levelNumber];
        gm.GetComponent<GameManager>().InitGame(levels);

        panel.GetComponent<Animator>().SetTrigger("hide");
        GetComponent<Animator>().SetTrigger("hideForUIPhase2");
        yield return new WaitForSeconds(0.7F);
        gm.GetComponent<GameManager>().ShowUI();
        Destroy(panel);
        Destroy(gameObject);
        yield break;
    }

    //// Корутина для запускап обычной рекламы
    //private IEnumerator ShowUssualAd(bool isLose)
    //{
    //    //string adUsualId = DataManager.adPlacementUsual;
    //    //if (Advertisement.IsReady(adUsualId))
    //    //{
    //    int gamesCount = DataManager.Instance.GetGamesCountForAd();
    //    if (gamesCount == 3)
    //    {
    //        // Отображение прозрачного контейнера поверх всего ui чтобы бдил ожидание завершения партиклов победы
    //        contForLockItems.SetActive(true);
    //        //  Небольшая паузка после игры
    //        if (isLose == false)
    //        {
    //            yield return new WaitForSeconds(1.5F);
    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(0.85F);
    //        }
    //        //Advertisement.Show(DataManager.adPlacementUsual);
    //    }
    //    yield break;
    //    //}
    //    //else
    //    //{
    //    //    yield break;
    //    //}
    //}
}

// todo после показа обычной рекламы делать контейнер доступным
//        if (placementId == DataManager.adPlacementUsual)
//{
//    contForLockItems.SetActive(false);
//}






