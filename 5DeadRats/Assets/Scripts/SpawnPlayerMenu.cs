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
        // Finds what menu to tell it joined
        GameObject joinedMenu = GameObject.Find("Player Joined Menus");


        if (joinedMenu != null)
        {
            joinedMenu.GetComponent<CharacterSelectionMenu>().playerJoined(input);
        }
        else
        {
            Debug.Log("SpawnPlayerMenu Couldn't find Player Joined Menu");
        }



    }




}
