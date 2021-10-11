﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LevelEditor
{
    public class MediatorFor_ColiderMode : MonoBehaviour
    {
        public ColiderPointsCreator ColiderPointsCreator;
        public ColiderPickButtonsManager ColiderPickButtonsManager;
        public СoliderPointsPainter СoliderPointsPainter;
        public ColiderPointsPlacer ColiderPointsPlacer;

        public GameObject canvasFor_ColliderMode;

        #region ВНЕШНИЕ СОБЫТИЯ НА КОТОРЫЕ РЕАГИРУЕТ ЭТОТ КЛАСС
        public void InitMode(CreatedLevelData levelData)
        {
            ColiderPickButtonsManager.On_LevelChanged(levelData.colliders.Count);
            ColiderPointsCreator.OnLevelChanged(levelData.colliders);
        }

        // При включении режима редактора колайдеров
        public void OnModeEnable()
        {
            canvasFor_ColliderMode.SetActive(true);
            ColiderPointsCreator.OnModeStateChanged(true);
            ColiderPointsPlacer.OnModeStateChanged(true);
        }

        // При выключении режима редактора колайдеров
        public void OnModeDisable()
        {
            canvasFor_ColliderMode.SetActive(false);
            ColiderPointsCreator.OnModeStateChanged(false);
            ColiderPointsPlacer.OnModeStateChanged(false);
        }
        #endregion

        #region ВНУТРЕНИЕ СОБЫТИЯ КОТОРЫМУ УПРАЯВЛЯЕТ ЭТОТ КЛАСС
        // При нажатии на карте для размещения припятсвия
        public void OnItemPlacedOnMap(Vector3 itemPos)
        {
            GameObject createdItemGO = ColiderPointsCreator.OnItemPlacedOnMap(itemPos, ColiderPickButtonsManager.Get_PickedColiderNumber());
            if (createdItemGO != null) СoliderPointsPainter.OnItemPlacedOnMap(createdItemGO);
        }
        // При нажатия на карте для удаления припятсвия
        public void OnItemDeletedFromMap(GameObject itemForDelete)
        {
            ColiderPointsCreator.OnItemDeletedFromMap(itemForDelete, ColiderPickButtonsManager.Get_PickedColiderNumber());
        }

        public void OnClick_PickColliderButton(int buttonNumber)
        {
            int pickedColiderNumber = ColiderPickButtonsManager.Get_PickedColiderNumber();
            // Если номер колайдера для выбора отличаеться от того который выбран
            if (pickedColiderNumber != buttonNumber)
            {
                var colliderDatas = ColiderPointsCreator.Get_ColliderDatas();
                СoliderPointsPainter.OnClick_PickColliderButton(pickedColiderNumber, buttonNumber, colliderDatas);
                ColiderPickButtonsManager.OnClick_PickColliderButton(buttonNumber);
                ColiderPointsCreator.OnClick_PickColiderButton(buttonNumber);
            }
        }

        public void OnClick_AddColliderButton()
        {
            // Добавить в массив пустой словарь
            // Добавить кнопку
            ColiderPointsCreator.OnClick_AddColliderButton();
            ColiderPickButtonsManager.OnClick_AddColliderButton();
        }

        // Удаляет последний созданный колайдер
        public void OnClick_DeleteColiderButton()
        {
            var colliderDatas = ColiderPointsCreator.Get_ColliderDatas();
            // Если есть созданные колайдеры
            if (colliderDatas.Count > 0)
            {
                int lastColiderNumber = colliderDatas.Count - 1;
                // Если удаляемый колайдер это не выбранный колайдер
                if (lastColiderNumber != ColiderPickButtonsManager.Get_PickedColiderNumber())
                {
                    ColiderPickButtonsManager.OnClick_DeleteColiderButton(lastColiderNumber);
                    ColiderPointsCreator.OnClick_DeleteColiderButton(lastColiderNumber);
                }
            }
        }
        #endregion

        public List<CreatedColiderDatas> Get_CreatedColidersList()
        {
            List<CreatedColiderDatas> createdColiderDatas = new List<CreatedColiderDatas>();
            // Перебор списка содержащего словари (один словарь = все геймобджекты точек для одного колайдера)
            foreach (var coliderData in ColiderPointsCreator.Get_ColliderDatas())
            {
                List<Vector3> colidePoints = new List<Vector3>();

                // Перебор полученого кроваря для получение из него всез записанных точек
                foreach (var coliderPoint in coliderData) colidePoints.Add(coliderPoint.transform.position);

                createdColiderDatas.Add(new CreatedColiderDatas(colidePoints.ToArray()));
            }
            return createdColiderDatas;
        }

        public int Get_PickedColiderNumber()
        {
            return ColiderPickButtonsManager.Get_PickedColiderNumber();
        }
    }
}