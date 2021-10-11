using UnityEngine;

public class PlayerCollisionDetecter : MonoBehaviour
{
    private VibrationManager VibrationManager;
    private AudioManager AudioManager;
    // соответствует id цвета
    private int playerColorId;

    public void Init(GameManager.GameData gameData)
    {
        VibrationManager = VibrationManager.Instance;
        AudioManager = AudioManager.Instance;
        var numberId = gameData.subLevelInProgressNumber;
        playerColorId = gameData.levelDatas[numberId].usedColors[0].colorId;
        Color playerColor = GameManager.gameData.usedColorsById[playerColorId];
        GetComponent<MeshRenderer>().material.color = playerColor;
    }

    public void OnChangeColor(int colorId)
    {
        playerColorId = colorId;
        Color playerColor = GameManager.gameData.usedColorsById[colorId];
        GetComponent<MeshRenderer>().material.color = playerColor;
    }

    public void OnCollectsAllOneColors(int nextColorId)
    {
        playerColorId = nextColorId;
        Color playerColor = GameManager.gameData.usedColorsById[nextColorId];
        GetComponent<MeshRenderer>().material.color = playerColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collisionColorId = collision.gameObject.name;
        if (collisionColorId == playerColorId.ToString())
        {
            VibrationManager.Vibrate(MoreMountains.NiceVibrations.HapticTypes.SoftImpact);
            AudioManager.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_COLLECT_COLOR_ITEM);
            Destroy(collision.gameObject);
            GameManager.Instance.OnCollectColorItem(int.Parse(collisionColorId));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Если это монетка
        if (other.gameObject.name == "1")
        {
            VibrationManager.Vibrate(MoreMountains.NiceVibrations.HapticTypes.Success);
            AudioManager.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_COLLECT_COIN);

            Destroy(other.gameObject);
            GameManager.Instance.OnCollectCoinItem();
        }
    }
}
