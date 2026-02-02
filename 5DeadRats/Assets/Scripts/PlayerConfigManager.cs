using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfig> playerConfigList;

    private int minPlayers = 2;
    private int maxPlayers = 4;


    public GameObject readyText;


    public static PlayerConfigManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Tried to make second player config manager");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigList = new List<PlayerConfig>();
        }
    }


    public void setPlayerCharacter(int index, int character)
    {
        playerConfigList[index].playerCharacter = character;
    }

    public void playerReady(int index)
    {
        playerConfigList[index].playerReady = true;

        if(playerConfigList.Count >= minPlayers && playerConfigList.All(player => player.playerReady == true))
        {
            // Start Game
            // For now just shows text saying ready
            Debug.Log("Game should start but won't yet");
            readyText.SetActive(true);
        }

    }


    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log($"Player joined {playerInput.playerIndex}");
        if(!playerConfigList.Any(player => player.playerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            playerConfigList.Add(new PlayerConfig(playerInput));
        }
    }



}


public class PlayerConfig
{
    public PlayerConfig(PlayerInput input)
    {
        playerIndex = input.playerIndex;
        playerInput = input;
    }



    public PlayerInput playerInput;
    public int playerIndex;
    public bool playerReady;
    public int playerCharacter;

}
