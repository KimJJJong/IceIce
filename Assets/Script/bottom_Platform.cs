using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottom_Platform : MonoBehaviour
{
    // Start is called before the first frame update
    private float screenWidth;
    private float screenHeight;
    private float spriteWidth;
    private float spriteHeight;
    void Start()
    {
        // 스프라이트 크기 계산
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
