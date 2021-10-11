using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Mathematics;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorDataManager : MonoBehaviour
    {
        private static string savingPath = "";

        #region КОД ДЛЯ ГЕНЕРАЦИИ
        //string mat = "";
        //const string quote = "\"";

        //    for (int i = 0; i<materials.Count; i++)
        //    {
        //        mat += "[" + quote + materials[i].color.ToString() + quote + "]" + "=" + i.ToString() + ",";
        //    }
        #endregion
        // Обращаюсь по айди = Color.ToString() и получаю айди для этого цвета
        public static List<string> colorIdsForEditor = new List<string>
        {
            "notColorItem",
            "notColorItem",
            "notColorItem",
            "RGBA(0.000, 0.565, 1.000, 1.000)", "RGBA(0.200, 0.651, 1.000, 1.000)","RGBA(0.400, 0.737, 0.996, 1.000)","RGBA(0.596, 0.831, 0.996, 1.000)","RGBA(0.800, 0.914, 1.000, 1.000)","RGBA(0.196, 0.541, 0.804, 1.000)","RGBA(0.396, 0.627, 0.804, 1.000)","RGBA(0.600, 0.714, 0.800, 1.000)","RGBA(0.000, 0.337, 0.600, 1.000)","RGBA(0.200, 0.431, 0.604, 1.000)","RGBA(0.404, 0.514, 0.600, 1.000)","RGBA(0.004, 0.227, 0.404, 1.000)","RGBA(0.204, 0.314, 0.400, 1.000)","RGBA(0.016, 0.196, 1.000, 1.000)","RGBA(0.204, 0.200, 1.000, 1.000)","RGBA(0.408, 0.400, 1.000, 1.000)","RGBA(0.604, 0.600, 1.000, 1.000)","RGBA(0.800, 0.800, 0.996, 1.000)","RGBA(0.200, 0.196, 0.796, 1.000)","RGBA(0.404, 0.400, 0.800, 1.000)","RGBA(0.600, 0.600, 0.800, 1.000)","RGBA(0.008, 0.102, 0.600, 1.000)","RGBA(0.204, 0.200, 0.596, 1.000)","RGBA(0.400, 0.400, 0.604, 1.000)","RGBA(0.008, 0.055, 0.400, 1.000)","RGBA(0.200, 0.200, 0.404, 1.000)","RGBA(0.376, 0.204, 0.996, 1.000)","RGBA(0.506, 0.212, 1.000, 1.000)","RGBA(0.624, 0.400, 1.000, 1.000)","RGBA(0.749, 0.600, 0.996, 1.000)","RGBA(0.875, 0.804, 0.996, 1.000)","RGBA(0.420, 0.204, 0.800, 1.000)","RGBA(0.549, 0.404, 0.796, 1.000)","RGBA(0.675, 0.604, 0.804, 1.000)","RGBA(0.224, 0.106, 0.604, 1.000)","RGBA(0.349, 0.200, 0.596, 1.000)","RGBA(0.475, 0.404, 0.600, 1.000)","RGBA(0.149, 0.059, 0.396, 1.000)","RGBA(0.267, 0.200, 0.396, 1.000)","RGBA(0.667, 0.220, 0.992, 1.000)","RGBA(0.733, 0.227, 1.000, 1.000)","RGBA(0.808, 0.404, 1.000, 1.000)","RGBA(0.867, 0.600, 0.996, 1.000)","RGBA(0.929, 0.800, 0.996, 1.000)","RGBA(0.604, 0.200, 0.800, 1.000)","RGBA(0.663, 0.400, 0.796, 1.000)","RGBA(0.733, 0.596, 0.800, 1.000)","RGBA(0.404, 0.118, 0.600, 1.000)","RGBA(0.471, 0.200, 0.596, 1.000)","RGBA(0.533, 0.400, 0.600, 1.000)","RGBA(0.271, 0.063, 0.396, 1.000)","RGBA(0.396, 0.200, 0.400, 1.000)","RGBA(0.992, 0.251, 0.996, 1.000)","RGBA(1.000, 0.251, 1.000, 1.000)","RGBA(1.000, 0.400, 1.000, 1.000)","RGBA(1.000, 0.600, 0.996, 1.000)","RGBA(0.996, 0.800, 1.000, 1.000)","RGBA(0.800, 0.196, 0.800, 1.000)","RGBA(0.804, 0.396, 0.800, 1.000)","RGBA(0.796, 0.600, 0.796, 1.000)","RGBA(0.600, 0.133, 0.600, 1.000)","RGBA(0.600, 0.200, 0.596, 1.000)","RGBA(0.596, 0.396, 0.600, 1.000)","RGBA(0.400, 0.078, 0.400, 1.000)","RGBA(0.400, 0.200, 0.329, 1.000)","RGBA(0.988, 0.192, 0.631, 1.000)","RGBA(1.000, 0.204, 0.706, 1.000)","RGBA(1.000, 0.400, 0.773, 1.000)","RGBA(1.000, 0.600, 0.855, 1.000)","RGBA(1.000, 0.800, 0.929, 1.000)","RGBA(0.804, 0.200, 0.576, 1.000)","RGBA(0.800, 0.400, 0.655, 1.000)","RGBA(0.804, 0.604, 0.667, 1.000)","RGBA(0.604, 0.098, 0.376, 1.000)","RGBA(0.600, 0.200, 0.455, 1.000)","RGBA(0.600, 0.400, 0.529, 1.000)","RGBA(0.400, 0.051, 0.251, 1.000)","RGBA(0.400, 0.204, 0.271, 1.000)","RGBA(1.000, 0.161, 0.337, 1.000)","RGBA(1.000, 0.196, 0.471, 1.000)","RGBA(1.000, 0.404, 0.608, 1.000)","RGBA(1.000, 0.604, 0.737, 1.000)","RGBA(1.000, 0.800, 0.863, 1.000)","RGBA(0.796, 0.196, 0.400, 1.000)","RGBA(0.796, 0.400, 0.533, 1.000)","RGBA(0.800, 0.600, 0.596, 1.000)","RGBA(0.600, 0.078, 0.208, 1.000)","RGBA(0.596, 0.200, 0.337, 1.000)","RGBA(0.600, 0.400, 0.467, 1.000)","RGBA(0.396, 0.039, 0.133, 1.000)","RGBA(0.400, 0.204, 0.192, 1.000)","RGBA(0.992, 0.145, 0.000, 1.000)","RGBA(1.000, 0.200, 0.208, 1.000)","RGBA(1.000, 0.404, 0.396, 1.000)","RGBA(1.000, 0.600, 0.604, 1.000)","RGBA(1.000, 0.800, 0.792, 1.000)","RGBA(0.800, 0.204, 0.200, 1.000)","RGBA(0.800, 0.396, 0.400, 1.000)","RGBA(0.800, 0.667, 0.600, 1.000)","RGBA(0.600, 0.071, 0.004, 1.000)","RGBA(0.604, 0.196, 0.208, 1.000)","RGBA(0.600, 0.400, 0.400, 1.000)","RGBA(0.400, 0.031, 0.000, 1.000)","RGBA(0.392, 0.271, 0.196, 1.000)","RGBA(0.996, 0.373, 0.000, 1.000)","RGBA(0.996, 0.494, 0.196, 1.000)","RGBA(0.996, 0.612, 0.400, 1.000)","RGBA(1.000, 0.745, 0.600, 1.000)","RGBA(1.000, 0.871, 0.792, 1.000)","RGBA(0.796, 0.420, 0.204, 1.000)","RGBA(0.796, 0.545, 0.388, 1.000)","RGBA(0.796, 0.733, 0.600, 1.000)","RGBA(0.600, 0.212, 0.000, 1.000)","RGBA(0.600, 0.345, 0.196, 1.000)","RGBA(0.600, 0.471, 0.400, 1.000)","RGBA(0.396, 0.141, 0.004, 1.000)","RGBA(0.392, 0.333, 0.200, 1.000)","RGBA(0.996, 0.667, 0.008, 1.000)","RGBA(1.000, 0.733, 0.196, 1.000)","RGBA(1.000, 0.800, 0.400, 1.000)","RGBA(1.000, 0.871, 0.600, 1.000)","RGBA(0.996, 0.933, 0.800, 1.000)","RGBA(0.800, 0.596, 0.192, 1.000)","RGBA(0.796, 0.667, 0.396, 1.000)","RGBA(0.800, 0.800, 0.592, 1.000)","RGBA(0.600, 0.400, 0.004, 1.000)","RGBA(0.604, 0.471, 0.200, 1.000)","RGBA(0.588, 0.529, 0.400, 1.000)","RGBA(0.392, 0.263, 0.004, 1.000)","RGBA(0.392, 0.333, 0.200, 1.000)","RGBA(1.000, 0.984, 0.000, 1.000)","RGBA(0.988, 0.984, 0.192, 1.000)","RGBA(1.000, 0.988, 0.404, 1.000)","RGBA(0.996, 0.992, 0.596, 1.000)","RGBA(1.000, 0.996, 0.792, 1.000)","RGBA(0.792, 0.800, 0.200, 1.000)","RGBA(0.792, 0.800, 0.392, 1.000)","RGBA(0.800, 0.800, 0.600, 1.000)","RGBA(0.600, 0.604, 0.008, 1.000)","RGBA(0.596, 0.600, 0.196, 1.000)","RGBA(0.600, 0.596, 0.396, 1.000)","RGBA(0.396, 0.400, 0.000, 1.000)","RGBA(0.400, 0.400, 0.196, 1.000)","RGBA(0.769, 0.980, 0.012, 1.000)","RGBA(0.808, 0.984, 0.196, 1.000)","RGBA(0.859, 0.984, 0.404, 1.000)","RGBA(0.902, 0.988, 0.600, 1.000)","RGBA(0.945, 0.992, 0.792, 1.000)","RGBA(0.659, 0.800, 0.204, 1.000)","RGBA(0.702, 0.800, 0.396, 1.000)","RGBA(0.745, 0.800, 0.588, 1.000)","RGBA(0.455, 0.600, 0.004, 1.000)","RGBA(0.502, 0.596, 0.196, 1.000)","RGBA(0.549, 0.604, 0.396, 1.000)","RGBA(0.306, 0.400, 0.008, 1.000)","RGBA(0.349, 0.404, 0.192, 1.000)","RGBA(0.027, 0.976, 0.000, 1.000)","RGBA(0.196, 0.976, 0.196, 1.000)","RGBA(0.400, 0.980, 0.396, 1.000)","RGBA(0.600, 0.984, 0.600, 1.000)","RGBA(0.800, 0.992, 0.792, 1.000)","RGBA(0.204, 0.800, 0.200, 1.000)","RGBA(0.396, 0.804, 0.404, 1.000)","RGBA(0.600, 0.800, 0.596, 1.000)","RGBA(0.000, 0.600, 0.000, 1.000)","RGBA(0.204, 0.600, 0.200, 1.000)","RGBA(0.404, 0.604, 0.400, 1.000)","RGBA(0.000, 0.396, 0.004, 1.000)","RGBA(0.200, 0.400, 0.196, 1.000)","RGBA(0.016, 0.984, 0.663, 1.000)","RGBA(0.204, 0.984, 0.729, 1.000)","RGBA(0.404, 0.988, 0.796, 1.000)","RGBA(0.600, 0.992, 0.863, 1.000)","RGBA(0.800, 0.996, 0.933, 1.000)","RGBA(0.200, 0.800, 0.596, 1.000)","RGBA(0.404, 0.800, 0.667, 1.000)","RGBA(0.600, 0.796, 0.733, 1.000)","RGBA(0.004, 0.600, 0.400, 1.000)","RGBA(0.200, 0.600, 0.463, 1.000)","RGBA(0.396, 0.600, 0.533, 1.000)","RGBA(0.008, 0.396, 0.271, 1.000)","RGBA(0.200, 0.400, 0.333, 1.000)","RGBA(0.008, 0.992, 1.000, 1.000)","RGBA(0.208, 0.992, 0.996, 1.000)","RGBA(0.396, 0.992, 0.988, 1.000)","RGBA(0.596, 0.996, 0.996, 1.000)","RGBA(0.800, 0.996, 0.996, 1.000)","RGBA(0.200, 0.796, 0.800, 1.000)","RGBA(0.200, 0.796, 0.800, 1.000)","RGBA(0.600, 0.796, 0.800, 1.000)","RGBA(0.000, 0.596, 0.600, 1.000)","RGBA(0.196, 0.600, 0.596, 1.000)","RGBA(0.396, 0.600, 0.604, 1.000)","RGBA(0.000, 0.404, 0.400, 1.000)","RGBA(0.200, 0.400, 0.404, 1.000)","RGBA(0.000, 0.000, 0.000, 1.000)","RGBA(0.100, 0.100, 0.100, 1.000)","RGBA(0.200, 0.200, 0.200, 1.000)","RGBA(0.300, 0.300, 0.300, 1.000)","RGBA(0.500, 0.500, 0.500, 1.000)","RGBA(0.700, 0.700, 0.700, 1.000)"
        };

        private void Awake()
        {
            savingPath = Application.streamingAssetsPath + "/";
        }

        #region СОХРАНЕНИЯ/ ПОЛУЧЕНИЕ ДАННЫХ УРОВНЯ
        // Загрузка данных об уровне из файла
        public static CreatedLevelData LoadLevelData(int levelNumber)
        {
            string filePath = savingPath + levelNumber + ".data";
            Debug.Log(filePath);
            // Если есть файл с данными уровня
            if (File.Exists(filePath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);

                DataManager.LevelData levelData = (DataManager.LevelData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();

                return Parse_SavedLevelData_ToEditorLevelData(levelData, levelNumber);
            }
            // Иначе пустой обьект
            else
            {
                return new CreatedLevelData(
                    levelNumber,
                    new Vector3(0, 0, 0),
                    new Vector3(20, 20, -40),
                    25,
                    new List<GameObject>(),
                    new List<CreatedColiderDatas>()
                   );
            }
        }

        // Сохранение данных об уровне в файл
        public static void SaveLevelData(
            CreatedLevelData createdLevelData,
            int subLevelNumber,
            LevelDataInputer.LevelSpecData levelSpecData)
        {
            if (subLevelNumber >= 0)
            {
                string filePath = savingPath + subLevelNumber + ".data";
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Create);

                var dataForSaving = Parse_EditorLevelData_ToLevelDataForSaving(createdLevelData, levelSpecData);
                binaryFormatter.Serialize(fileStream, dataForSaving);
                fileStream.Close();
            }
        }
        #endregion

        #region ПАРСИНГ ДАННЫХ РЕДАКТОРА В ДАННЫЕ УРОВНЯ
        // Метод в котором я определяю какие данные сохранять из обьекта созданого в редакторе
        // todo Этот метод нужно подбивать исходя из данных для конкретной игры
        private static DataManager.LevelData Parse_EditorLevelData_ToLevelDataForSaving(
            CreatedLevelData createdLevelData,
            LevelDataInputer.LevelSpecData levelSpecData)
        {
            #region ПОЛУЧЕНИЕ ДАННЫХ ОБ ИСПОЛЬЗУЕМЫХ ЦВЕТАХ
            //var usedColors = new List<DataManager.LevelData.UsedInLevelColor>();
            //List<Color> colors = new List<Color>();
            //List<int> colorsConts = new List<int>();
            //List<int> colorIds = new List<int>();
            //// Перебор всех элементов для определения используемых цветов
            //foreach (var item in createdLevelData.items)
            //{
            //    // Исключаю обьекты не цвета (монетки бомбы и тд)
            //    if (item.name != "0" && item.name != "1" && item.name != "2")
            //    {
            //        // Получаю цвет элемента
            //        Color itemColor = item.GetComponent<MeshRenderer>().material.color;
            //        // Если такого цвета нету то добавляеться
            //        if (!colors.Contains(itemColor)) colors.Add(itemColor);
            //    }
            //}
            //// Перебор всех айтемов для определения количества обьектов цвета у каждого цвета
            //for (int i = 0; i < colors.Count; i++)
            //{
            //    colorsConts.Add(0);
            //    foreach (var item in createdLevelData.items)
            //    {
            //        // Исключаю обьекты не цвета (монетки бомбы и тд)
            //        if (item.name != "0" && item.name != "1" && item.name != "2")
            //        {
            //            // Проверяю с каким он цветом совпадает и прибавляю это количество
            //            if (colors[i] == item.GetComponent<MeshRenderer>().material.color)
            //            {
            //                colorsConts[i]++;
            //            }
            //        }
            //    }
            //}
            //// Получения айди каждого цвета используемого в уровне
            //foreach (var item in colors)
            //{
            //    var colorId = colorIdsForEditor.IndexOf(item.ToString());
            //    Debug.Log(colorId);
            //    colorIds.Add(colorId);
            //}

            //// Создание обьектов для сохранения
            //for (int i = 0; i < colors.Count; i++)
            //{
            //    var colorObjForSave = new DataManager.LevelData.UsedInLevelColor(
            //        colors[i],
            //        colorIds[i],
            //        colorsConts[i]
            //        );
            //    usedColors.Add(colorObjForSave);

            ////}
            #endregion

            var cameraPos = GetCameraPosForSaving(createdLevelData);
            var itemsForSave = GetItemsForSave(createdLevelData);

            var savedItem = new DataManager.LevelData(
               cameraPos,
               createdLevelData.cameraScale,
               createdLevelData.Get_PlayerPosition(),
               levelSpecData.playerSpeed,
               levelSpecData.movementMode,
               levelSpecData.canSwitchColors,
               levelSpecData.globalTimer,
               levelSpecData.usedColors,
               itemsForSave
               );
            return savedItem;
        }

        private static Vector3 GetCameraPosForSaving(CreatedLevelData createdLevelData)
        {
            // Переворачивание камеры в другую ориентацию
            var cameraPos = createdLevelData.Get_CameraPosition();
            float z = cameraPos.z;
            float y = cameraPos.y;
            cameraPos.z = y;
            cameraPos.y = z;
            return cameraPos;
        }

        private static List<DataManager.LevelData.Item> GetItemsForSave(CreatedLevelData createdLevelData)
        {
            // Создание списка элементов
            var itemsForSave = new List<DataManager.LevelData.Item>();
            foreach (var item in createdLevelData.items)
            {
                var pos = item.transform.position;
                float zPos = pos.z;
                float yPos = pos.y;
                pos.z = yPos;
                pos.y = Math.Abs(zPos);

                var rot = item.transform.rotation.eulerAngles;
                if (item.name == "0" || item.name == "2")
                {
                    rot.x = 90;
                }
                else if (item.name == "1")
                {
                    rot.x = 0;
                }

                var go = new DataManager.LevelData.Item(
                    pos,
                    rot,
                    int.Parse(item.name),
                    false
                    );
                itemsForSave.Add(go);
            };
            return itemsForSave;
        }
        #endregion

        #region ПАРСИНГ ДАННЫХ УРОВНЯ В ДАННЫЕ ДЛЯ РЕДАКТОРА
        private static CreatedLevelData Parse_SavedLevelData_ToEditorLevelData(DataManager.LevelData levelData, int levelNumber)
        {
            List<CreatedColiderDatas> createdColiderDatas = new List<CreatedColiderDatas>();
            List<GameObject> createdItemDatas = new List<GameObject>();
            foreach (var item in levelData.items)
            {
                var go = new GameObject();
                go.name = item.itemId.ToString();
                var pos = item.GetPositon();
                float zPos = pos.z;
                float yPos = pos.y;
                pos.z = -yPos;
                pos.y = zPos;
                go.transform.position = pos;
                var rot = Quaternion.Euler(item.GetRotation());
                if (item.itemId == 0 || item.itemId == 2)
                {
                    rot.x = 0;
                }
                else if (item.itemId == 1)
                {
                    var rot2 = item.GetRotation();
                    rot2.x = 90;
                    rot = Quaternion.Euler(rot2);
                }
                go.transform.rotation = rot;

                createdItemDatas.Add(go);
                Destroy(go);
            }

            //// Переворачивание камеры в ориентацию редактора
            var cameraPos = levelData.GetCameraPos();
            float z = cameraPos.z;
            float y = cameraPos.y;
            cameraPos.y = z;
            cameraPos.z = -y;

            return new CreatedLevelData(
                levelNumber,
                levelData.GetPlayerPos(),
                cameraPos,
                levelData.cameraScale,
                createdItemDatas,
                createdColiderDatas
                );
        }
        #endregion
    }

    #region КЛАССЫ СТРУКТУРЫ КОТОРЫЕ НУЖНЫ РЕДАКТОРУ (ИСПОЛЬЗУЮТЬСЯ ДРЯ ХРАНЕНИЯ ДАННЫХ РЕДАКТОРОМ
    // Класс в который сохраняються данные об созданных элементах и созданных колайдерах и некоторые другие данные
    [Serializable]
    public class CreatedLevelData
    {
        public int levelNumber_ForDewelop = 0;
        // Позиция персонажа
        private float playerXStartPos = 0;
        private float playerYStartPos = 0;
        // Позиция камеры
        private float cameraXPos = 20;
        private float cameraYPos = 20;
        private float cameraZPos = -40;
        public float cameraScale = 20;
        // Массив всех добавленных обьектов
        public List<GameObject> items = new List<GameObject>();
        // Массив созданных колайдеров
        public List<CreatedColiderDatas> colliders = new List<CreatedColiderDatas>();


        public CreatedLevelData()
        {
        }

        public CreatedLevelData(
            int levelForDewelopNumber,
            Vector3 playerPos,
            Vector3 cameraPos,
            float cameraScale,
            List<GameObject> itemsList,
            List<CreatedColiderDatas> createdColiderDatas)
        {
            this.levelNumber_ForDewelop = levelForDewelopNumber;

            playerXStartPos = playerPos.x;
            playerYStartPos = playerPos.y;

            cameraXPos = cameraPos.x;
            cameraYPos = cameraPos.y;
            cameraZPos = cameraPos.z;

            this.cameraScale = cameraScale;

            this.items = itemsList;
            this.colliders = createdColiderDatas;
        }


        public Vector3 Get_CameraPosition()
        {
            return new Vector3(cameraXPos, cameraYPos, cameraZPos);
        }

        public Vector3 Get_PlayerPosition()
        {
            return new Vector3(playerXStartPos, playerYStartPos, -0.5f);
        }
    }
    // Класс с данными (массив позиций точек из которых строиться колайдер) о колайдерах созданных для под уровня
    [Serializable]
    public class CreatedColiderDatas
    {
        private float[] coliderPointsXPositions = new float[0];
        private float[] coliderPointsYPositions = new float[0];

        public CreatedColiderDatas(Vector3[] coliderPoints)
        {
            // Перевод массива вектор3 в массивы позиций по x,y,z для сериализации
            coliderPointsXPositions = new float[coliderPoints.Length];
            coliderPointsYPositions = new float[coliderPoints.Length];
            int i = 0;
            foreach (var pointPosition in coliderPoints)
            {
                coliderPointsXPositions[i] = pointPosition.x;
                coliderPointsYPositions[i] = pointPosition.y;
                i++;
            }
        }

        // Перевод массивов x,y,z в массив вектор3 
        public Vector2[] Get_ColiderPoints()
        {
            Vector2[] coliderPoints = new Vector2[coliderPointsXPositions.Length + 1];
            for (int i = 0; i < coliderPointsXPositions.Length; i++)
            {
                Vector2 coliderPoint = new Vector2(
                    coliderPointsXPositions[i],
                    coliderPointsYPositions[i]
                    );

                coliderPoints[i] = coliderPoint;
            }
            // Последняя точка равна первой точке, чтобы колайдер замкнулся
            coliderPoints[coliderPoints.Length - 1] = new Vector2(
                coliderPointsXPositions[0],
                coliderPointsYPositions[0]);
            return coliderPoints;
        }
    }
    #endregion
}
