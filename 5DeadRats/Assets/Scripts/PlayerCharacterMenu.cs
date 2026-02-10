using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI joinedText;

    [SerializeField]
    private GameObject selectionMenu;

    [SerializeField]
    private GameObject readyText;

    private PlayerInput playerInput;

    private PlayerControls controls;


    private void Awake()
    {
        controls = new PlayerControls();

    }





    public void PlayerJoined(PlayerInput input)
    {
        joinedText.SetText("Yes PLayer :D");

        playerInput = input;

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void PlayerInput_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {

            // Checks if action performed is the same as a cirtain action and if so does that function.
            if (obj.action.name == controls.Menu.Confirm.name)
            {
                selectionMenu.GetComponent<CharacterSelectionMenu>().playerReady(playerInput.playerIndex);
                readyText.SetActive(true);
            }

        }
    }
}
