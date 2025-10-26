using UnityEngine;
using UnityEngine.UI;

public class SelectCtrl : MonoBehaviour
{
    public void OnClickCharacter(int num)
    {
        StaticValues.playerCharacterNum = num;

        UnityEngine.SceneManagement.SceneManager.LoadScene("02_Game");
    }
}
