using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float FPS = 4;

    private Image image;
    private float timer;
    private int frameNumber = 0;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > FPS / 60)
        {
            timer = 0;
            frameNumber++;

            if (frameNumber > 23)
            {
                frameNumber = 0;
            }

            image.sprite = frames[frameNumber];
        }
    }
}
