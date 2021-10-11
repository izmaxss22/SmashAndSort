using UnityEngine;
using UnityEngine.UI;

public class GameTestItems : MonoBehaviour
{
    public Text fpsText;
    public Text levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        levelNumber.text = DataManager.Instance.GetLastAvailableLevelNumber().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ShowFPS();
    }

    private int showFpsTimer;
    private void ShowFPS()
    {
        //if (showFpsTimer == 0)
        //{
        fpsText.text = (1 / Time.deltaTime).ToString("00") + "   -  " + Screen.currentResolution.refreshRate;
        //}
        //else if (showFpsTimer == 60)
        //{
        //    showFpsTimer = 0;
        //    return;
        //}
        //showFpsTimer++;
    }

    public void NextLevel()
    {
        int level = DataManager.Instance.GetLastAvailableLevelNumber();
        DataManager.Instance.SetLastAvailableLevelNumber(++level);
        levelNumber.text = level.ToString();
    }

    public void PreviousLevel()
    {
        int level = DataManager.Instance.GetLastAvailableLevelNumber();
        if (level > 0)
        {
            DataManager.Instance.SetLastAvailableLevelNumber(--level);
            levelNumber.text = level.ToString();
        }
    }
}
