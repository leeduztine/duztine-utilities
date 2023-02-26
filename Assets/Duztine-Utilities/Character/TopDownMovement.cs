using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2D TOP DOWN CHARACTER MOVEMENT
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveDir;

    private void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir.Normalize();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }
}
