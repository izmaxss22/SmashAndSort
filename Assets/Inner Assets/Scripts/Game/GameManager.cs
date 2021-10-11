using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameProgressManager GameProgressManager;
    public MapItemsManager MapItemsManager;
    public PlayerManager PlayerManager;
    public GameUIManager GameUIManager;
    public TimerManager TimerManager;

    // Класс обертка для игровых данных (здесь собираються все данные которые используються в игре)
    public class GameData
    {
        public int subLevelInProgressNumber;
        public int coinsCount;
        // Доступ по айди цвета
        public Dictionary<int, int> colorCounts;
        // Пересозданный массив для доступа к используемым в ПОДУРОВНЕ цветам по ид
        public Dictionary<int, Color> usedColorsById;
        public List<DataManager.LevelData> levelDatas;

        public GameData(
             int subLevelInProgressNumber,
             int coinsCount,
             Dictionary<int, int> colorCounts,
             Dictionary<int, Color> usedColorsById,
             List<DataManager.LevelData> levelDatas
            )
        {
            this.subLevelInProgressNumber = subLevelInProgressNumber;
            this.coinsCount = coinsCount;
            this.colorCounts = colorCounts;
            this.usedColorsById = usedColorsById;
            this.levelDatas = levelDatas;
        }
    }

    public static GameData gameData;
    public static GameManager Instance;

    private bool gameIsOvered;
    private bool reviveIsUsed;
    private bool playerIsMoved;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateLevelForMainMenu(int[] subLevelNumbers)
    {
        CreateGameData(subLevelNumbers);
        MapItemsManager.CreateMapItems(gameData.levelDatas[0]);
        GameUIManager.CreateUI(gameData);
    }

    public void InitGameFromMainMenu()
    {
        ChangeGamesCountForShowingAd();
        PlayerManager.CreatePlayer(gameData);
        TimerManager.InitTimerData(gameData);
    }

    public void InitGame(int[] subLevelNumbers)
    {
        ChangeGamesCountForShowingAd();
        CreateGameData(subLevelNumbers);
        MapItemsManager.CreateMapItems(gameData.levelDatas[0]);
        PlayerManager.CreatePlayer(gameData);
        GameUIManager.CreateUI(gameData);
        TimerManager.InitTimerData(gameData);
    }

    public void ShowUI()
    {
        GameUIManager.ShowUI();
    }

    public void HideUI()
    {
        GameUIManager.HideUI();
    }

    // Изменение количества игр для определения когда надо показывать рекламу
    private void ChangeGamesCountForShowingAd()
    {
        int gamesCount = DataManager.Instance.GetGamesCountForAd();
        if (gamesCount < 3)
        {
            gamesCount++;
            DataManager.Instance.SetGamesCountForAd(gamesCount);
        }
        else
        {
            gamesCount = 1;
            DataManager.Instance.SetGamesCountForAd(gamesCount);
        }
    }

    // Создание обьекта (класс обертка для игровых данных) с данными которые используеються в игре
    private void CreateGameData(int[] subLevelNumbers)
    {
        // todo Получаю номер партикла персонажа и другие данные и сую их в класс GameData
        List<DataManager.LevelData> levels = new List<DataManager.LevelData>();
        // Загрузка всех уровней
        foreach (var item in subLevelNumbers)
        {
            var level = SerealizationManager.LoadConstantSerealizedObject<DataManager.LevelData>(
                item.ToString() + ".data");
            levels.Add(level);
        }
        int subLevelInProgressNumber = 0;
        int coinsCount = DataManager.Instance.GetCoinsCount();
        Dictionary<int, int> colorCounts = new Dictionary<int, int>();
        Dictionary<int, Color> usedColorsById = new Dictionary<int, Color>();

        for (int i = 0; i < levels[0].usedColors.Count; i++)
        {
            Color color = levels[0].usedColors[i].GetColor();
            int colorId = levels[0].usedColors[i].colorId;
            int colorCount = levels[0].usedColors[i].colorCounts;
            colorCounts.Add(colorId, colorCount);
            usedColorsById.Add(colorId, color);
        }

        gameData = new GameData(
            subLevelInProgressNumber,
            coinsCount,
            colorCounts,
            usedColorsById,
            levels
            );
    }

    public void UpdateGameDataAffterCompleteSubLevel()
    {
        var currentSubLevelData = gameData.levelDatas[gameData.subLevelInProgressNumber];

        Dictionary<int, int> colorCounts = new Dictionary<int, int>();
        Dictionary<int, Color> usedColorsById = new Dictionary<int, Color>();

        for (int i = 0; i < currentSubLevelData.usedColors.Count; i++)
        {
            Color color = currentSubLevelData.usedColors[i].GetColor();
            int colorId = currentSubLevelData.usedColors[i].colorId;
            int colorCount = currentSubLevelData.usedColors[i].colorCounts;
            colorCounts.Add(colorId, colorCount);
            usedColorsById.Add(colorId, color);
        }

        gameData.colorCounts = colorCounts;
        gameData.usedColorsById = usedColorsById;
    }

    #region СОБЫТИЯ В ИГРЕ
    // GAME PROGRESS MANAGER EVENT
    public void OnCompleteSubLevel()
    {
        StartCoroutine(SwitchToNextSubLevel());
    }

    private IEnumerator SwitchToNextSubLevel()
    {
        gameIsOvered = true;
        playerIsMoved = false;
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_COMPLETE_LEVEL);
        // После прохождения под уровня я могу возродиться еще раз
        reviveIsUsed = false;
        GameUIManager.ShowDimmer();
        UpdateGameDataAffterCompleteSubLevel();
        TimerManager.OnChangeSubLevel(gameData);
        GameUIManager.SetCanvasItemsForSubLevel(gameData);
        yield return new WaitForSeconds(0.5f);
        // Удаление данных прошлого уровня
        MapItemsManager.DeletaMapItems();
        GameUIManager.DeleteChangeColorButtons();
        GameUIManager.OnCompleteSubLevel(gameData.subLevelInProgressNumber - 1);
        PlayerManager.DestroyPlayer();
        // Установка данных нового уровня
        var levelData = gameData.levelDatas[gameData.subLevelInProgressNumber];
        MapItemsManager.CreateMapItems(levelData);
        GameUIManager.CreateChangeColorButton(gameData);
        PlayerManager.CreatePlayer(gameData);
        GameUIManager.HideDimmer();
        gameIsOvered = false;
        yield break;
    }

    // GAME PROGRESS MANAGER EVENT
    public void OnCompleteLevel()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        gameIsOvered = true;
        PlayerManager.DisablePlayerMovement();
        HideUI();
        yield return new WaitForSeconds(0.3f);
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_COMPLETE_LEVEL);
        VibrationManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Success);
        DataManager.Instance.SetCoinsCount(gameData.coinsCount);
        // Делаю доступным следующий уровень
        int levelInProgress = DataManager.Instance.GetLastAvailableLevelNumber();
        DataManager.Instance.SetLastAvailableLevelNumber(++levelInProgress);
        // Вызвать окно после игры с данными
        WindowsManager.Instance.FromGameToAffterGameScreen(gameObject, levelInProgress - 1, false, reviveIsUsed);
    }

    // Внутренний метод для окончанмия игры
    public void OnGameOver()
    {
        if (gameIsOvered == false)
        {
            gameIsOvered = true;
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_GAME_OVER);
        VibrationManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Failure);
        TimerManager.OnGameOver();
        playerIsMoved = false;
        PlayerManager.DisablePlayerMovement();
        HideUI();
        yield return new WaitForSeconds(0.3f);
        int levelInProgress = DataManager.Instance.GetLastAvailableLevelNumber();
        WindowsManager.Instance.FromGameToAffterGameScreen(gameObject, levelInProgress, true, reviveIsUsed);
        yield break;
    }

    // Внутренний метод для окончанмия игры
    public void OnUserEndGame()
    {
        if (gameIsOvered == false)
        {
            gameIsOvered = true;
            StartCoroutine(UserEndGame());
        }
    }

    private IEnumerator UserEndGame()
    {
        TimerManager.OnGameOver();
        playerIsMoved = false;
        PlayerManager.DisablePlayerMovement();
        HideUI();
        yield return new WaitForSeconds(0.3f);
        int levelInProgress = DataManager.Instance.GetLastAvailableLevelNumber();
        WindowsManager.Instance.FromGameToAffterGameScreen(gameObject, levelInProgress, true, true);
        yield break;
    }


    // При нажатии возрождения
    public void OnGameRevive()
    {
        gameData.coinsCount = DataManager.Instance.GetCoinsCount();
        PlayerManager.EnablePlayerMovement();
        //reviveIsUsed = true;
        gameIsOvered = false;
    }

    // GAME UI MANAGER EVENT
    public void OnChangeColor(int colorId)
    {
        PlayerManager.OnChangeColor(colorId);
    }

    // GAME PROGRESS MANAGER EVENT
    public void OnCollectAllOneColorItems(int lastColorId, int nextColorId)
    {
        AudioManager.Instance.PlayAudioSourceWithSideToSidePitch(AudioManager.AudioSourcesIds.GAME_COLLECT_ALL_COLORS, 1.5f, 0.05f);

        PlayerManager.OnCollectAllOneColorItem(nextColorId);
        GameUIManager.OnCollectAllOneColorItems(lastColorId, nextColorId);
        TimerManager.OnCollectAllOneColorItems(nextColorId);
    }

    #region СОБЫТИЯ ПЕРСОНАЖА
    // PLAYER MANAGER EVENT
    public void OnPlayerDeath()
    {
    }

    // PLAYER COLISION DETECTER EVENT
    public void OnCollectColorItem(int colorNumber)
    {
        int itemsLeft = GameProgressManager.OnCollectColorItem(colorNumber);
        if (itemsLeft != 0)
        {
            GameUIManager.OnCollectColorItem(colorNumber, gameData.colorCounts[colorNumber]);
        }
    }

    // PLAYER COLISION DETECTER EVENT
    public void OnCollectCoinItem()
    {
        GameProgressManager.OnCollectCoin();
        GameUIManager.OnCollectCoinItem(gameData.coinsCount);
    }

    // PLAYER CONTROLER EVENT

    public void OnPlayerMove()
    {
        if (playerIsMoved == false)
        {
            TimerManager.OnPlayerFirstMoveOnLevel();
            playerIsMoved = true;
        }
    }
    #endregion

    // DEAD CUBE ITEM EVENT
    public void OnCollisionWithDeadItem()
    {
        // todo Проверить могу ли я умереть (типо щит или еще чего)
        OnGameOver();
    }

    #region СОБЫТИЯ ТАЙМЕРА
    // TIMER MANAGER EVENT при окончании времени у таймера
    public void OnTimerEnd()
    {
        OnGameOver();
    }

    // TIMER MANAGER EVENT при начале отсчета таймера
    public void OnTimerStart(int usedColorInArrayNumber)
    {
        GameUIManager.OnTimerStart(gameData, usedColorInArrayNumber);
    }

    // TIMER MANAGER EVENT изменение оставшегося времени таймера
    public void OnTimerChangeValue(int duration)
    {
        GameUIManager.OnTimerChangeValue(duration);
    }

    // TIMER MANAGER EVENT при отключении таймера (по причине отсутсвия таймера для след цвета)
    public void OnDeactivateTimer()
    {
        GameUIManager.OnTimerDeactivate();
    }
    #endregion
    #endregion
}
