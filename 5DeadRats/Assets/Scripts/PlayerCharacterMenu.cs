using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCharacterMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI joinedText;

    [SerializeField]
    private GameObject selectionMenu;

    [SerializeField]
    private GameObject readyText;

    [SerializeField]
    private TextMeshProUGUI charcterText;


    [SerializeField]
    private Image ratImage;

    private PlayerInput playerInput;

    private PlayerControls controls;

    private int currentCharacter = 1;


    private string[] characterNames = { "Ruby Rockethorn", "Pablo Quescobar", "Winona", "John Moviestar", "Steven Cheddarverse" };

    [SerializeField]
    private Sprite[] spriteMouthOpen;

    [SerializeField]
    private Sprite[] spriteMouthClose;



    private void Awake()
    {
        controls = new PlayerControls();

    }





    public void PlayerJoined(PlayerInput input)
    {
        joinedText.SetText($"Player {input.playerIndex + 1}");

        playerInput = input;

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;

        charcterText.enabled = true;
        charcterText.SetText(characterNames[0]);
        ratImage.enabled = true;
    }


    public void stopInputs()
    {
        playerInput.onActionTriggered -= PlayerInput_onActionTriggered;
    }


    private void PlayerInput_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            // Checks if action performed is the same as a cirtain action and if so does that function.
            if (obj.action.name == controls.Menu.Confirm.name)
            {
                selectionMenu.GetComponent<CharacterSelectionMenu>().playerReady(playerInput.playerIndex, currentCharacter);
                readyText.SetActive(true);
            }
            // Checks if action performed is the same as a cirtain action and if so does that function.
            if (obj.action.name == controls.Menu.Left.name)
            {
                changeCharacter(-1);
            }
            if (obj.action.name == controls.Menu.Right.name)
            {
                changeCharacter(1);
            }

        }
    }




    private void changeCharacter(int direction)
    {
        if (readyText.activeSelf == true) { return; }

        currentCharacter += direction;
        if (currentCharacter == 0)
        {
            currentCharacter = 5;
        }
        else if (currentCharacter == 6)
        {
            currentCharacter = 1;
        }
        charcterText.SetText(characterNames[currentCharacter - 1]);
        ratImage.sprite = spriteMouthClose[currentCharacter - 1];
    }
}
