using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContinueFromMazeButtonScript : MonoBehaviour
{
    private Button My_Button;
    void Start()
    {
        My_Button = GetComponent<Button>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            My_Button.onClick.Invoke();
        }
    }

    public void Play_Quiz(InputAction.CallbackContext context)
    {
        My_Button.onClick.Invoke();
    }
}
