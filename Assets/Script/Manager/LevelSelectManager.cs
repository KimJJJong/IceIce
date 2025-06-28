using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonPrefab;  // ��ư ������
    [SerializeField] private Transform gridParent;          // ��ư�� ���� �θ� (GridLayoutGroup �پ��ִ� ������Ʈ)
    [SerializeField] private int totalLevels = 15;          // �� ���� ����
   //  [SerializeField] private Sprite[] starSprites;          // �� �̹��� (0~3�� ����)

    private List<GameObject> levelButtons = new();

    private void Start()
    {
        GenerateLevelButtons();
    }

    private void GenerateLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject buttonObj = Instantiate(levelButtonPrefab, gridParent);
            buttonObj.name = $"Level_{i}_Button";
            LevelButton button = buttonObj.GetComponent<LevelButton>();

            int stars = PlayerPrefs.GetInt($"Level_{i}_Stars", 0); // ����� �� ���� (0~3)
            button.Setup(i/*, stars, starSprites[stars]*/);
            int tmpInt = i;
            button.GetComponent<Button>().onClick.AddListener(() => OnLevelSelected(tmpInt));
            levelButtons.Add(buttonObj);
        }
    }

    private void OnLevelSelected(int levelIndex)
    {
        Debug.Log($"���� {levelIndex} ���õ�");
        SceneManager.LoadScene($"Level_{levelIndex}"); // �� �̸� ��Ģ ����: "Level_1", "Level_2"...
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }
}
