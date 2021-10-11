using UnityEngine;
using UnityEngine.UI;

public class ColorItem : MonoBehaviour
{
    public Image colorIcon;
    public InputField fieldForColorTimer;
    private int colorId;
    private int colorCounts;

    public void Init(Color color, int colorId, int colorCounts, int colorTimer)
    {
        colorIcon.color = color;
        this.colorId = colorId;
        this.colorCounts = colorCounts;
        fieldForColorTimer.text = colorTimer.ToString();
    }

    public DataManager.LevelData.UsedInLevelColor GetData()
    {
        var data = new DataManager.LevelData.UsedInLevelColor(
             colorIcon.color,
             colorId,
             colorCounts,
             int.Parse(fieldForColorTimer.text));
        return data;
    }

    public class Data
    {
        public Color color;
        public int colorId;
        public int colorCounts;
        public int colorTimer;
    }
}
