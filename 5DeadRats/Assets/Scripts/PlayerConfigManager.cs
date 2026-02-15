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

    public static PlayerConfigManager instance;

    [SerializeField]
    private GameObject menuLogic;


    private void Awake()
    {
        // Forces there to only be one of the manager.
        if (instance != null)
        {
            Debug.Log("Tried to make second player config manager");
        }
        else
        {
            // Sets self to not be deleted
            instance = this;
            DontDestroyOnLoad(instance);
            // Creates a list for players to be added to
            playerConfigList = new List<PlayerConfig>();
        }
    }

    // Gives the player list when asked
    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigList;
    }

    // Takes an index and character and changes that character when asked
    public void setPlayerCharacter(int index, int character)
    {
        playerConfigList[index].playerCharacter = character;
        Debug.Log($"Player {index} changed to character {character}");
    }



    public void setPlayerBuffed(int index, bool buffed)
    {
        playerConfigList[index].playerBuffed = buffed;
        Debug.Log($"Player {index} buffed: {buffed}");
    }

    public void allowJoining(bool joining)
    {
        if (joining)
        {
            inputManager.EnableJoining();
        }
        else
        {
            inputManager.DisableJoining();
        }
    }


    // Readys up a given plater
    public void playerReady(int index)
    {
        playerConfigList[index].playerReady = true;
        Debug.Log($"Player {index} readied up");


        // If there is the min number of players
        // And they are all now ready
        // And they all have a character
        if (playerConfigList.Count >= minPlayers && (playerConfigList.All(player => player.playerReady == true)) && (playerConfigList.All(player => player.playerCharacter != 0)))
        {
            // Start Game
            // For now just shows text saying ready
            // Also stops joining so I'd recommend doing this while testing
            Debug.Log("Game should start but won't yet");



            inputManager.DisableJoining();

            menuLogic.GetComponent<MenuLogic>().charactersChoosen();
        }

    }


    // Adds player to the list when they join
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


// Stores everything about a player
public class PlayerConfig
{
    // When created is given a player input which it stores + index
    public PlayerConfig(PlayerInput input)
    {
        playerIndex = input.playerIndex;
        playerInput = input;
    }



    public PlayerInput playerInput;
    public int playerIndex;
    public bool playerReady;
    public int playerCharacter;

    public bool playerBuffed;

}
