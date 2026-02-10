using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSetupMenu;

    [SerializeField]
    private PlayerInput input;

    private void Awake()
    {
        // Finds where the menu should go
        GameObject parentMenu = GameObject.Find("Player Join Menus");
        GameObject joinedMenu = GameObject.Find("Player Joined Menus");



        if (parentMenu != null)
        {
            //Creates player set up menu and sets its index
            GameObject menu = Instantiate(playerSetupMenu, parentMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetUpMenuControl>().SetPlayerIndex(input.playerIndex);
        }
        else
        {
            Debug.Log("SpawnPlayerMenu Couldn't find Player Join Menus");
        }


        if (joinedMenu != null)
        {
            joinedMenu.GetComponent<CharacterSelectionMenu>().playerJoined(input.playerIndex);
        }
        else
        {
            Debug.Log("SpawnPlayerMenu Couldn't find Player Joined Menu");
        }



    }




}
