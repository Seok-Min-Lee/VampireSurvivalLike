using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameCtrl : MonoBehaviour
{
    public static GameCtrl Instance;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private TextMeshProUGUI timeScoreText;
    [SerializeField] private TextMeshProUGUI killScoreText;

    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject rewardWindow;
    [SerializeField] private GameObject scoreWindow;

    [SerializeField] private RewardButton[] rewardButtons;
    [SerializeField] private RewardInfo[] rewardInfoes;

    private float timer = 0f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        timeText.text = "00:00";
        Debug.Log(AudioManager.Instance == null);
        AudioManager.Instance.Load(() => 
        {
            AudioManager.Instance.Init(0.5f, 0.5f);
            AudioManager.Instance.PlayBGM(SoundKey.BGM); 
        });

#if UNITY_EDITOR
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
#endif
    }
    private void Update()
    {
        timer += Time.deltaTime;

        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
    public void OnClickPause()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);
        Time.timeScale = 0f;
        pauseWindow.SetActive(true);
    }
    public void OnClickResume()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);
        pauseWindow.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnClickHome()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameTouch);
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Title");
    }
    public void OnLevelUp()
    {
        Time.timeScale = 0f;
        
        List<RewardInfo> randoms = Utils.Shuffle<RewardInfo>(rewardInfoes);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            rewardButtons[i].Init(randoms[i]);
        }

        rewardWindow.SetActive(true);
    }
    public void OnClickReward()
    {
        rewardWindow.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnGameEnd()
    {
        AudioManager.Instance.PlaySFX(SoundKey.GameEnd);
        Time.timeScale = 0f;

        float time = Time.time;
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);

        timeScoreText.text = $"생존 시간\n{minutes:00}:{seconds:00}";
        killScoreText.text = $"처치 수\n {Player.Instance.killCount.ToString("#,##0")}";

        scoreWindow.SetActive(true);
    }
}
