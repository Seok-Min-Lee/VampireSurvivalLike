using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    [SerializeField] private Image[] titleParts;

    public void OnClickPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Select");
        Debug.Log("OnClickPlay");
    }
    public void OnClickExit()
    {
        Debug.Log("OnClickExit");
        Application.Quit();
    }
}
