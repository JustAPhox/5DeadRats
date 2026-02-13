using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    // Used to show the current question
    [SerializeField]
    private Text questionTextBox;

    // Used to show question and answers
    [SerializeField]
    private GameObject questionBox;

    // Shows the current answers
    [SerializeField]
    private Text[] answerBoxText;

    private int playerCount;


    // Used for inputting actions without a controller
    [SerializeField]
    private GameObject playerPreFab;

    private GameObject[] playersObjects;

    [SerializeField]
    private GameObject controllerSpace;

    // Debug info
    [SerializeField]
    private Text[] answeredBoxRound1;
    [SerializeField]
    private GameObject correctAnswerShower;

    // Stores info about answers
    private int[,] givenAnswers;
    private int[] answerChanges;
    private int correctAnswer;
    private int[] playerScores;

    private int currentRound = 0;

    // How many people have answered
    private int currentAnswerCount = 0;


    private bool votingAllowed = false;


    // Start is called before the first frame update
    void Start()
    {
        // Given Answers will always be the same length as number of players

        SetUpPlayers();


        givenAnswers = new int[playerCount,2];

        playerScores = new int[playerCount];

        answerChanges = new int[5];

        StartQuestion();

        votingAllowed = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetUpPlayers()
    {
        // [IMPORTANT] Gets an array of all the players
        var playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();

        // Gets the number of players
        playerCount = playerConfigs.Length;

        playersObjects = new GameObject[playerCount];

        // For each player
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            // Create a player object to be controlled
            var player = Instantiate(playerPreFab, controllerSpace.transform);

            // Set parent and give them the quizmaster to report back to.
            player.transform.SetParent(controllerSpace.transform);
            player.GetComponent<QuizCharacterScript>().QuizMaster = gameObject;

            // [IMPORTANT] Give them a player config to initialise with 
            player.GetComponent<QuizCharacterScript>().initialisePlayer(playerConfigs[i]);



           playersObjects[i] = player;
        }
    }

    void StartQuestion()
    {
        // Get a random question and store its info
        int questionCode = GetComponent<QuizQuestionPicker>().chooseQuestion();

        // Stores the correct answer
        correctAnswer = GetComponent<QuizQuestionPicker>().giveAnswer(questionCode);

        // Sends the questionbox the current question
        questionBox.GetComponent<QuizQuestionManager>().setCurrentQuestion(questionCode);

        // Shows the correct answer for debug info
        correctAnswerShower.GetComponent<QuizAnswerShower>().setCorrectAnswer(correctAnswer);
    }



    public void answeredReceived(int answerGiven, int playerNumber)
    {
        if (!votingAllowed) { return; }

        // If the person hasn't answered already don't allow it to
        if (givenAnswers[playerNumber, currentRound] != 0) { return; }

        // Increment the number of answers
        currentAnswerCount++;
        // Store and show the answer given
        givenAnswers[playerNumber, currentRound] = answerGiven;
        correctAnswerShower.GetComponent<QuizAnswerShower>().setPlayerAnswer(currentRound, playerNumber, answerGiven);


        // If everyone voted move to next phase
        if (currentAnswerCount == playerCount)
        {
            if (currentRound == 0) 
            {
                startNextRound();
            }
            else if (currentRound == 1)
            {
                countPoints();
            }
        }
    }


    private void startNextRound()
    {
        Debug.Log($"Started Round {currentRound + 1}");

        currentAnswerCount = 0;
        currentRound += 1;

    }



    private void countPoints()
    {
        Debug.Log("End of Quiz");

        votingAllowed = false;

        questionBox.GetComponent<QuizQuestionManager>().revealCorrectAnswer();


        // Get changes for each vote

        //

        for (int i = 0; i < playerCount; i++)
        {
            if (givenAnswers[i, 0] != givenAnswers[i, 1])
            {
                answerChanges[givenAnswers[i, 1]] += 1;
            }
        }

        // Gives points
        for (int i = 0; i < playerCount; i++) 
        {
            // If correct in the second round get 2 points
            if (givenAnswers[i,1] == correctAnswer)
            {
                playerScores[i] += 2;

                // Get a bonus point if starts correct
                if (givenAnswers[i, 0] == correctAnswer)
                {
                    playerScores[i] += 1;
                }
            }


            // Gain score based on the number of people who changed to your first answer
            // Unless you didn't vote

            if (givenAnswers[i, 0] != 0)
            {
                playerScores[i] += answerChanges[givenAnswers[i, 0]];

            }

            playersObjects[i].GetComponent<QuizCharacterScript>().updatePointTotal(playerScores[i]);
        }



        assignWinners();


    }



    private void assignWinners()
    {
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

}