using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_Lobby");
    }


    public void OnClickStartGame()
    {
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
        SceneManager.LoadScene("PageSelectScene");
    }
}
