using System;
using UnityEngine;

public class AutoSizeCamera: MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector2 defaultScreen = new Vector2(1920, 1080);

    private void Start()
    {
        float defaultRatio = (float) defaultScreen.y / defaultScreen.x;
        float currentRatio = (float) Screen.height / Screen.height;
        float rate = currentRatio / defaultRatio;
        mainCamera.orthographicSize *= rate;
        mainCamera.fieldOfView *= rate;
    }
}