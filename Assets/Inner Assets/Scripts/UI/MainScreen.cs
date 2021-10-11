using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public GameObject prefabForGameManager;
    private GameObject createdGameManager;
    public Text textForLevelNumber;
    public GameObject contForSubLevelsInfo;
    public GameObject textSoon;

    public Button buttonStart;
    private int pickedModeId;
    public GameObject buttonLevelsMode;
    public Text textLevelsMode;
    public GameObject buttonRelaxMode;
    public Text textRelaxMode;
    public Sprite spriteForLevelModeButtonDisabled;
    public Sprite spriteForLevelModeButtonEnabled;
    public Sprite spriteForRelaxlModeButtonDisabled;
    public Sprite spriteForRelaxModeButtonEnabled;
    public Color colorForTextChangeModeButtonsWhenDisabled;

    public Image buttonVibration;
    public Image buttonSound;

    public GameObject subLevelsInfoItemPrefab;

    public Sprite spriteForButtonDisabled;
    public Sprite spriteForButtonEnabled;

    // Start is called before the first frame update
    void Start()
    {
        var lastAvailableNumber = DataManager.Instance.GetLastAvailableLevelNumber();
        textForLevelNumber.text = "LEVEL " + (lastAvailableNumber + 1).ToString();
        foreach (var item in DataManager.Instance.dataForLevelGrouper[lastAvailableNumber])
        {
            Instantiate(subLevelsInfoItemPrefab, contForSubLevelsInfo.transform);
        }
        InitGameManagerLevel();
        InitSettingsButtons();
    }

    // Создание гейммененджера для отображения текущего уровня
    private void InitGameManagerLevel()
    {
        createdGameManager = Instantiate(prefabForGameManager);
        createdGameManager.transform.SetSiblingIndex(3);
        var levelNumber = DataManager.Instance.GetLastAvailableLevelNumber();
        var subLevelNumbers = DataManager.Instance.dataForLevelGrouper[levelNumber];
        createdGameManager.GetComponent<GameManager>().CreateLevelForMainMenu(subLevelNumbers);
    }

    private void InitSettingsButtons()
    {
        bool vibrationState = DataManager.Instance.GetVibrationState();
        buttonVibration.sprite = vibrationState ? spriteForButtonEnabled : spriteForButtonDisabled;
        bool soundState = DataManager.Instance.GetSoundState();
        buttonSound.sprite = soundState ? spriteForButtonEnabled : spriteForButtonDisabled;
    }

    public void OnClickButtonsStart()
    {
        var levelNumber = DataManager.Instance.GetLastAvailableLevelNumber();
        if (DataManager.Instance.dataForLevelGrouper.Length > levelNumber)
        {
            var energyCount = DataManager.Instance.GetEnergyCount();
            if (energyCount == 0)
            {
                AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.SHOW_ENERGY_SCREEN);
                VibrationManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Failure);

                WindowsManager.Instance.FromMainToGetEnergyScreen(gameObject);
            }
            else
            {
                buttonStart.interactable = false;
                AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MAIN_SCREEN_START);
                VibrationManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.SoftImpact);
                DataManager.Instance.Set_EnergyCount(--energyCount);
                WindowsManager.Instance.FromMainScreenToGame(gameObject, createdGameManager);
            }
        }
        else
        {
            WindowsManager.Instance.CreateAllLevelsNoticeScreen();
        }
    }

    public void OnClickButtonToSettingsScreen()
    {
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS);
        WindowsManager.Instance.FromMainToSettingsScreen();
    }

    public void OnClickButtonToLevelsMode()
    {
        if (pickedModeId != 0)
        {
            AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, 1.6f);

            pickedModeId = 0;
            buttonRelaxMode.GetComponent<Image>().sprite = spriteForRelaxlModeButtonDisabled;
            textRelaxMode.color = colorForTextChangeModeButtonsWhenDisabled;

            buttonLevelsMode.GetComponent<Image>().sprite = spriteForLevelModeButtonEnabled;
            textLevelsMode.color = Color.white;

            contForSubLevelsInfo.SetActive(true);
            textForLevelNumber.gameObject.SetActive(true);
            textSoon.SetActive(false);
        }
    }

    public void OnClickButtonToRelaxMode()
    {
        if (pickedModeId != 1)
        {
            AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, 1.5f);

            pickedModeId = 1;
            buttonLevelsMode.GetComponent<Image>().sprite = spriteForLevelModeButtonDisabled;
            textLevelsMode.color = colorForTextChangeModeButtonsWhenDisabled;

            buttonRelaxMode.GetComponent<Image>().sprite = spriteForRelaxModeButtonEnabled;
            textRelaxMode.color = Color.white;

            contForSubLevelsInfo.SetActive(false);
            textForLevelNumber.gameObject.SetActive(false);
            textSoon.SetActive(true);
        }
        //todo добавить логику от которой меняеться поведение кнопки старт
    }

    public void OnClickButtonSound()
    {
        bool state = DataManager.Instance.GetSoundState();
        float pitch = state ? 1.1f : 1.4f;
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, pitch);
        // Инвентирую значение
        buttonSound.sprite = !state ? spriteForButtonEnabled : spriteForButtonDisabled;
        DataManager.Instance.SetSoundState(!state);
    }

    public void OnClickButtonVibarate()
    {
        bool state = DataManager.Instance.GetVibrationState();
        float pitch = state ? 1.1f : 1.4f;
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, pitch);
        buttonVibration.sprite = !state ? spriteForButtonEnabled : spriteForButtonDisabled;
        DataManager.Instance.SetVibrationState(!state);
    }
}
