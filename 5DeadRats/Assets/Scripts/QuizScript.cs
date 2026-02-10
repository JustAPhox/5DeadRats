using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private Text[,] testingworkds;

    // Stores info about answers
    private int[,] givenAnswers;
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

            // [IMPORTANT] Give them a player config to initialise with 
            player.GetComponent<QuizCharacterScript>().initialisePlayer(playerConfigs[i]);

            // Set parent and give them the quizmaster to report back to.
            player.transform.SetParent(controllerSpace.transform);
            player.GetComponent<QuizCharacterScript>().QuizMaster = gameObject;

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
        Debug.Log("Before Allowed check");

        if (!votingAllowed) { return; }

        Debug.Log("After Allowed check");


        // If the person hasn't answered already don't allow it to
        if (givenAnswers[playerNumber, currentRound] != 0) { return; }

        Debug.Log("Before Already Voted check");


        // Increment the number of answers
        currentAnswerCount++;
        // Store and show the answer given
        givenAnswers[playerNumber, currentRound] = answerGiven;
        correctAnswerShower.GetComponent<QuizAnswerShower>().setPlayerAnswer(currentRound, playerNumber, answerGiven);


        // If everyone voted move to next phase
        if (currentAnswerCount == playerCount)
        {
            countPoints();
        }
    }


    private void startRoundTwo()
    {
        // Reset answer count
        currentAnswerCount= 0;
        currentRound = 1;
    }



    private void countPoints()
    {
        votingAllowed = false;


        questionBox.GetComponent<QuizQuestionManager>().revealCorrectAnswer();




        for (int i = 0; i < playerCount; i++) 
        {
            if (givenAnswers[i,0] == correctAnswer)
            {
                Debug.Log($"Player {i} answered correctly");
                playerScores[i] += 3;
            }
            else
            {
                Debug.Log($"Player {i} answered incorrectly");
                playerScores[i] -= 1;
            }



            playersObjects[i].GetComponent<QuizCharacterScript>().updatePointTotal(playerScores[i]);
        }





    }
}