using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace LevelEditor
{
    public class LevelGrouperManager : MonoBehaviour
    {
        //public DataManager dataManager;
        //public UIForInputLevelDataManager UIForInputLevelDataManager;

        //private int levelNumber = -1;
        //public InputField inputFieldFor_LevelNumber;

        //private void Start()
        //{
        //    LoadLevel(0);
        //}

        //#region СОБЫТИЯ НА КОТОРЫЕ РЕАГИРУЕТ ЭТОТ КЛАСС
        //public void OnClik_LoadSpecifiedLeve()
        //{
        //    LoadLevel(int.Parse(inputFieldFor_LevelNumber.text));
        //}

        //public void OnClik_LoadNextLevel()
        //{
        //    LoadLevel(levelNumber + 1);
        //}

        //public void OnClik_LoadPreviousLevel()
        //{
        //    LoadLevel(levelNumber - 1);
        //}

        //public void OnClik_SaveLevelData()
        //{
        //    SaveLevel();
        //}
        //#endregion

        //#region ВНУТРЕНИЕ МЕТОДЫ
        //private void LoadLevel(int levelForLoadNumber)
        //{
        //    // Если нужно загрузить уровень отличающийся от того что сейчас и он не меньше 0
        //    if (levelForLoadNumber != levelNumber && levelForLoadNumber >= 0)
        //    {
        //        levelNumber = levelForLoadNumber;
        //        LevelData levelData = DataManager.Load_LevelData(levelNumber);
        //        UIForInputLevelDataManager.OnLevelChanged(levelData);
        //        inputFieldFor_LevelNumber.text = levelNumber.ToString();
        //        Delete_AllItems();
        //        Create_AllItems(LoadSubLevel(levelData.subLevelsNumber[0]));
        //    }
        //}


        //// Удаление всех обьектов с карты
        //private void Delete_AllItems()
        //{
        //    foreach (var item in createdItems) Destroy(item);
        //    createdItems.Clear();
        //}

        //// Создание всех обьектов из списка обьектов
        //private void Create_AllItems(LevelDatas levelData)
        //{
        //    Camera.main.orthographicSize = levelData.cameraScale;
        //    Camera.main.transform.position = levelData.Get_CameraPosition();
        //    foreach (var item in levelData.items)
        //    {
        //        int itemId = item.itemId;
        //        Vector3 pos = item.GetItemPossiton();
        //        Quaternion rot = item.GetItemRotation();
        //        Create_Item(itemId, pos, rot);
        //    }
        //}

        //public LevelEditor_ItemsList itemsForLevelEditor;

        //private Dictionary<int, ItemForLevelEditor> itemsAddedInEditor;

        //private List<GameObject> createdItems = new List<GameObject>();
        //public GameObject itemsCont;

        //// Создание указаного элемента на карте
        //private void Create_Item(int itemId, Vector3 pos, Quaternion rot)
        //{
        //    itemsAddedInEditor = itemsForLevelEditor.GetItems();
        //    GameObject GO = Instantiate(
        //        itemsAddedInEditor[itemId].itemGameObjectForPlacingOnMap,
        //        pos,
        //        rot,
        //        itemsCont.transform);
        //    GO.name = itemId.ToString();
        //    GO.AddComponent<BoxCollider>();
        //    createdItems.Add(GO);
        //}

        //private void SaveLevel()
        //{
        //    LevelData levelData = UIForInputLevelDataManager.Get_LeveData();
        //    if (levelData != null) DataManager.Save_LevelData(levelData, levelNumber);
        //}
        //#endregion

        //#region ВНЕШНИЕ МЕТОДЫ
        //public LevelDatas LoadSubLevel(int subLevelNumber)
        //{
        //    return DataManager.Load_SubLevelData(subLevelNumber);
        //}
        //#endregion

    }
}