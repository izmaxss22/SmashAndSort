using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private DataManager DataManager;

    public enum AudioSourcesIds
    {
        MOST_UI_BUTTONS,
        GET_ENERGY_SCREEN_CLOSE_SCREEN,
        GET_ENERGY_SCREEN_BUTTON_GET,
        GET_ENERGY_SCREEN_AFFTER_AD_WATCH,
        GAME_COLLECT_COLOR_ITEM,
        GAME_COMPLETE_LEVEL,
        GAME_GAME_OVER,
        GAME_COLLECT_ALL_COLORS,
        GAME_COLLECT_COIN,
        GAME_DESTROY_COIN,
        MAIN_SCREEN_START,
        SHOW_ENERGY_SCREEN
    }

    public List<AudioSourceItem> addedAudioSources = new List<AudioSourceItem>();

    [Serializable]
    public class AudioSourceItem
    {
        public AudioSourcesIds id;
        public AudioSource audioSource;
    }

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private Dictionary<AudioSourcesIds, AudioSource> audioSources = new Dictionary<AudioSourcesIds, AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        DataManager = DataManager.Instance;
        foreach (var item in addedAudioSources)
        {
            audioSources.Add(item.id, item.audioSource);
        }
    }

    // Простое воспроизведение с питчем 1
    public void PlayAudioSource(AudioSourcesIds audioId)
    {
        if (CheckAudioCanPlay())
        {
            var aSource = audioSources[audioId];
            aSource.pitch = 1;
            aSource.Play();
        }
    }

    // Воспроизводит с указаным питчем
    public void PlayAudioSource(AudioSourcesIds audioId, float pitch)
    {
        if (CheckAudioCanPlay())
        {
            AudioSource audioSource = audioSources[audioId];
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }

    // Воспроизводит айдимо в различным питчем (указанным в range)
    public void PlayAudioSourceWithSideToSidePitch(AudioSourcesIds audioId, float basePitch, float rangeForRandom)
    {
        if (CheckAudioCanPlay())
        {
            AudioSource audioSource = audioSources[audioId];
            audioSource.pitch = UnityEngine.Random.Range(basePitch - rangeForRandom, basePitch + rangeForRandom);
            audioSource.Play();
        }
    }

    private bool CheckAudioCanPlay()
    {
        return DataManager.GetSoundState();
    }
}