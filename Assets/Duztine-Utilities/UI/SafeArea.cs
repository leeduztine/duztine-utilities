using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    [SerializeField] private List<RectTransform> excludedPanelList;
    private RectTransform panel;
    private Rect lastSafeArea = new Rect(0f, 0f, 0f, 0f);
    private ScreenOrientation lastScreenOrientation = ScreenOrientation.AutoRotation;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (lastSafeArea != Screen.safeArea || lastScreenOrientation != Screen.orientation)
        {
            ApplySafeArea(Screen.safeArea);
        }
    }

    private void ApplySafeArea(Rect safeArea)
    {
        lastSafeArea = safeArea;
        lastScreenOrientation = Screen.orientation;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMax.x /= Screen.width;

        panel.anchorMin = new Vector2(anchorMin.x, panel.anchorMin.y);
        panel.anchorMax = new Vector2(anchorMax.x, panel.anchorMax.y);
        
        ExcludeSafeArea(anchorMin.x / (anchorMin.x - 1f), 1f / anchorMax.x);
    }

    private void ExcludeSafeArea(float leftRatio, float rightRatio)
    {
        excludedPanelList?.ForEach(excludedPanel =>
        {
            excludedPanel.anchorMin = new Vector2(leftRatio, panel.anchorMin.y);
            excludedPanel.anchorMax = new Vector2(rightRatio, panel.anchorMax.y);
        });
    }
}