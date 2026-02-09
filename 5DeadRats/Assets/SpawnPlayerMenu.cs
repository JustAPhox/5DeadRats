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
        var parentMenu = GameObject.Find("Player Join Menus");

        if (parentMenu != null)
        {
            GameObject menu = Instantiate(playerSetupMenu, parentMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetUpMenuControl>().SetPlayerIndex(input.playerIndex);
        }


    }




}
