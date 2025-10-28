using UnityEngine;
using UnityEngine.UI;

public class SelectCtrl : MonoBehaviour
{
    public void OnClickCharacter(int num)
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);

        StaticValues.playerCharacterNum = num;

        UnityEngine.SceneManagement.SceneManager.LoadScene("02_Game");
    }
}
