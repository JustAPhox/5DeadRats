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
        var parentMenu = GameObject.Find("Player Join Menus");


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


    }




}
