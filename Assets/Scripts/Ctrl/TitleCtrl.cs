using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    [SerializeField] private Image[] titleParts;

    public void OnClickPlay()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);

        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Select");
        Debug.Log("OnClickPlay");
    }
    public void OnClickExit()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);

        Debug.Log("OnClickExit");
        Application.Quit();
    }
}
