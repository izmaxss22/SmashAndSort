using System;
using System.Collections.Generic;
using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelDataInputer : MonoBehaviour
{
    public LevelSpecData data;

    public GameObject mainCont;

    public InputField fieldForPlayerSpeed;
    public InputField fieldForPlayerMovementMode;
    public Toggle toggleForCanSwitchColorsState;
    public InputField fieldForGlobalTimer;

    public GameObject contForUsedColors;
    public GameObject prefabForUsedColorItem;
    private List<GameObject> createdUserColorItems = new List<GameObject>();

    public Button buttonSave;

    public void Init(
        DataManager.LevelData levelData,
        List<GameObject> gameObjectsInLevel,
        bool levelIsChanged,
        Action<LevelSpecData> callback)
    {
        buttonSave.onClick.AddListener(() => OnClickButtonSave(callback));
        // todo здесь баг есл и поменял айтемы но количество осталось тем же
        // Если уровень изменился то ввожу данные с нуля за исключением данных которые можно переиспользовать
        if (levelIsChanged || levelData.usedColors == null)
        {
            SetDefaultData(levelData, gameObjectsInLevel);
        }
        else
        {
            SetDataFromSavedLevel(levelData);
        }
        SetDataToInputFields();
    }

    private void OnClickButtonSave(Action<LevelSpecData> callBack)
    {
        GetDataFromInputFields();
        callBack.Invoke(data);
        Destroy(gameObject);
    }

    public void OnClickButtonHide()
    {
        var component = GetComponent<CanvasGroup>();
        if (component.alpha == 0f)
        {
            component.alpha = 1;
        }
        else component.alpha = 0f;
    }

    private void SetDefaultData(DataManager.LevelData levelData, List<GameObject> gameObjects)
    {
        data = new LevelSpecData
        {
            playerSpeed = levelData.playerSpeed,
            movementMode = levelData.idForPlayerMovementMode,
            globalTimer = levelData.globalTimer,
            canSwitchColors = levelData.canSwitchColors
        };

        #region ПОЛУЧЕНИЕ ДАННЫХ ОБ ИСПОЛЬЗУЕМЫХ ЦВЕТАХ

        List<Color> colors = new List<Color>();
        List<int> colorsConts = new List<int>();
        List<int> colorIds = new List<int>();
        List<int> timersLenghtsForColors = new List<int>();

        // Перебор всех элементов для определения используемых цветов
        foreach (var item in gameObjects)
        {
            // Исключаю обьекты не цвета (монетки бомбы и тд)
            if (item.name != "0" && item.name != "1" && item.name != "2")
            {
                // Получаю цвет элемента
                Color itemColor = item.GetComponent<MeshRenderer>().material.color;
                // Если такого цвета нету то добавляеться
                if (!colors.Contains(itemColor)) colors.Add(itemColor);
            }
        }
        // Перебор всех айтемов для определения количества обьектов цвета у каждого цвета
        for (int i = 0; i < colors.Count; i++)
        {
            colorsConts.Add(0);
            foreach (var item in gameObjects)
            {
                // Исключаю обьекты не цвета (монетки бомбы и тд)
                if (item.name != "0" && item.name != "1" && item.name != "2")
                {
                    // Проверяю с каким он цветом совпадает и прибавляю это количество
                    if (colors[i] == item.GetComponent<MeshRenderer>().material.color)
                    {
                        colorsConts[i]++;
                    }
                }
            }
        }
        // Получения айди каждого цвета используемого в уровне
        foreach (var item in colors)
        {
            var colorId = LevelEditorDataManager.colorIdsForEditor.IndexOf(item.ToString());
            colorIds.Add(colorId);
        }

        foreach (var item in colors)
        {
            timersLenghtsForColors.Add(0);
        }

        // Создание обьектов для сохранения
        for (int i = 0; i < colors.Count; i++)
        {
            var colorObjForSave = new DataManager.LevelData.UsedInLevelColor(
                colors[i],
                colorIds[i],
                colorsConts[i],
                timersLenghtsForColors[i]
                );
            Debug.Log(1);
            data.usedColors.Add(colorObjForSave);
        }
        #endregion
    }

    private void SetDataFromSavedLevel(DataManager.LevelData levelData)
    {
        data = new LevelSpecData
        {
            playerSpeed = levelData.playerSpeed,
            movementMode = levelData.idForPlayerMovementMode,
            canSwitchColors = levelData.canSwitchColors,
            globalTimer = levelData.globalTimer,
            usedColors = levelData.usedColors
        };
    }

    private void SetDataToInputFields()
    {
        fieldForPlayerSpeed.text = data.playerSpeed.ToString();
        fieldForPlayerMovementMode.text = data.movementMode.ToString();
        toggleForCanSwitchColorsState.isOn = data.canSwitchColors;
        fieldForGlobalTimer.text = data.globalTimer.ToString();
        foreach (var item in data.usedColors)
        {
            var color = Instantiate(prefabForUsedColorItem, contForUsedColors.transform);
            color.GetComponent<ColorItem>().Init(item.GetColor(), item.colorId, item.colorCounts, item.timerLenght);
            createdUserColorItems.Add(color);
        }
    }

    public void GetDataFromInputFields()
    {
        data.playerSpeed = float.Parse(fieldForPlayerSpeed.text);
        data.movementMode = int.Parse(fieldForPlayerMovementMode.text);
        data.canSwitchColors = toggleForCanSwitchColorsState.isOn;
        data.globalTimer = int.Parse(fieldForGlobalTimer.text);
        foreach (var item in createdUserColorItems)
        {
            var itemNumber = item.transform.GetSiblingIndex();
            var itemData = item.GetComponent<ColorItem>().GetData();
            data.usedColors[itemNumber] = itemData;
        }
    }

    private void ClearData()
    {
        data.usedColors.Clear();
    }

    public class LevelSpecData
    {
        public float playerSpeed;
        public int movementMode;
        public bool canSwitchColors;
        public int globalTimer;

        public List<DataManager.LevelData.UsedInLevelColor> usedColors = new List<DataManager.LevelData.UsedInLevelColor>();
    }
}
