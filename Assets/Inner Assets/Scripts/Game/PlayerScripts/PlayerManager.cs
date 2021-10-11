using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject prefabForPlayer;
    private PlayerMovement PlayerMovement;
    private PlayerCollisionDetecter PlayerCollisionDetecter;

    public void CreatePlayer(GameManager.GameData gameData)
    {
        Vector3 playerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.3f));
        playerPos.y = 1;
        var go = Instantiate(prefabForPlayer, playerPos, Quaternion.identity, transform);
        go.name = "player";
        // todo Установить партиклы исходя из номера системы частиц (либо пока просто одни частицы а остальные в обнове)
        PlayerMovement = go.GetComponent<PlayerMovement>();
        PlayerMovement.Init(gameData);
        PlayerCollisionDetecter = go.GetComponent<PlayerCollisionDetecter>();
        PlayerCollisionDetecter.Init(gameData);
    }

    public void OnCollectAllOneColorItem(int nextColorId)
    {
        PlayerCollisionDetecter.OnCollectsAllOneColors(nextColorId);
    }

    public void OnChangeColor(int colorId)
    {
        PlayerCollisionDetecter.OnChangeColor(colorId);
    }

    public void DestroyPlayer()
    {
        Destroy(PlayerMovement.gameObject);
    }

    public void EnablePlayerMovement()
    {
        PlayerMovement.SetMovementState(true);
    }

    public void DisablePlayerMovement()
    {
        PlayerMovement.SetMovementState(false);
    }
}


//TODO
// Собрать цвета как бустер
// Подумать над механиками в плане разеба
// Элементы которые передвигаються разбивая арт