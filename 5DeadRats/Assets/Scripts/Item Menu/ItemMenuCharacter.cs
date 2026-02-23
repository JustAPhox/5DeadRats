using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemMenuCharacter : MonoBehaviour
{
    private PlayerConfig playerConfig;

    private PlayerControls controls;

    private int playerCharacter;

    public GameObject itemLogic;


    private void Awake()
    {
        // [IMPORTANT] Gets the controlls and changes the action map
        controls = new PlayerControls();

    }


    public void initialisePlayer(PlayerConfig config)
    {
        playerConfig = config;

        playerCharacter = config.playerCharacter;

        // [IMPORTANT] Adds a way to detect the C# events the players are creating.
        playerConfig.playerInput.onActionTriggered += PlayerInput_onActionTriggered;

        playerConfig.playerInput.SwitchCurrentActionMap("Items");




}

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        // Checks if the action was specifically performed (and not one of 2 other stages that can also fire and mess things up. Def needed for things you want triggered once. Don't know about movement.)
        if (obj.performed)
        {
            // Checks if action performed is the same as a cirtain action and if so does that function.
            if (obj.action.name == controls.Items.Confirm.name)
            {
                selectItem();
            }
            else if (obj.action.name == controls.Items.Left.name)
            {
                changeSelectedItem(-1);
            }
            else if (obj.action.name == controls.Items.Right.name)
            {
                changeSelectedItem(1);
            }
        }
    }




    private void selectItem()
    {
        Debug.Log($"Player {playerConfig.playerIndex} confirms");
    }

    private void changeSelectedItem(int change)
    {
        Debug.Log($"Player {playerConfig.playerIndex} changes item in direction {change}");
    }
}
