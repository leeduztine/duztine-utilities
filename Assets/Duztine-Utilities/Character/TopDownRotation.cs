using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2D TOP DOWN CHARACTER ROTATION
/// </summary>
public class TopDownRotation : MonoBehaviour
{
    [SerializeField] private float offsetAngle = 0f;
    
    private Camera cam;
    private Vector2 mousePos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        LookAtMousePosition();
    }

    private void LookAtMousePosition()
    {
        var selfPos = (Vector2)transform.position;
        var lookDir = mousePos - selfPos;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offsetAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
