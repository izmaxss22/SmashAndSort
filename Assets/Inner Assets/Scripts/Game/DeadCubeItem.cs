using UnityEditor;
using UnityEngine;

public class DeadCubeItem : MonoBehaviour
{
    private string playerName = "player";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == playerName)
            GameManager.Instance.OnCollisionWithDeadItem();
    }
}
