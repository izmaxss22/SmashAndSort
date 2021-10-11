using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace LevelEditor
{
    public class UIForInputLevelDataManager : MonoBehaviour
    {
        //public LevelGrouperManager LevelGrouperManager;

        //#region UI ДАННЫЕ

        //#region UI ЭЛЕМЕНТЫ СВЯЗАННЫЕ С УКАЗАНИЕ КАКИЕ ПОД УРОВНИ СОДЕЖЖИТ УРОВЕНЬ
        //[Header("Sub Level Numbers")]
        //public Text textFor_hierarchyNumberFor_addSubLevelButton;
        //public Text textFor_hierarchyNumberFor_DeleteSubLevelButton;

        //public GameObject contFor_inputFieldsFor_SubLevelNumber;
        //public GameObject prefabFor_inputFiledsFor_SubLevelNumber;
        //private List<InputField> listOf_inputFieldsFor_SubLevelNumbers = new List<InputField>();
        //#endregion

        //// Общее количество очков в уровне
        //[Header("Total points count")]
        //public Text textFor_totalPointsInLevel;

        //#region КОЛИЧЕСТВО ОЧКОВ ДЛЯ ВЫПОЛНЕНИЯ ПОД УРОВНЯ
        //[Header("Sub level points count")]
        //public GameObject contFor_textItemsFor_SubLevelPointsCount;
        //public GameObject prefabFor_textItemFor_SubLevelPointsCount;
        //private List<Text> listOf_createdTextItemsFor_SubLevelPointsCount = new List<Text>();
        //#endregion

        //#region КОЛИЧЕСТВО ОЧКОВ ДЛЯ ПОЛУЧЕНИЯ ЗВЕЗД
        //[Header("Stars points count")]
        //public GameObject contFor_inputFieldFor_pointCountsForStar;
        //public GameObject prefabFor_inputFiledFor_pointCountsForStar;
        //private List<InputField> listOf_createdInputFiledFor_pointCountsForStar = new List<InputField>();
        //#endregion

        //#region ДАННЫЕ СВЯЗАННЫЕ С УКАЗАНИЕМ ДАННЫХ ДЛЯ БОНУСНОГО УРОВНЯ
        //[Header("Bonus level datas")]
        //public Toggle toggleFor_itABonusLevelState;

        //public GameObject contWith_inputFieldsFor_bonusLevelsRewardsIds;
        //public List<InputField> listOf_inputFieldsFor_bonusLevelRewardsIds = new List<InputField>();

        //public GameObject contWith_inputFieldsFor_bonusLevelsRewardsCounts;
        //public List<InputField> listOf_inputFieldsFor_bonusLevelRewardsCounts = new List<InputField>();
        //#endregion
        //#endregion

        //private LevelData levelData;
        //private bool levelCanBeSaved;

        //#region События на которые реагирует этот класс
        //public void OnLevelChanged(LevelData levelData)
        //{
        //    Create_UIItemsLevelData(levelData);
        //}

        //public void OnSwitch_ItBonusToogle()
        //{
        //    Set_BonusLevelRewardsId();
        //    Set_BonusLevelRewardsCounts();
        //}

        //// Добавление под уровня в уровень (добавление с указанием места в последовательности)
        //public void OnClick_AddSubLevel()
        //{
        //    // Номер элемента  в иерархии (если допустим нужно вставить подуровень между 1 и 2 подуровнями)
        //    int hierarchyNumber = int.Parse(textFor_hierarchyNumberFor_addSubLevelButton.text) - 1;
        //    if (hierarchyNumber > -1)
        //    {
        //        levelCanBeSaved = false;

        //        GameObject subLevelNumber = Instantiate(
        //            prefabFor_inputFiledsFor_SubLevelNumber,
        //            Vector3.zero,
        //            Quaternion.identity,
        //            contFor_inputFieldsFor_SubLevelNumber.transform);
        //        subLevelNumber.transform.SetSiblingIndex(hierarchyNumber);


        //        if (listOf_inputFieldsFor_SubLevelNumbers.Count >= hierarchyNumber)
        //            listOf_inputFieldsFor_SubLevelNumbers.Insert(hierarchyNumber, subLevelNumber.GetComponent<InputField>());
        //        else
        //            listOf_inputFieldsFor_SubLevelNumbers.Add(subLevelNumber.GetComponent<InputField>());

        //    }
        //}

        //// Удаление под уровня в уровень (удаление с указанием места в последовательности)
        //public void OnClick_DeleteSubLevel()
        //{
        //    // Номер элемента  в иерархии (если допустим нужно вставить подуровень между 1 и 2 подуровнями)
        //    int hierarchyNumber = int.Parse(textFor_hierarchyNumberFor_DeleteSubLevelButton.text) - 1;

        //    if (hierarchyNumber > -1 && hierarchyNumber < listOf_inputFieldsFor_SubLevelNumbers.Count)
        //    {
        //        levelCanBeSaved = false;

        //        Destroy(listOf_inputFieldsFor_SubLevelNumbers[hierarchyNumber].gameObject);
        //        listOf_inputFieldsFor_SubLevelNumbers.RemoveAt(hierarchyNumber);
        //    }
        //}

        //public void OnClick_UpdateSubLevels()
        //{
        //    // Обнуление данных у уровня
        //    levelData.pointsTotalCount_AvialableInLevel = 0;
        //    levelData.pointsCountsFor_getEachStar = new int[0];
        //    levelData.poinsCountsFor_subLevelsCompleting = new int[0];
        //    levelData.itIsABonusLevel = false;
        //    levelData.bonusLevelRewardsIds = new int[0];
        //    levelData.bonusLevelRewardsCounts = new int[0];

        //    List<int> subLevelNumbers = new List<int>();
        //    // Получение указанных под уровней
        //    foreach (var subLevelNumber in listOf_inputFieldsFor_SubLevelNumbers)
        //    {
        //        int number = int.Parse(subLevelNumber.text);
        //        subLevelNumbers.Add(number);
        //    }
        //    levelData.subLevelsNumber = subLevelNumbers.ToArray();

        //    // Создание окна заного новыми под уровнями и обнулеными данными
        //    Create_UIItemsLevelData(levelData);
        //    levelCanBeSaved = true;
        //}
        //#endregion

        //#region МЕТОДЫ ИНИЦИАЛИЗАЦИИ ОКНА
        //// Установка для ui элементов данных полученного уровня
        //private void Create_UIItemsLevelData(LevelData levelData)
        //{
        //    // Очиста прошлых данных в окне
        //    Clear_LevelData();
        //    this.levelData = levelData;
        //    // Установка новых
        //    Set_SubLevelNumbers();
        //    Set_SubLevelsPointsCount();
        //    Set_TotalPointsInLevelCount();
        //    Set_PointsCountsForGetStars();

        //    Set_ItsABonusLevelSwitcherState();
        //    Set_BonusLevelRewardsId();
        //    Set_BonusLevelRewardsCounts();
        //}

        //private void Set_SubLevelNumbers()
        //{
        //    foreach (var subLevelNumber in levelData.subLevelsNumber)
        //    {
        //        GameObject inputFieldFor_subLevelNumber = Instantiate(
        //            prefabFor_inputFiledsFor_SubLevelNumber,
        //            Vector3.zero,
        //            Quaternion.identity,
        //            contFor_inputFieldsFor_SubLevelNumber.transform);
        //        inputFieldFor_subLevelNumber.GetComponent<InputField>().text = subLevelNumber.ToString();
        //        listOf_inputFieldsFor_SubLevelNumbers.Add(inputFieldFor_subLevelNumber.GetComponent<InputField>());
        //    }

        //}

        //// Установка общего количества очков для этого уровня
        //private void Set_TotalPointsInLevelCount()
        //{
        //    int pointsCount = 0;
        //    // Сумирование суммы очков всех под уровней
        //    foreach (var item in listOf_createdTextItemsFor_SubLevelPointsCount)
        //    {
        //        pointsCount += int.Parse(item.text);
        //    }

        //    textFor_totalPointsInLevel.text = pointsCount.ToString();
        //}

        //// Устанновка общего количества очков для каждого под уровня
        //private void Set_SubLevelsPointsCount()
        //{
        //    // Перебор каждого под уровня
        //    foreach (var subLevelNumber in levelData.subLevelsNumber)
        //    {
        //        var subLevelData = LevelGrouperManager.LoadSubLevel(subLevelNumber);

        //        int pointInSubLevelCount = 0;
        //        // Перебор всех элементов созданых в этом под уровне
        //        // Идет подсчет количества очков в под уровне
        //        foreach (var itemPlacedOnMap in subLevelData.items)
        //        {
        //            // За каждый большой элемент содержащий очки добавляеться 4 очка
        //            if (itemPlacedOnMap.itemId == 0) pointInSubLevelCount += 4;
        //            // За маленький += 1 
        //            if (itemPlacedOnMap.itemId == 1) pointInSubLevelCount += 1;
        //        }

        //        // Создание обьекта с количеством очков для этого под уровня
        //        Text textItemGameObject = Instantiate(
        //            prefabFor_textItemFor_SubLevelPointsCount,
        //            Vector3.zero,
        //            Quaternion.identity,
        //            contFor_textItemsFor_SubLevelPointsCount.transform).GetComponent<Text>();
        //        textItemGameObject.text = pointInSubLevelCount.ToString();
        //        listOf_createdTextItemsFor_SubLevelPointsCount.Add(textItemGameObject);
        //    }
        //}

        //// Установка количества очков для получения каждой звезды
        //private void Set_PointsCountsForGetStars()
        //{
        //    // Если в уровне уже указанны эти данные 
        //    if (levelData.pointsCountsFor_getEachStar.Length > 0)
        //        foreach (var pointCount in levelData.pointsCountsFor_getEachStar)
        //            Create_InputFieldFor_PointsCountForStar(pointCount);

        //    // Иначе создание с дефолтными значениями
        //    else
        //        for (int i = 0; i < 3; i++)
        //        {
        //            // Последний элемент всегде равен общему количеству очков
        //            // потом-что последняя звезда выдаеться тогда когда получил все очки
        //            if (i == 2)
        //                Create_InputFieldFor_PointsCountForStar(int.Parse(textFor_totalPointsInLevel.text));
        //            else
        //                Create_InputFieldFor_PointsCountForStar(0);
        //        }
        //}

        //// Создание поля для ввода количества очков
        //private void Create_InputFieldFor_PointsCountForStar(int pointCount)
        //{
        //    InputField inputField = Instantiate(
        //        prefabFor_inputFiledFor_pointCountsForStar,
        //        Vector3.zero,
        //        Quaternion.identity,
        //        contFor_inputFieldFor_pointCountsForStar.transform).GetComponent<InputField>();
        //    inputField.text = pointCount.ToString();

        //    listOf_createdInputFiledFor_pointCountsForStar.Add(inputField);
        //}

        //// Установка переключателя с обозначением бонусный ли это уровень
        //private void Set_ItsABonusLevelSwitcherState()
        //{
        //    toggleFor_itABonusLevelState.isOn = levelData.itIsABonusLevel;
        //}

        //// Установка данных для награды за каждую звезду бонусного уровня
        //private void Set_BonusLevelRewardsId()
        //{
        //    // Установка данных происходит только если этот уровень бонусный
        //    if (toggleFor_itABonusLevelState.isOn)
        //    {
        //        contWith_inputFieldsFor_bonusLevelsRewardsIds.SetActive(true);
        //        // Если уровень уже содержит данные с описанием айди наград
        //        if (levelData.bonusLevelRewardsIds.Length > 0)
        //        {
        //            for (int i = 0; i < 3; i++)
        //            {
        //                string bonusLevelRewardId = levelData.bonusLevelRewardsIds[i].ToString();
        //                listOf_inputFieldsFor_bonusLevelRewardsIds[i].text = bonusLevelRewardId;
        //            }
        //        }
        //        // Иначе дефолтные данные
        //        else
        //            for (int i = 0; i < 3; i++)
        //                listOf_inputFieldsFor_bonusLevelRewardsIds[i].text = "0";

        //    }
        //    // Если это не бонусный уровень то эти поля ввода скрыты
        //    else contWith_inputFieldsFor_bonusLevelsRewardsIds.SetActive(false);
        //}

        //// Установка данных с количеством награды за каждую звезду бонусного уровня
        //private void Set_BonusLevelRewardsCounts()
        //{
        //    // Установка данных происходит только если этот уровень бонусный
        //    if (toggleFor_itABonusLevelState.isOn)
        //    {
        //        contWith_inputFieldsFor_bonusLevelsRewardsCounts.SetActive(true);
        //        // Если уровень уже содержит данные с описанием количества наград
        //        if (levelData.bonusLevelRewardsCounts.Length > 0)
        //        {
        //            for (int i = 0; i < 3; i++)
        //            {
        //                string bonusLevelRewardCount = levelData.bonusLevelRewardsCounts[i].ToString();
        //                listOf_inputFieldsFor_bonusLevelRewardsCounts[i].text = bonusLevelRewardCount;
        //            }
        //        }
        //        // Иначе дефолтные данные
        //        else
        //            for (int i = 0; i < 3; i++)
        //                listOf_inputFieldsFor_bonusLevelRewardsCounts[i].text = "0";

        //    }
        //    // Если это не бонусный уровень то эти поля ввода скрыты
        //    else contWith_inputFieldsFor_bonusLevelsRewardsCounts.SetActive(false);

        //}

        //private void Clear_LevelData()
        //{
        //    foreach (var inputField in listOf_inputFieldsFor_SubLevelNumbers) Destroy(inputField.gameObject);
        //    listOf_inputFieldsFor_SubLevelNumbers.Clear();

        //    textFor_totalPointsInLevel.text = "0";

        //    foreach (var textItem in listOf_createdTextItemsFor_SubLevelPointsCount) Destroy(textItem.gameObject);
        //    listOf_createdTextItemsFor_SubLevelPointsCount.Clear();

        //    foreach (var inputFieldItem in listOf_createdInputFiledFor_pointCountsForStar) Destroy(inputFieldItem.gameObject);
        //    listOf_createdInputFiledFor_pointCountsForStar.Clear();

        //    toggleFor_itABonusLevelState.isOn = false;
        //}
        //#endregion

        //#region СОЗДАНИЕ ДАННЫХ ОБ УРОВНЕ ИЗ ВВЕДЕННЫХ ДАННЫХ
        //public LevelData Get_LeveData()
        //{
        //    if (levelCanBeSaved)
        //    {
        //        levelData.pointsTotalCount_AvialableInLevel = int.Parse(textFor_totalPointsInLevel.text);
        //        SetToLevelData_SubLevelForCompletingPointsCount();
        //        SetToLevelData_PointCountForGettingStars();
        //        levelData.itIsABonusLevel = toggleFor_itABonusLevelState.isOn;
        //        SetToLevelData_RewardsIds();
        //        SetToLevelData_RewardsCounts();
        //        return levelData;
        //    }
        //    else
        //    {
        //        Debug.LogError("Перед получение данных, нужно обновить данные под уровней");
        //        return null;
        //    }
        //    // Обаботать когда созданны но не сохраннены под уровни
        //}

        //private void SetToLevelData_SubLevelForCompletingPointsCount()
        //{
        //    int subLevelsCount = listOf_inputFieldsFor_SubLevelNumbers.Count;
        //    levelData.poinsCountsFor_subLevelsCompleting = new int[subLevelsCount];
        //    // Установка данных в массив исходя из полей с указанием количества этих очков
        //    for (int i = 0; i < subLevelsCount; i++)
        //    {
        //        int subLevelPointsCount = int.Parse(listOf_createdTextItemsFor_SubLevelPointsCount[i].text);
        //        levelData.poinsCountsFor_subLevelsCompleting[i] = subLevelPointsCount;
        //    }
        //}

        //private void SetToLevelData_PointCountForGettingStars()
        //{
        //    levelData.pointsCountsFor_getEachStar = new int[3];
        //    for (int i = 0; i < 3; i++)
        //    {
        //        int pointsForStarCount = int.Parse(listOf_createdInputFiledFor_pointCountsForStar[i].text);
        //        levelData.pointsCountsFor_getEachStar[i] = pointsForStarCount;
        //    }

        //}

        //private void SetToLevelData_RewardsIds()
        //{
        //    levelData.bonusLevelRewardsIds = new int[3];
        //    for (int i = 0; i < 3; i++)
        //    {
        //        int rewardId = int.Parse(listOf_inputFieldsFor_bonusLevelRewardsIds[i].text);
        //        levelData.bonusLevelRewardsIds[i] = rewardId;
        //    }

        //}

        //private void SetToLevelData_RewardsCounts()
        //{
        //    levelData.bonusLevelRewardsCounts = new int[3];
        //    for (int i = 0; i < 3; i++)
        //    {
        //        int rewardCount = int.Parse(listOf_inputFieldsFor_bonusLevelRewardsCounts[i].text);
        //        levelData.bonusLevelRewardsCounts[i] = rewardCount;
        //    }

        //}
        //#endregion
    }
}