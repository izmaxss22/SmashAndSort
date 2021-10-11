﻿using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class MediatorFor_ItemsMode : MonoBehaviour
    {
        public ItemsCreator ItemsCreator;
        public ItemsPlacer ItemsPlacer;
        public ItemsSelectorCanvas ItemsSelectorCanvas;

        #region ВНЕШНИЕ СОБЫТИЯ
        public void InitMode(CreatedLevelData levelData, Dictionary<int, LevelEditor_ItemsList.ItemForLevelEditor> itemsForLevelEditor)
        {
            ItemsCreator.InitMode(levelData, itemsForLevelEditor);
            ItemsSelectorCanvas.InitMode(itemsForLevelEditor);
            ItemsPlacer.InitMode(itemsForLevelEditor);
            var items = ItemsCreator.GetCreatedItems();
            // Если у айтема есть спец данные то они устанавливаються
            // todo это походу вообще убрать так как спец данные присваиваються сразу классом с данными внутри дата менежджера
            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (items[i].TryGetComponent(out ItemWithSpecData specData))
            //        specData.specData = levelData.items[i].itemSpecData;
            //}
        }

        public void OnModeEnable()
        {
            ItemsCreator.OnModeEnable();
            ItemsSelectorCanvas.OnModeEnable();
            ItemsPlacer.OnModeEnable();
        }

        public void OnModeDisable()
        {
            ItemsCreator.OnModeDisable();
            ItemsSelectorCanvas.OnModeDisable();
            ItemsPlacer.OnModeDisable();
        }
        #endregion

        #region ВНУТРЕНИЕ СОБЫТИЯ
        public void OnСlick_PickItemButton(int itemNumber)
        {
            ItemsPlacer.OnСlick_PickItemButton(itemNumber);
        }

        public void OnClick_PutItem_OnMap(int id, Vector3 pos, Quaternion rot)
        {
            ItemsCreator.OnClick_PutItem(id, pos, rot);
        }

        public void OnClick_DeleteItem_OnMap(GameObject GOForDelete)
        {
            ItemsCreator.OnClick_DeleteItem(GOForDelete);
        }

        public void OnClick_ReplaceItem_OnMap(GameObject GOForDelete, int newItemid, Vector3 newItemPos, Quaternion newItemRot)
        {
            ItemsCreator.OnClick_ReplaceItem(GOForDelete, newItemid, newItemPos, newItemRot);
        }
        #endregion

        #region МЕТОДЫ ДЛЯ ВНЕШНИХ КЛАССОВ
        public List<GameObject> GetCreatedItems()
        {
            return ItemsCreator.GetCreatedItems();
        }
        #endregion
    }
}