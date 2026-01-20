using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazePlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 Movement_Input;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movement = Movement_Input * Speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(270f, transform.rotation.eulerAngles.y, 0f);
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        Movement_Input = ctx.ReadValue<Vector2>();
    }
}
