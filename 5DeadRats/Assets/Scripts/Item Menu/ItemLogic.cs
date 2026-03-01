using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemLogic : MonoBehaviour
{

    private int playerCount;


    [SerializeField]
    private GameObject playerPreFab;

    [SerializeField]
    private GameObject itemPreFab;

    [SerializeField]
    private GameObject itemSpace;


    private GameObject[] playersObjects;

    private int[] itemStatus;
    private GameObject[] itemObjects;
    private int selectedItemPos;

    private int boughtItemCount;



    // Start is called before the first frame update
    void Start()
    {
        SetUpPlayers();
        SetUpItems();

        tempBadRewardGiver();
        //moveToMaze();
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

        //For each player
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //Create a player object to be controlled
            GameObject player = Instantiate(playerPreFab);

            player.GetComponent<ItemMenuCharacter>().itemLogic = gameObject;

            player.transform.SetParent(gameObject.transform);



            //[IMPORTANT] Give them a player config to initialise with
            player.GetComponent<ItemMenuCharacter>().initialisePlayer(playerConfigs[i]);

            playersObjects[i] = player;
        }
    }


    private void SetUpItems()
    {
        itemStatus = new int[playerCount];
        itemObjects = new GameObject[playerCount];


        //For each player
        for (int i = 0; i < playerCount; i++)
        {
            //Create a player object to be controlled
            GameObject item = Instantiate(itemPreFab, itemSpace.transform);

            item.transform.SetParent(itemSpace.transform);

            itemObjects[i] = item;


            if (i == 0)
            {
                item.GetComponent<Image>().color = Color.green;
            }
        }
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


    public void buyItem(int playerIndex)
    {
        itemObjects[selectedItemPos].GetComponent<Image>().color = Color.red;
        itemStatus[selectedItemPos] = 1;

        boughtItemCount += 1;

        if (boughtItemCount != playerCount)
        {
            selectItem(playerIndex, 1);
        }
        else
        {
            moveToMaze();
        }


    }


    public void selectItem(int playerIndex, int change)
    {

        if (boughtItemCount == playerCount) { return; }


        if (itemStatus[selectedItemPos] == 0)
        {
            itemObjects[selectedItemPos].GetComponent<Image>().color = Color.white;
        }

        bool keepGoing = true;


        int attempts = 0;


        while (keepGoing)
        {
            attempts += 1;

            keepGoing = false;


            selectedItemPos += change;



            if (selectedItemPos < 0)
            {
                selectedItemPos = playerCount - 1;
            }
            else if (selectedItemPos > playerCount - 1)
            {
                selectedItemPos = 0;
            }

            if (itemStatus[selectedItemPos] == 1)
            {
                keepGoing = true;
            }

            if (attempts > playerCount * 2)
            {
                keepGoing = false;
            }

        }

        itemObjects[selectedItemPos].GetComponent<Image>().color = Color.green;


    }




    public void moveToMaze()
    {
        Debug.Log($"Moving to maze Scene");

        SceneManager.LoadScene("Maze1");
    }

}