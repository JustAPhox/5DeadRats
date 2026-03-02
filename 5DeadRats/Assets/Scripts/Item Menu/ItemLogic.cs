using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [SerializeField]
    private TextMeshProUGUI currentPlayerText;

    private PlayerConfig[] playerConfigs;

    private GameObject[] playersObjects;

    private int[] itemStatus;
    private GameObject[] itemObjects;
    private int selectedItemPos;

    private int boughtItemCount;

    private int[] playerOrder;
    private int[] playerScores;

    



    // Start is called before the first frame update
    void Start()
    {
        SetUpPlayers();

        OrderPlayers();

        SetUpItems();

        //tempBadRewardGiver();
        //moveToMaze();
    }




    // Update is called once per frame
    void Update()
    {

    }



    private void SetUpPlayers()
    {
        // [IMPORTANT] Gets an array of all the players
        playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();

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



    private void OrderPlayers()
    {
        playerOrder = new int[playerCount];
        playerScores = new int[playerCount];

        for (int i = 0;i < playerCount; i++)
        {
            playerOrder[i] = i;
            playerScores[i] = playerConfigs[i].quizScore;
        }


        Array.Sort(playerScores, playerOrder);
        Array.Reverse(playerScores);
        Array.Reverse(playerOrder);

        Debug.Log($"Player Order: {string.Join(", ", playerOrder)}");
        Debug.Log($"Player Scores: {string.Join(", ", playerScores)}");

        currentPlayerText.SetText($"{PlayerConfigManager.instance.GetPlayerCharacterName(playerOrder[0])} choose your item.");
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

            item.GetComponent<ItemShower>().itemLogic = gameObject;


            itemObjects[i] = item;


            int[] itemCode;

            if (i == 0)
            {
                itemCode = gameObject.GetComponent<ItemChooser>().GivePositiveItem();
            }
            else if (i == 1)
            {
                itemCode = gameObject.GetComponent<ItemChooser>().GiveNegativeItem();
            }
            else
            {
                itemCode = gameObject.GetComponent<ItemChooser>().GiveRandomItem();
            }


            item.GetComponent<ItemShower>().initialiseBox(itemCode);


            if (i == 0)
            {
                item.GetComponent<ItemShower>().itemSelected();
            }
        }
    }


    public void buyItem(int playerIndex)
    {
        if (playerIndex != playerOrder[boughtItemCount]) { return; }

        string boughtItem = itemObjects[selectedItemPos].GetComponent<ItemShower>().itemBought();

        ApplyItem(playerIndex, boughtItem);

        itemStatus[selectedItemPos] = 1;

        boughtItemCount += 1;

        if (boughtItemCount != playerCount)
        {
            selectItem(playerOrder[boughtItemCount], 1);
        }
        else
        {
            moveToMaze();
        }


    }

    private void ApplyItem(int playerIndex, string boughtItem)
    {
        playerConfigs[playerIndex].playerItems.Append(boughtItem);

        if (boughtItem == "overclocked_pacemaker")
        {
            playerConfigs[playerIndex].playerDammageStat += 1;
            playerConfigs[playerIndex].playerHealthStat += 1;
            playerConfigs[playerIndex].playerSpeedStat += 1;
            playerConfigs[playerIndex].playerVisionStat += 1;
            playerConfigs[playerIndex].playerCritStat += 1;
        }
        else if (boughtItem == "toothbrush")
        {
            playerConfigs[playerIndex].playerDammageStat += 1;
        }
        else if (boughtItem == "hearty_cheese")
        {
            playerConfigs[playerIndex].playerHealthStat += 1;
        }
    }

    public void selectItem(int playerIndex, int change)
    {

        if (boughtItemCount == playerCount) { return; }
        if (playerIndex != playerOrder[boughtItemCount]) { return; }


        if (itemStatus[selectedItemPos] == 0)
        {
            itemObjects[selectedItemPos].GetComponent<ItemShower>().itemUnselected();
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

        itemObjects[selectedItemPos].GetComponent<ItemShower>().itemSelected();


    }




    public void moveToMaze()
    {
        Debug.Log($"Moving to maze Scene");

        SceneManager.LoadScene("Maze1");
    }

}

