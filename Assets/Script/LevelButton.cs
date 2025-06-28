using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image numberImage;
    [SerializeField] private Sprite[] numberSprites;  // 0~9 숫자 스프라이트

    public void Setup(int levelNumber)
    {
        if (levelNumber >= 0 && levelNumber <= numberSprites.Length)
        {
            numberImage.sprite = numberSprites[levelNumber-1];
        }
    }
}
