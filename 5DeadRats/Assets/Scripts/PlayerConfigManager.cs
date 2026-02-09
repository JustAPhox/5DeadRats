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

    [SerializeField]
    private int minPlayers = 2;

    [SerializeField]
    private PlayerInputManager inputManager;

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


    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigList;
    }


    public void setPlayerCharacter(int index, int character)
    {
        playerConfigList[index].playerCharacter = character;
        Debug.Log($"Player {index} changed to character {character}");
    }

    public void playerReady(int index)
    {
        playerConfigList[index].playerReady = true;
        Debug.Log($"Player {index} readied up");


        if (playerConfigList.Count >= minPlayers && (playerConfigList.All(player => player.playerReady == true)) && (playerConfigList.All(player => player.playerCharacter != 0)))
        {
            // Start Game
            // For now just shows text saying ready
            Debug.Log("Game should start but won't yet");
            inputManager.DisableJoining();
            readyText.SetActive(true);
        }

    }


    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log($"Player joined {playerInput.playerIndex}");

        if (!playerConfigList.Any(player => player.playerIndex == playerInput.playerIndex))
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
