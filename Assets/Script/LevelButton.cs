using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image numberImage;
    [SerializeField] private Sprite[] numberSprites;  // 0~9 ���� ��������Ʈ

    public void Setup(int levelNumber)
    {
        if (levelNumber >= 0 && levelNumber <= numberSprites.Length)
        {
            numberImage.sprite = numberSprites[levelNumber-1];
        }
    }
}
