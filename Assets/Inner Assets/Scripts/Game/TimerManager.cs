using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private GameManager GameManager;
    private bool timerIsGlobal;
    private int globalTimerDuration;
    private List<DataManager.LevelData.UsedInLevelColor> colorsData = new List<DataManager.LevelData.UsedInLevelColor>();
    private IEnumerator TimerCountingCoroutine;

    private int lastUsedColorInArrayForTimer;

    private void Start()
    {
        GameManager = GameManager.Instance;
    }

    // Инициализация данных для таймера на подуровне
    public void InitTimerData(GameManager.GameData gameData)
    {
        lastUsedColorInArrayForTimer = 0;
        var levelNumber = gameData.subLevelInProgressNumber;
        if (gameData.levelDatas[levelNumber].globalTimer > 0)
        {
            timerIsGlobal = true;
            globalTimerDuration = gameData.levelDatas[levelNumber].globalTimer;
        }
        else
        {
            timerIsGlobal = false;
        }
        colorsData = gameData.levelDatas[gameData.subLevelInProgressNumber].usedColors;
    }

    public void OnGameOver()
    {
        StopTimer();
    }

    // При переключении на след под уровень
    public void OnChangeSubLevel(GameManager.GameData gameData)
    {
        // Пересоздаю таймер и запускаю его заного (учитывая необходимость)
        InitTimerData(gameData);
        StopTimer();
    }

    // При первом передвижении персонажа на уровне
    public void OnPlayerFirstMoveOnLevel()
    {
        StartTimer(lastUsedColorInArrayForTimer);
    }

    // При полном сборе одного цвета
    public void OnCollectAllOneColorItems(int nextColorId)
    {
        // Если таймер не глобальный (глобальный таймер не нужно перезапускать) 
        if (timerIsGlobal == false)
        {
            //То перезапускаеться таймер для следующего цвета
            StopTimer();
            // Перебор всех цветом на поисх подходящего
            for (int i = 0; i < colorsData.Count; i++)
                if (nextColorId == colorsData[i].colorId)
                    // Проверка на то есть ли у него таймер
                    if (colorsData[i].timerLenght > 0)
                    {
                        StartTimer(i);
                    }
                    else
                    {
                        lastUsedColorInArrayForTimer = i;
                    }
        }
    }

    // Остановка таймера
    private void StopTimer()
    {
        GameManager.OnDeactivateTimer();
        if (TimerCountingCoroutine != null)
        {
            StopCoroutine(TimerCountingCoroutine);
            TimerCountingCoroutine = null;
        }
    }

    // Корутина запуска таймера
    private void StartTimer(int colorInArrayNumber)
    {
        if (TimerCountingCoroutine == null)
        {
            // Если таймер глобальный
            if (timerIsGlobal)
            {
                // Запуск глобального таймера
                GameManager.OnTimerStart(-1);
                TimerCountingCoroutine = TimerCounting(globalTimerDuration);
                StartCoroutine(TimerCountingCoroutine);
            }
            // Иначе проверяю необходимость запуска таймера для цвета 
            else
            {
                // Получение значения нужно ли запускать таймер (есть ли он для цвета)
                bool isNeedStartColorTimer = colorsData[colorInArrayNumber].timerLenght > 0;
                lastUsedColorInArrayForTimer = colorInArrayNumber;
                if (isNeedStartColorTimer)
                {
                    // Запуск таймера для этого цвета
                    GameManager.OnTimerStart(colorInArrayNumber);
                    TimerCountingCoroutine = TimerCounting(colorsData[colorInArrayNumber].timerLenght);
                    StartCoroutine(TimerCountingCoroutine);
                }
            }
        }
    }

    // Корутина которая отвечает за работу таймера
    private IEnumerator TimerCounting(int timerDuration)
    {
        int duration = timerDuration;
        while (true)
        {
            duration--;
            GameManager.OnTimerChangeValue(duration);
            if (duration > 0)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                GameManager.OnTimerEnd();
                yield break;
            }
        }
    }
}