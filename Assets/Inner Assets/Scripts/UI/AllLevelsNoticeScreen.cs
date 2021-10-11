using System.Collections;
using UnityEngine;

public class AllLevelsNoticeScreen : MonoBehaviour
{
    public void OnClickCloseButton()
    {
        StartCoroutine(CloseScreen());
    }

    private IEnumerator CloseScreen()
    {
        GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        yield break;
    }
}
