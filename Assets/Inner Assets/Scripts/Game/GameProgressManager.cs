using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public void Init(GameManager.GameData gameData)
    {
    }

    public void OnCollectCoin()
    {
        GameManager.gameData.coinsCount += 1;
    }

    public int OnCollectColorItem(int colodId)
    {
        var gameData = GameManager.gameData;
        var newCount = --gameData.colorCounts[colodId];
        // Если собрал все цвета данного цвета
        if (newCount == 0)
        {
            bool collectAllColors = true;
            // Проверка на то остались ли другие цвета
            foreach (var item in gameData.colorCounts.Values)
            {
                if (item > 0)
                {
                    collectAllColors = false;
                }
            }
            // Если собрал все цвета на уровне
            if (collectAllColors)
            {
                // Если это был последний уровень
                if (gameData.subLevelInProgressNumber + 1 == gameData.levelDatas.Count)
                    GameManager.Instance.OnCompleteLevel();
                // Иначе переход к след подуровню
                else
                {
                    gameData.subLevelInProgressNumber++;
                    GameManager.Instance.OnCompleteSubLevel();
                }
            }
            // Иначе говорю о том что собрал все цвета одного цвета
            else
            {
                var nextColorId = DefineNextAvailableColor();
                GameManager.Instance.OnCollectAllOneColorItems(colodId, nextColorId);
            }
        }

        return newCount;
    }

    // Определение следующего доступного цвета после сбора прошлого
    public int DefineNextAvailableColor()
    {
        int nextColorId = 0;
        foreach (var item in GameManager.gameData.colorCounts)
        {
            if (item.Value > 0)
            {
                nextColorId = item.Key;
                break;
            }
        }
        return nextColorId;
    }
}
