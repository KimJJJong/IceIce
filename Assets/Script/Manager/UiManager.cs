using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject puzzlePagePanel; 
    [SerializeField] private GameObject movePagePanel;   
    [SerializeField] private GameObject gameOverClear_Panel;
    [SerializeField] private GameObject gameOverFail_Panel;


    [Header("HUD Elements")]       // inGame Ui
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
//    [SerializeField] private GameObject floatingTextPrefab;

    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI ClearScoreText;
    [SerializeField] private TextMeshProUGUI FailScoreText;

    [Header("Drag Manager")]
    [SerializeField] private GameObject dragManager;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        EnterPuzzle();
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_InGame");
    }


    #region 시작 화면 & 설정

    public void EnterPuzzle()
    {
        puzzlePagePanel.SetActive(true);
        movePagePanel.SetActive(false);
        gameOverClear_Panel.SetActive(false);
        gameOverFail_Panel.SetActive(false);

        dragManager.SetActive(true);
    }



    public void OnClickStartButton()
    {

        puzzlePagePanel.SetActive(false);
        movePagePanel.SetActive(true);
        gameOverClear_Panel.SetActive(false);
        gameOverFail_Panel.SetActive(false);

        dragManager.SetActive(false);

        TimeManager.Instance.StartGame();

        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }



    #endregion

    #region HUD



    public void UpdateTime(float time)
    {
        timeText.text = $"{Mathf.CeilToInt(time)}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $" {score}";
    }



    #endregion

    #region 게임 오버

    public void ShowGameOverFail_Panel(float finalScore)
    {
        movePagePanel.SetActive(false);

        gameOverFail_Panel.SetActive(true);

        FailScoreText.text = $"Final Score: {finalScore.ToString("F1")}";

        AudioManager.Instance.PlaySFX("SFX_Fail");
    }

    public void ShowGameOverClear_Panel(float finalScore)
    {

        movePagePanel.SetActive(false);

        gameOverClear_Panel.SetActive(true);

        ClearScoreText.text = $"Final Score: {finalScore.ToString("F1")}";
     
        AudioManager.Instance.PlaySFX("SFX_Clear");
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }

    public void OnClickSelectLevel()
    {
        SceneManager.LoadScene("PageSelectScene");

        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
        AudioManager.Instance.PlayBGM("BGM_Lobby");

    }

    #endregion
}
