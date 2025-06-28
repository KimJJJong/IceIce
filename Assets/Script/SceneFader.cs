using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;  // 전체화면을 덮는 검은 이미지
    [SerializeField] private float fadeDuration = 3f;

    void Start()
    {
       fadeImage.gameObject.SetActive( true );

        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {

        float time = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        if (SceneManager.GetActiveScene().name is not "InitScene")
        fadeImage.gameObject.SetActive(false);

    }

    private IEnumerator FadeOut(string sceneName)
    {
        float time = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
       // fadeImage.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}
