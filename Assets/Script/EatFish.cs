using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 고기에 닿음!");
            HandleMeatTrigger();
        }
    }

    private void HandleMeatTrigger()
    {
       // UIManager.Instance.ShowGameOverClear_Panel();
        TimeManager.Instance.EndGameClear();
        AudioManager.Instance.PlaySFX("SFX_EatFish");
    }
}
