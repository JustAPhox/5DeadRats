using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemLogic : MonoBehaviour
{

    private int playerCount;


    [SerializeField]
    private GameObject playerPreFab;

    private GameObject[] playersObjects;



    // Start is called before the first frame update
    void Start()
    {
        SetUpPlayers();
        tempBadRewardGiver();
        moveToMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void SetUpPlayers()
    {
        // [IMPORTANT] Gets an array of all the players
        PlayerConfig[] playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();

        // Gets the number of players
        playerCount = playerConfigs.Length;

        playersObjects = new GameObject[playerCount];

        // For each player
        //for (int i = 0; i < playerConfigs.Length; i++)
        //{
        // Create a player object to be controlled
        //GameObject player = Instantiate(playerPreFab);

        // [IMPORTANT] Give them a player config to initialise with 
        //player.GetComponent<QuizCharacterScript>().initialisePlayer(playerConfigs[i]);

        //playersObjects[i] = player;
        //}
    }


    private void tempBadRewardGiver()
    {
        PlayerConfig[] playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();

        int[] playerScores = new int[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            playerScores[i] = playerConfigs[i].quizScore;
        }


        int highestPoints = 0;




        // Finds heighest score
        for (int i = 0; i < playerCount; i++)
        {
            if (playerScores[i] > highestPoints)
            {
                highestPoints = playerScores[i];
            }
        }

        // All players with best score win
        for (int i = 0; i < playerCount; i++)
        {
            if (playerScores[i] == highestPoints)
            {
                PlayerConfigManager.instance.GetComponent<PlayerConfigManager>().setPlayerBuffed(i, true);
            }
            else
            {
                PlayerConfigManager.instance.GetComponent<PlayerConfigManager>().setPlayerBuffed(i, false);
            }
        }


    }




    private void moveToMaze()
    {
        Debug.Log($"Moving to maze Scene");

        SceneManager.LoadScene("2D Maze Scene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(3);
    }

}