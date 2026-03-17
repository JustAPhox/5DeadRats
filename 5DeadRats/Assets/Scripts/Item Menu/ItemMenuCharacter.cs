using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemMenuCharacter : MonoBehaviour
{
    private PlayerConfig playerConfig;

    private PlayerControls controls;

    public GameObject itemLogic;


    private void Awake()
    {
        // [IMPORTANT] Gets the controlls and changes the action map
        controls = new PlayerControls();

    }


    public void initialisePlayer(PlayerConfig config)
    {
        playerConfig = config;

        // [IMPORTANT] Adds a way to detect the C# events the players are creating.
        playerConfig.playerInput.onActionTriggered += PlayerInput_onActionTriggered;

        playerConfig.playerInput.SwitchCurrentActionMap("Items");




}

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.actionMap.name != "Items") { return; }

        // Checks if the action was specifically performed (and not one of 2 other stages that can also fire and mess things up. Def needed for things you want triggered once. Don't know about movement.)
        if (obj.performed)
        {
            if (obj.action.name == controls.Items.Pause.name)
            {
                itemLogic.GetComponent<ItemLogic>().togglePause();
            }

            if (Time.timeScale == 0)
            {
                if (obj.action.name == controls.Items.BuyItem.name)
                {
                    itemLogic.GetComponent<ItemLogic>().pauseSelect();
                }
                else if (obj.action.name == controls.Items.PauseUp.name)
                {
                    itemLogic.GetComponent<ItemLogic>().pauseChange();
                }
                else if (obj.action.name == controls.Items.PauseDown.name)
                {
                    itemLogic.GetComponent<ItemLogic>().pauseChange();
                }
            }
            else
            {
                // Checks if action performed is the same as a cirtain action and if so does that function.
                if (obj.action.name == controls.Items.BuyItem.name)
                {
                    selectItem();
                }
                else if (obj.action.name == controls.Items.ItemLeft.name)
                {
                    changeSelectedItem(-1);
                }
                else if (obj.action.name == controls.Items.ItemRight.name)
                {
                    changeSelectedItem(1);
                }
            }
        }
    }




    private void selectItem()
    {
        itemLogic.GetComponent<ItemLogic>().buyItem(playerConfig.playerIndex);
    }

    private void changeSelectedItem(int change)
    {
        itemLogic.GetComponent<ItemLogic>().selectItem(playerConfig.playerIndex, change);
    }
}
