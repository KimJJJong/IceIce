using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("���� ����")]
    [SerializeField] private float gameDuration = 90f;
    
    private float remainingTime;
    private bool isPlaying = false;

    // �̺�Ʈ �ݹ�
    public System.Action OnGameStart;
    public System.Action OnGameEnd;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (!isPlaying) return;

        remainingTime -= Time.deltaTime;
        UIManager.Instance.UpdateTime(remainingTime);

        if (remainingTime <= 0f)
        {
            EndGame();
        }
    }

    /// <summary>
    /// ���� ���� ó�� from UIManager
    /// </summary>
    public void StartGame()
    {
        Debug.Log("[GameManager] ���� ����");
        
        remainingTime = gameDuration;
        isPlaying = true;

        UIManager.Instance.ShowHUD(true);
        UIManager.Instance.UpdateTime(remainingTime);
        
        ScoreManager.Instance.InitScore();

        OnGameStart?.Invoke();
    }

    /// <summary>
    /// ���� ���� ó��
    /// </summary>
    private void EndGame()
    {
        if (!isPlaying) return;

        Debug.Log("[GameManager] ���� ����");


        isPlaying = false;

        UIManager.Instance.ShowGameOverPanel(ScoreManager.Instance.CurrentScore);

        OnGameEnd?.Invoke();
    }

    /// <summary>
    /// �ܺο��� ���� ���� (ü�� 0 ��)
    /// </summary>
    public void ForceEndGame()
    {
        if (isPlaying)
        {
            EndGame();
        }
    }


    public bool IsGamePlaying => isPlaying;
    public float RemainingTime => remainingTime;
}
