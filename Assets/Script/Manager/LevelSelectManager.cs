using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonPrefab;  // 버튼 프리팹
    [SerializeField] private Transform gridParent;          // 버튼을 넣을 부모 (GridLayoutGroup 붙어있는 오브젝트)
    [SerializeField] private int totalLevels = 15;          // 총 레벨 개수
   //  [SerializeField] private Sprite[] starSprites;          // 별 이미지 (0~3개 상태)

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

            int stars = PlayerPrefs.GetInt($"Level_{i}_Stars", 0); // 저장된 별 개수 (0~3)
            button.Setup(i/*, stars, starSprites[stars]*/);
            int tmpInt = i;
            button.GetComponent<Button>().onClick.AddListener(() => OnLevelSelected(tmpInt));
            levelButtons.Add(buttonObj);
        }
    }

    private void OnLevelSelected(int levelIndex)
    {
        Debug.Log($"레벨 {levelIndex} 선택됨");
        SceneManager.LoadScene($"Level_{levelIndex}"); // 씬 이름 규칙 예시: "Level_1", "Level_2"...
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }
}
