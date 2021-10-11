using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    private DataManager DataManager;

    public Image buttonVibration;
    public Image buttonSound;

    public Sprite spriteForButtonDisabled;
    public Sprite spriteForButtonEnabled;

    // Start is called before the first frame update
    void Start()
    {
        DataManager = DataManager.Instance;
        InitButtons();
    }

    private void InitButtons()
    {
        bool vibrationState = DataManager.GetVibrationState();
        buttonVibration.sprite = vibrationState ? spriteForButtonEnabled : spriteForButtonDisabled;
        bool soundState = DataManager.GetSoundState();
        buttonSound.sprite = soundState ? spriteForButtonEnabled : spriteForButtonDisabled;
    }

    public void OnClickButtonSound()
    {
        bool state = DataManager.GetSoundState();
        float pitch = state ? 1.1f : 1.4f;
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, pitch);
        // Инвентирую значение
        buttonSound.sprite = !state ? spriteForButtonEnabled : spriteForButtonDisabled;
        DataManager.SetSoundState(!state);
    }

    public void OnClickButtonVibarate()
    {
        bool state = DataManager.GetVibrationState();
        float pitch = state ? 1.1f : 1.4f;
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, pitch);
        buttonVibration.sprite = !state ? spriteForButtonEnabled : spriteForButtonDisabled;
        DataManager.SetVibrationState(!state);
    }

    public void OnClickButtonRestorePurchases()
    {
        //todo
    }

    public void OnClickButtonClose()
    {
        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.MOST_UI_BUTTONS, 1.6f);
        StartCoroutine(CloseScreen());
    }

    private IEnumerator CloseScreen()
    {
        gameObject.GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
