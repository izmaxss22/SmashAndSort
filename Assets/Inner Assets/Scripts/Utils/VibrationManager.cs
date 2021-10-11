using MoreMountains.NiceVibrations;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private DataManager DataManager;
    public static VibrationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager = DataManager.Instance;
    }

    public void Vibrate(HapticTypes hapticType)
    {
        if (DataManager.GetVibrationState())
        {
            MMVibrationManager.Haptic(hapticType);
        }
    }
}
