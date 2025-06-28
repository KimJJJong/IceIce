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
    [SerializeField] private GameObject gameOverPanel; 


    [Header("HUD Elements")]       // inGame Ui
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
//    [SerializeField] private GameObject floatingTextPrefab;

    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI finalScoreText;

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
        gameOverPanel.SetActive(false);
        
        dragManager.SetActive(true);
    }



    public void OnClickStartButton()
    {
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");

        puzzlePagePanel.SetActive(false);
        movePagePanel.SetActive(true);
        gameOverPanel.SetActive(false);

        dragManager.SetActive(false);

        TimeManager.Instance.StartGame();
    }



    #endregion

    #region HUD



    public void UpdateTime(float time)
    {
        timeText.text = $"Time: {Mathf.CeilToInt(time)}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }



    #endregion

    #region 게임 오버

    public void ShowGameOverPanel(float finalScore)
    {
        movePagePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Final Score: {finalScore.ToString("F1")}";
    }

    public void OnClickRestartButton()
    {
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickSelectLevel()
    {
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
        AudioManager.Instance.PlayBGM("BGM_Lobby");

        SceneManager.LoadScene("PageSelectScene");

    }

    #endregion
}
