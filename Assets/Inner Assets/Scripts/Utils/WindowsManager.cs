using System;
using System.Collections;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    public static WindowsManager Instance;

    public Transform mainCanvas;
    public GameObject prefabForPanelCoinsEndDesc;
    public GameObject prefabForMainScreen;
    public GameObject prefabForGameManager;
    public GameObject prefabForSettingsScreen;
    public GameObject prefabForGetEnergyScreen;
    public GameObject prefabForReviveScreen;
    public GameObject prefabForAllLevelsNotcieScreen;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void FromMainToSettingsScreen()
    {
        Instantiate(prefabForSettingsScreen, mainCanvas);
    }

    public void FromMainScreenToGame(GameObject mainScreen, GameObject gameManager)
    {
        StartCoroutine(SwitchFromMainScreenToGame(mainScreen, gameManager));
    }

    private IEnumerator SwitchFromMainScreenToGame(GameObject mainScreen, GameObject gameManager)
    {
        var panel = GameObject.Find("energyAndCoinsPanel");

        gameManager.GetComponent<GameManager>().InitGameFromMainMenu();
        panel.GetComponent<Animator>().SetTrigger("hide");
        mainScreen.GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        Destroy(panel);
        Destroy(mainScreen);
        gameManager.GetComponent<GameManager>().ShowUI();
        yield break;
    }

    public void FromMainToGetEnergyScreen(GameObject gameObject)
    {
        Instantiate(prefabForGetEnergyScreen, mainCanvas);
    }

    public void FromGameToAffterGameScreen(
        GameObject gameManager,
        int levelNumber,
        bool isLose,
        bool reviveIsUsed)
    {
        var go = Instantiate(prefabForReviveScreen, mainCanvas);
        go.GetComponent<ReviveScreen>().InitScreen(gameManager, levelNumber, isLose, reviveIsUsed);
        Instantiate(prefabForPanelCoinsEndDesc, mainCanvas);
    }

    public void FromAfterGameToGetEnergyScreen(GameObject gameObject)
    {
        Instantiate(prefabForGetEnergyScreen, mainCanvas);
    }

    public void CreateAllLevelsNoticeScreen()
    {
        Instantiate(prefabForAllLevelsNotcieScreen, mainCanvas);
    }
}
