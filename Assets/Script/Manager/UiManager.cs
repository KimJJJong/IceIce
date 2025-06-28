using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject startPanel; // In Start Scene
    [SerializeField] private GameObject hudPanel;   // May be in Page Scene
    [SerializeField] private GameObject gameOverPanel; // with inGame Scene


    [Header("HUD Elements")]       // inGame Ui
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
//    [SerializeField] private GameObject floatingTextPrefab;

    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI finalScoreText;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        ShowStartMenu();
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_Lobby");
    }


    #region 시작 화면 & 설정

    public void ShowStartMenu()
    {
        startPanel.SetActive(true);
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }



    public void OnClickStartButton()
    {
        //SceneManager.Instance.GoToPage();
    }



    #endregion

    #region HUD

    public void ShowHUD(bool show)
    {
        hudPanel.SetActive(show);
    }

    public void UpdateTime(float time)
    {
        timeText.text = $"Time: {Mathf.CeilToInt(time)}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }


/*    public void ShowFloatingText(Vector3 worldPos, int amount)
    {
        if (floatingTextPrefab == null) return;

        GameObject obj = Instantiate(floatingTextPrefab, worldPos, Quaternion.Euler(0, 180, 0));
        obj.GetComponentInChildren<TextMeshProUGUI>().text = $"+{amount}";
        Destroy(obj, 1.5f); // 자동 제거
    }
*/
    #endregion

    #region 게임 오버

    public void ShowGameOverPanel(int finalScore)
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Final Score: {finalScore}";
    }

    public void OnClickRestartButton()
    {
        ShowStartMenu();  // restart
        AudioManager.Instance.PlayBGM("BGM_Lobby");
    }
    public void OnClickGoToPageButton()
    {
        //SceneManager.Instacne.GoToPage();
    }

    #endregion
}
