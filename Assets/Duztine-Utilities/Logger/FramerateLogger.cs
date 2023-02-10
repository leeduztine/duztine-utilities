using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramerateLogger : MonoBehaviour
{
    [SerializeField] private bool showFps;

    [SerializeField] private Text fpsTxt;
    [SerializeField] private Color goodFpsColor = new Color(0f, 1f, 0f);
    [SerializeField] private Color badFpsColor = new Color(1f, 0f, 0f);
    [SerializeField] private int goodFps = 60;
    
    private int frameCount = 0;
    private float nextTime = 0f;

    private void Update()
    {
        if (showFps)
        {
            frameCount++;

            if (Time.time > nextTime)
            {
                fpsTxt.color = (frameCount >= goodFps ? goodFpsColor : badFpsColor);

                fpsTxt.text = $"FPS: {frameCount}";
                frameCount = 0;
                nextTime = Time.time + 1f;
            }
        }
        else
        {
            fpsTxt.text = "";
        }
    }
}
