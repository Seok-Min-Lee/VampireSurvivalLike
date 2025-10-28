using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    [SerializeField] GameObject homeWindow;
    [SerializeField] GameObject characterWindow;

    [SerializeField] private Image[] titleParts;
    private void Start()
    {
        homeWindow.SetActive(true);
        characterWindow.SetActive(false);
    }
    public void OnClickPlay()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);

        homeWindow.SetActive(false);
        characterWindow.SetActive(true);
    }
    public void OnClickExit()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);
        Application.Quit();
    }
    public void OnClickBack()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);

        homeWindow.SetActive(true);
        characterWindow.SetActive(false);
    }
    public void OnClickCharacter(int num)
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);

        StaticValues.playerCharacterNum = num;

        UnityEngine.SceneManagement.SceneManager.LoadScene("02_Game");
    }
}
