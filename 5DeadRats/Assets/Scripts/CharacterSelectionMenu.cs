using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerCharacterMenu;

    [SerializeField]
    private GameObject playerConfigManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerJoined(PlayerInput input)
    {
        playerCharacterMenu[input.playerIndex].GetComponent<PlayerCharacterMenu>().PlayerJoined(input);
    }


    public void playerReady(int index)
    {
        playerConfigManager.GetComponent<PlayerConfigManager>().setPlayerCharacter(index, 1);
        playerConfigManager.GetComponent<PlayerConfigManager>().playerReady(index);
    }
}
