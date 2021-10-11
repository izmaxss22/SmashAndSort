using System;
using System.Collections.Generic;
using EZCameraShake;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject dimmer;

    public GameObject contForLevelsInfoCont;
    public GameObject contForTimer;
    public Animator animatorForContForTiemer;
    public Text textForTimer;
    public Image mainContForChangeColorButtons;
    public GameObject contForChangeColorButtons;
    public GameObject contForLockChangeColorButtonIcon;

    public GameObject prefabForLevelInfoItem;
    private List<Image> createdLevelItems = new List<Image>();
    public Sprite completedLevelIcon;

    public Sprite spriteForTimerColor;
    public Sprite spriteForTimerGlobal;

    public Animator contForFullCollorEffect;

    public GameObject prefabForChangeColorButton;
    // Ключи это айди цвета который закреплен за этой кнопкой
    private Dictionary<int, Image> createdChangeColorButtons = new Dictionary<int, Image>();
    private Dictionary<int, Text> createdCountTextForChangeColorButtons = new Dictionary<int, Text>();
    private int pickedColorId;

    public void CreateUI(GameManager.GameData gameData)
    {
        pickedColorId = gameData.levelDatas[gameData.subLevelInProgressNumber].usedColors[0].colorId;

        CreateUICameraData();
        CreateChangeColorButton(gameData);
        CreateLevelsInfoCont(gameData);
        SetCanvasItemsForSubLevel(gameData);
        //todo
        mainContForChangeColorButtons.gameObject.SetActive(false);
    }

    // Установка камеры которая рендерит этот игровой ui
    public void CreateUICameraData()
    {
        uiCanvas.GetComponent<Canvas>().worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        uiCanvas.GetComponent<Canvas>().planeDistance = 80;
    }

    // Создание кнопкок для смены цвета, исходя их всех цветов на подуровне
    public void CreateChangeColorButton(GameManager.GameData gameData)
    {
        int subLevelNumber = gameData.subLevelInProgressNumber;
        var usedColors = gameData.levelDatas[subLevelNumber].usedColors;
        for (int i = 0; i < usedColors.Count; i++)
        {
            var go = Instantiate(prefabForChangeColorButton, contForChangeColorButtons.transform);
            // Установка цвета кнопки
            var color = usedColors[i].GetColor();
            go.GetComponent<Image>().color = color;
            // Установка колбэка нажатия кнопки
            var colodId = usedColors[i].colorId;
            go.GetComponent<Button>().onClick.AddListener(() => OnClickChangeColorButton(colodId));
            // Установка количества цвета
            var colorCount = usedColors[i].colorCounts;
            var buttonCountText = go.GetComponentInChildren<Text>();
            buttonCountText.text = colorCount.ToString();

            createdChangeColorButtons.Add(colodId, go.GetComponent<Image>());
            createdCountTextForChangeColorButtons.Add(colodId, buttonCountText);
        }
    }

    // Удаление всех ранее созанныдх кнопок
    public void DeleteChangeColorButtons()
    {
        foreach (var item in createdChangeColorButtons.Values)
            Destroy(item.gameObject);

        foreach (var item in createdCountTextForChangeColorButtons.Values)
            Destroy(item.gameObject);

        createdChangeColorButtons.Clear();
        createdCountTextForChangeColorButtons.Clear();
    }

    // Изменение выбраного цвета
    private void ChangePickedColor(int colorId)
    {
        if (pickedColorId != colorId)
        {
            pickedColorId = colorId;
            GameManager.Instance.OnChangeColor(colorId);
        }
    }

    // Создание иконок с информацией о количестве уровней и статусе прохождения
    private void CreateLevelsInfoCont(GameManager.GameData gameData)
    {
        foreach (var item in gameData.levelDatas)
        {
            var go = Instantiate(prefabForLevelInfoItem, contForLevelsInfoCont.transform).GetComponent<Image>();
            go.transform.SetAsLastSibling();
            createdLevelItems.Add(go);
        }
    }

    // Установка дефолтных данных для канваса при переключении уровня
    public void SetCanvasItemsForSubLevel(GameManager.GameData gameData)
    {
        contForTimer.SetActive(false);
        mainContForChangeColorButtons.enabled = true;
        mainContForChangeColorButtons.gameObject.SetActive(true);
        contForLockChangeColorButtonIcon.SetActive(false);

        int subLevelNumber = gameData.subLevelInProgressNumber;
        // Если режим управления перетаскиванием
        if (gameData.levelDatas[subLevelNumber].idForPlayerMovementMode == 0)
        {
            // Если цвета переключать нельзя то иконка lock
            if (gameData.levelDatas[subLevelNumber].canSwitchColors == false)
            {
                // Убираю иконку для более приятного цвета затемнения
                mainContForChangeColorButtons.enabled = false;
                contForLockChangeColorButtonIcon.SetActive(true);
            }
        }
        // Если управление свайпами 
        else
        {
            // То переключения цветов вообще не видно
            mainContForChangeColorButtons.gameObject.SetActive(false);
        }
    }

    public void ShowUI()
    {
        uiCanvas.GetComponent<Animator>().SetTrigger("show");
    }

    public void HideUI()
    {
        uiCanvas.GetComponent<Animator>().SetTrigger("hide");
    }

    public void ShowDimmer()
    {
        dimmer.GetComponent<Animator>().SetTrigger("show");
    }

    public void HideDimmer()
    {
        dimmer.GetComponent<Animator>().SetTrigger("hide");
    }

    #region СОБЫТИЯ
    private void OnClickChangeColorButton(int colorId)
    {
        ChangePickedColor(colorId);
    }

    public void OnClickEndGameButton()
    {
        GameManager.Instance.OnUserEndGame();
    }


    private float ii = 1.5f;
    private float aa = 4f;
    private float bb = 0.5f;
    private float dd = 0.7f;

    public void OnCollectAllOneColorItems(int lastColorId, int nextColorId)
    {
        // Отключение кнопки переключения цвета и перемещение на следующий доступный
        createdChangeColorButtons[lastColorId].gameObject.SetActive(false);
        pickedColorId = nextColorId;
        contForFullCollorEffect.SetTrigger("do");
        CameraShaker.Instance.ShakeOnce(ii, aa, bb, dd);
        //CameraShaker.Instance.ShakeOnce(1f, 1.2f, 0.3f, 1);
    }

    public void OnCollectColorItem(int colorId, int newCount)
    {
        //CameraShaker.Instance.ShakeOnce(0.1f, 4f, 0.3f, 0.3f);

        // Изменение количества оставшихся элементов у кнопки цвета
        createdCountTextForChangeColorButtons[colorId].text = newCount.ToString();
    }

    public void OnCompleteSubLevel(int completedSubLevelNumber)
    {
        // Пометка у иконкки о том что уровень выбран
        createdLevelItems[completedSubLevelNumber].sprite = completedLevelIcon;
    }

    public void OnCollectCoinItem(int coinsCount)
    {
        // todo
        //*Увеличиваю количество монеток исходя из текста
    }

    public void OnTimerStart(GameManager.GameData gameData, int usedColorInArrayNumber)
    {
        int subLevelNumber = gameData.subLevelInProgressNumber;
        // Если глобальный таймер то включа/ его
        if (gameData.levelDatas[subLevelNumber].globalTimer > 0)
        {
            contForTimer.SetActive(true);
            contForTimer.GetComponent<Image>().sprite = spriteForTimerGlobal;
            contForTimer.GetComponent<Image>().color = Color.white;
        }
        else
        {
            // Если первый цвет на уровне это цвет с таймеров
            var color = gameData.levelDatas[subLevelNumber].usedColors[usedColorInArrayNumber];
            contForTimer.SetActive(true);
            contForTimer.GetComponent<Image>().sprite = spriteForTimerColor;
            contForTimer.GetComponent<Image>().color = color.GetColor();
        }
    }

    public void OnTimerDeactivate()
    {
        contForTimer.SetActive(false);
    }

    public void OnTimerChangeValue(int duration)
    {
        animatorForContForTiemer.SetTrigger("pulse");
        int seconds = duration % 60;
        int minutes = duration / 60;
        textForTimer.text = minutes + ":" + seconds.ToString("00");
    }
    #endregion
}
