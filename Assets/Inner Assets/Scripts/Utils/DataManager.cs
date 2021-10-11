using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region ПЕРЕМЕННЫЕ

    public List<GameObject> prefabsForGameItems;

    // Массив массивов который содержит данные о номерах подуровней лежащих в одном уровне
    public int[][] dataForLevelGrouper = new int[][]
    {
        // 1 -5
        new int[]{3,2},
        new int[]{4,5},
        new int[]{8},
        new int[]{6,10},
        new int[]{7},
        // 6-10
        new int[]{9},
        new int[]{14},
        new int[]{11},
        new int[]{12},
        new int[]{15},
        // 11 - 15
        new int[]{13},
        new int[]{17},
        new int[]{16},
        new int[]{18},
        new int[]{0},
        new int[]{1},
    };

    public static DataManager Instance;
    private enum PlayerPrefsIds
    {
        ENERGY_COUNT = 1,
        COINS_COUNT = 2,
        LAST_AVIALABLE_LEVEL = 3,
        SOUND_MUTE_STATE = 4,
        VIBRATION_STATE = 5,
        GAMES_COUNT_FOR_AD = 6,
        GAME_LAUNCH_COUNTS = 7,
        RATE_SCREEN_CALLED_COUNTS = 8,
        PICKED_PLANE_SKIN_NUMBER = 9
    };

    public const int MAX_ENERGY_COUNT = 5;

    public int gameId_appStore = 4007192;
    public int gameId_googlePLay = 4007193;

    public const string adPlacementGetEnergy = "getEnergyAd";
    public const string adPlacementRevive = "reviveAd";
    public const string adPlacementUsual = "usualAd";

    public event Action OnChangeCoinsCount;
    // public event Action OnChangeEnergyCount;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    #region КОНВЕРТОРЫ
    private bool Convert_Int_ToBool(int state)
    {
        if (state == 1) return true;
        else return false;
    }
    private int Convert_Bool_ToInt(bool state)
    {
        if (state == true) return 1;
        else return 0;
    }
    #endregion

    #region ПОЛУЧЕНИЕ КОЛИЧЕСТВА ЭНЕРГИИ
    // public int GetEnergyCount()
    // {
    //     return PlayerPrefs.GetInt(PlayerPrefsIds.ENERGY_COUNT.ToString(), 5);
    // }
    //
    // public void Set_EnergyCount(int value)
    // {
    //     PlayerPrefs.SetInt(PlayerPrefsIds.ENERGY_COUNT.ToString(), value);
    //     OnChangeEnergyCount?.Invoke();
    // }
    #endregion

    #region ПОЛУЧЕНИЕ КОЛИЧЕСТВА МОНЕТОК
    public int GetCoinsCount()
    {
        return PlayerPrefs.GetInt(PlayerPrefsIds.COINS_COUNT.ToString(), 0);
    }

    public void SetCoinsCount(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsIds.COINS_COUNT.ToString(), value);
        OnChangeCoinsCount?.Invoke();
    }
    #endregion

    #region ПОЛУЧЕНИЕ НОМЕРА ПОСЛЕДНЕГО ДОСТУПНОГО ДЛЯ ПРОХОЖДЕНИЯ УРОВНЯ
    public int GetLastAvailableLevelNumber()
    {
        return PlayerPrefs.GetInt(PlayerPrefsIds.LAST_AVIALABLE_LEVEL.ToString(), 0);
    }

    public void SetLastAvailableLevelNumber(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsIds.LAST_AVIALABLE_LEVEL.ToString(), value);
    }
    #endregion

    #region ПОЛУЧЕНИЕ КОЛИЧЕСТВА ЗАПУСКОВ ПРИЛОЖЕНИЯ
    public int GetGameLaunchCounts()
    {
        return PlayerPrefs.GetInt(PlayerPrefsIds.GAME_LAUNCH_COUNTS.ToString(), 0);
    }

    public void SetGameLaunchCounts(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsIds.GAME_LAUNCH_COUNTS.ToString(), value);
    }
    #endregion

    #region ПОЛУЧЕНИЕ КОЛИЧЕСТВА ВЫЗОВО ОКНА ОЦЕНКИ
    public int GetRateScreenCallsCount()
    {
        return PlayerPrefs.GetInt(PlayerPrefsIds.RATE_SCREEN_CALLED_COUNTS.ToString(), 0);
    }

    public void SetRateScreenCallsCount(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsIds.RATE_SCREEN_CALLED_COUNTS.ToString(), value);
    }
    #endregion

    #region ПОЛУЧЕНИЕ КОЛИЧЕСТВА ИГР НЕОБХОДИМОЕ ДЛЯ ОПРЕДЕЛЕНИЯ КОГДА ПОКАЗЫВАТЬ РЕКЛАМУ
    public int GetGamesCountForAd()
    {
        return PlayerPrefs.GetInt(PlayerPrefsIds.GAMES_COUNT_FOR_AD.ToString(), 0);
    }

    public void SetGamesCountForAd(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsIds.GAMES_COUNT_FOR_AD.ToString(), value);
    }
    #endregion

    #region СОСТОЯНИЕ ЗВУКА
    public bool GetSoundState()
    {
        int state = PlayerPrefs.GetInt(PlayerPrefsIds.SOUND_MUTE_STATE.ToString(), 1);
        return Convert_Int_ToBool(state);
    }

    public void SetSoundState(bool state)
    {
        PlayerPrefs.SetInt(
            PlayerPrefsIds.SOUND_MUTE_STATE.ToString(),
            Convert_Bool_ToInt(state));
    }
    #endregion

    #region СОСТОЯНИЕ ВИБРАЦИИ
    public bool GetVibrationState()
    {
        int state = PlayerPrefs.GetInt(PlayerPrefsIds.VIBRATION_STATE.ToString(), 1);
        return Convert_Int_ToBool(state);
    }

    public void SetVibrationState(bool state)
    {
        PlayerPrefs.SetInt(
            PlayerPrefsIds.VIBRATION_STATE.ToString(),
            Convert_Bool_ToInt(state));
    }

    #endregion

    #region ПОЛУЧЕНИЕ КОЛИЧЕСТВА ВЫЗОВО ОКНА ОЦЕНКИ
    public int GetPickedPlaneSkinNumber()
    {
        return PlayerPrefs.GetInt(PlayerPrefsIds.PICKED_PLANE_SKIN_NUMBER.ToString(), 0);
    }

    public void SetPickedPlaneSkinNumber(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsIds.PICKED_PLANE_SKIN_NUMBER.ToString(), value);
    }
    #endregion

    #region КЛАССЫ СТРУКТУРЫ
    [Serializable]
    public class LevelData
    {
        public float cameraPosX;
        public float cameraPosY;
        public float cameraPosZ;
        public float cameraScale;

        public float playerPosX;
        public float playerPosY;
        public float playerPosZ;

        public float playerSpeed;
        public int idForPlayerMovementMode;
        public bool canSwitchColors;
        public int globalTimer;
        public List<UsedInLevelColor> usedColors = new List<UsedInLevelColor>();
        public List<Item> items = new List<Item>();

        public LevelData(
            Vector3 cameraPos,
            float cameraScale,
            Vector3 playerPos,
            float playerSpeed,
            int idForPlayerMovementMode,
            bool canSwitchColors,
            int globalTimer,
            List<UsedInLevelColor> usedColors,
            List<Item> items
            )
        {
            this.cameraPosX = cameraPos.x;
            this.cameraPosY = cameraPos.y;
            this.cameraPosZ = cameraPos.z;

            this.cameraScale = 36;

            this.playerPosX = playerPos.x;
            this.playerPosY = playerPos.y;
            this.playerPosZ = playerPos.z;

            this.playerSpeed = playerSpeed;
            this.idForPlayerMovementMode = idForPlayerMovementMode;
            this.canSwitchColors = canSwitchColors;
            this.globalTimer = globalTimer;
            this.usedColors = usedColors;
            this.items = items;
        }

        [Serializable]
        // Класс для массива с описанием используеммых цветов на уровне
        public class UsedInLevelColor
        {
            public float r;
            public float g;
            public float b;
            public int colorId;
            // Количество этого цвета в уровне
            public int colorCounts;
            // Длина таймера для этого цвета
            public int timerLenght;

            public UsedInLevelColor(
                Color сolor,
                int colorId,
                int colorCounts,
                int timerLenght
                )
            {
                this.r = сolor.r;
                this.g = сolor.g;
                this.b = сolor.b;
                this.colorId = colorId;
                this.colorCounts = colorCounts;
                this.timerLenght = timerLenght;
            }

            public Color GetColor()
            {
                return new Color(r, g, b);
            }
        }

        [Serializable]
        public class Item
        {
            public float itemPosX;
            public float itemPosY;
            public float itemPosZ;

            public float itemRotationX;
            public float itemRotationY;
            public float itemRotationZ;

            public int itemId;

            public bool isHaveSpecData;

            public Item(
                Vector3 itemPos,
                Vector3 itemRot,
                int itemId,
                bool isHaveSpecData
                )
            {
                itemPosX = itemPos.x;
                itemPosY = itemPos.y;
                itemPosZ = itemPos.z;
                this.itemRotationX = itemRot.x;
                this.itemRotationY = itemRot.y;
                this.itemRotationZ = itemRot.z;

                this.itemId = itemId;

                this.isHaveSpecData = isHaveSpecData;
            }

            public Vector3 GetPositon()
            {
                return new Vector3(itemPosX, itemPosY, itemPosZ);
            }

            public Vector3 GetRotation()
            {
                return new Vector3(itemRotationX, itemRotationY, itemRotationZ);
            }

            public Quaternion GetRotationInEuler()
            {
                return Quaternion.Euler(new Vector3(itemRotationX, itemRotationY, itemRotationZ));
            }
        }

        public Vector3 GetCameraPos()
        {
            return new Vector3(cameraPosX, 90, cameraPosZ);
        }

        public Vector3 GetPlayerPos()
        {
            return new Vector3(playerPosX, playerPosY, 1);
        }

    }
    #endregion
}


// Управление
// Поле изменить текстру
// Кубики заходят за ui
// Один кубик в левом верхнем углу дроугой чв правом нижнемим и заебывает ехать поэтому нужно ускорение
// Движение во все стороны
// Свайпы криво (
// Но если кубикы далеко то
// Слайдер скорости