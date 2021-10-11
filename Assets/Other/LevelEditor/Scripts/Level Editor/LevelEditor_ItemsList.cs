using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    // Класс в для добавления префабов в редактор
    public class LevelEditor_ItemsList : MonoBehaviour
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        // Список префабов
        public List<ItemForLevelEditor> itemsList = new List<ItemForLevelEditor>();

        // Получение словаря префабов с доступом по айди префаба
        public Dictionary<int, ItemForLevelEditor> GetItems()
        {
            Dictionary<int, ItemForLevelEditor> items = new Dictionary<int, ItemForLevelEditor>();
            foreach (var item in itemsList) items.Add(item.itemId, item);
            return items;
        }

        // Установка баззовых данных
        private void Awake()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                var item = new ItemForLevelEditor();
                if (item.itemId == 0) item.itemId = i;
                item.itemGameObjectForPlacingOnMap = gameObjects[i];
                item.name = item.itemGameObjectForPlacingOnMap.name;
                itemsList.Add(item);
            }
        }

        // Класс-структура для описания префаба добавленого в редактор
        [Serializable]
        public class ItemForLevelEditor
        {
            public string name;
            public int itemId;
            public GameObject itemGameObjectForPlacingOnMap;
            public Sprite iconForSelectorCanvas;
        }
    }
}