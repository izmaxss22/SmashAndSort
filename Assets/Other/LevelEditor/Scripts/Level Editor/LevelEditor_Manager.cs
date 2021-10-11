using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelEditor_Manager : MonoBehaviour
    {
        private float zoomSpeed = 7f;
        private float dragSpeed = 60f;

        private Camera mainCamera;

        public LevelEditor_ItemsList itemsForLevelEditor;
        public LevelEditorDataManager LevelEditorDataManager;

        public MediatorFor_ItemsMode mediatorFor_ItemsMode;
        public MediatorFor_ColiderMode mediatorFor_ColiderMode;

        private CreatedLevelData CreatedLevelData = new CreatedLevelData();

        private int activeEditorModeNumber = -1;
        private enum editorModeNumbers
        {
            ItemMode,
            ColliderMode
        }

        public GameObject prefab_cubeForGridMaking_0;
        public GameObject prefab_cubeForGridMaking_1;
        public GameObject contFor_editorGrid;

        public GameObject buttonsPanel;
        public InputField inputField_specifiedLevelNumberForLoad;
        public InputField inputFieldFor_playerXPos;
        public InputField inputFieldFor_playerYPos;
        public InputField inputFieldFor_playerZPos;

        void Start()
        {
            mainCamera = Camera.main;
            DrawGrid();
            // Обнуление уровня (чтобы прошел проверку при вызове LoadLevel()
            CreatedLevelData.levelNumber_ForDewelop = -1;
            LoadLevelData(0);
        }

        void Update()
        {
            VievportControler();
        }

        #region Методы нажатия на кнопки
        // Метод открыткия/закрытия панели с кнопками
        public void OnClick_ShowButtonsPanelButton()
        {
            buttonsPanel.SetActive(!buttonsPanel.activeSelf);
        }

        // Загрузить уровень на +1
        public void OnClick_LoadNextLevelButton()
        {
            LoadLevelData(CreatedLevelData.levelNumber_ForDewelop + 1);
        }

        // Загрузить уровень на -1
        public void OnClick_LoadPreviousLevelButton()
        {
            LoadLevelData(CreatedLevelData.levelNumber_ForDewelop - 1);
        }

        // Загрузить указанный уровень
        public void OnClick_LoadSpecifiedLevelButton()
        {
            int levelForLoadNumber = int.Parse(inputField_specifiedLevelNumberForLoad.text);
            LoadLevelData(levelForLoadNumber);
        }

        // Сохранить уровень который сейчас редактируеться
        public void OnClick_SaveLevelDatasButton()
        {
            CreateLevelDataInputerDialog();
        }

        // Переключение режимов
        public void OnCLick_ButtonPickEditorMode(int modeNumber)
        {
            Change_EditorMode(modeNumber);
        }
        #endregion

        #region РАБОЧИЕ МЕТОДЫ
        // Работа с перемещением по эдитору
        private void VievportControler()
        {
            // Перемещение камеры в стороны
            if (Input.GetMouseButton(1))
            {
                mainCamera.transform.Translate(
                    -Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed,
                    -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed,
                    0);
            }
            // Приблежение камеры
            else if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0)
            {
                // Чтобы приближение поля на срабатывало при промотке канваса выбора элементов
                if (Input.mousePosition.y > 350)
                {
                    mainCamera.orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                }
            }
        }

        private void DrawGrid()
        {
            Vector3 position = new Vector3(0, 0, 1);
            for (int x = 0; x < 50; x++)
            {
                for (int y = 1; y < 50; y++)
                {
                    position.x = x;
                    position.y = y;

                    if ((x + y) % 2 == 0) Instantiate(prefab_cubeForGridMaking_0,
                        position,
                        Quaternion.identity,
                        contFor_editorGrid.transform);


                    else
                    {
                        Instantiate(
                      prefab_cubeForGridMaking_1,
                      position,
                      Quaternion.identity,
                      contFor_editorGrid.transform);
                    }
                }
            }
        }

        private void Change_EditorMode(int modeNumber)
        {
            if (activeEditorModeNumber != modeNumber)
            {
                // Выключение прошлого режима
                switch (activeEditorModeNumber)
                {
                    case (int)editorModeNumbers.ItemMode:
                        mediatorFor_ItemsMode.OnModeDisable();
                        break;
                    case (int)editorModeNumbers.ColliderMode:
                        mediatorFor_ColiderMode.OnModeDisable();
                        break;
                }
                // Включение нового режима
                switch (modeNumber)
                {
                    case (int)editorModeNumbers.ItemMode:
                        mediatorFor_ItemsMode.OnModeEnable();
                        break;
                    case (int)editorModeNumbers.ColliderMode:
                        mediatorFor_ColiderMode.OnModeEnable();
                        break;
                }
                activeEditorModeNumber = modeNumber;
            }
        }

        public GameObject prefabForLevelDataInputerScreen;
        public GameObject cont;
        public Toggle levelIsChangedToogle;
        private void CreateLevelDataInputerDialog()
        {
            var go = Instantiate(prefabForLevelDataInputerScreen, cont.transform);
            var itemsDatas = mediatorFor_ItemsMode.GetCreatedItems();

            var savingPath = Application.streamingAssetsPath + "/";
            string filePath = savingPath + CreatedLevelData.levelNumber_ForDewelop + ".data";

            DataManager.LevelData levelData;
            // Если есть файл с данными уровня
            if (File.Exists(filePath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);

                levelData = (DataManager.LevelData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            // Иначе пустой обьект дефолтный обьеккт (с дефолтными данными)
            else
            {
                levelData = new DataManager.LevelData(Vector3.zero, 0, Vector3.zero, 15, 0, true, 0, null, null);
            }

            go.GetComponent<LevelDataInputer>().Init(levelData, itemsDatas, levelIsChangedToogle.isOn, SaveLevelData);
        }

        private void SaveLevelData(LevelDataInputer.LevelSpecData specData)
        {
            // Получения данных уровня
            int levelNumber = CreatedLevelData.levelNumber_ForDewelop;
            Vector3 cameraPos = mainCamera.transform.position;
            float cameraScale = mainCamera.orthographicSize;
            Vector3 playerPos = new Vector3(
                float.Parse(inputFieldFor_playerXPos.text),
                float.Parse(inputFieldFor_playerYPos.text),
                float.Parse(inputFieldFor_playerZPos.text));
            var itemsDatas = mediatorFor_ItemsMode.GetCreatedItems();
            var colidersDatas = mediatorFor_ColiderMode.Get_CreatedColidersList();
            // Создание обьекта для сохранения и сохранения
            CreatedLevelData newLevelDataForSaving = new CreatedLevelData(
                levelNumber,
                playerPos,
                cameraPos,
                cameraScale,
                itemsDatas,
                colidersDatas);
            LevelEditorDataManager.SaveLevelData(newLevelDataForSaving, levelNumber, specData);
        }

        private void LoadLevelData(int levelNumber)
        {
            // Если уровень отличаеться от того что сейчас и уровень не меньше 0
            if (levelNumber != CreatedLevelData.levelNumber_ForDewelop && levelNumber >= 0)
            {
                CreatedLevelData = LevelEditorDataManager.LoadLevelData(levelNumber);
                // Установка данных  о камере на уровне
                mainCamera.transform.position = CreatedLevelData.Get_CameraPosition();
                mainCamera.orthographicSize = CreatedLevelData.cameraScale;
                // Установка позиции персонажа на уровне
                inputFieldFor_playerXPos.text = CreatedLevelData.Get_PlayerPosition().x.ToString();
                inputFieldFor_playerYPos.text = CreatedLevelData.Get_PlayerPosition().y.ToString();
                inputFieldFor_playerZPos.text = CreatedLevelData.Get_PlayerPosition().z.ToString();

                inputField_specifiedLevelNumberForLoad.text = levelNumber.ToString();

                mediatorFor_ItemsMode.InitMode(CreatedLevelData, itemsForLevelEditor.GetItems());
                mediatorFor_ColiderMode.InitMode(CreatedLevelData);
            }
        }
    }
    #endregion
}