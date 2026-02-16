using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuizScript : MonoBehaviour
{
    // Used to show question and answers
    [SerializeField]
    private GameObject questionBox;

    private int playerCount;



    [SerializeField] 
    TextMeshProUGUI timerText;

    private float timerEndTime;
    private bool timerStarted = false;


    // Used for inputting actions without a controller
    [SerializeField]
    private GameObject playerPreFab;

    private GameObject[] playersObjects;

    [SerializeField]
    private GameObject controllerSpace;

    // Debug info
    [SerializeField]
    private GameObject correctAnswerShower;

    // Stores info about answers
    private int[,] givenAnswers;
    private int[] answerChanges;
    private int correctAnswer;
    private int[] playerScores;

    private int currentPhase = 0;
    private int currentRound = 0;
    private int maxRounds = 2;

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

    }


    // Update is called once per frame
    void Update()
    {
        if ((timerEndTime <= Time.time) && timerStarted)
        {
            Debug.Log("Timer Ended");
            timerStarted = false;
            startNextPhase();
            timerText.SetText($"00:00");
        }
        else if (timerStarted)
        {
            TimeSpan currentTimer = new TimeSpan(0,0,0, (int) (timerEndTime - Time.time) ,0);

            string secondsLeft = currentTimer.Seconds.ToString();

            if (secondsLeft.Length == 1)
            {
                secondsLeft = "0" + secondsLeft;
            }


            timerText.SetText($"{currentTimer.Minutes}:{secondsLeft}");


        }


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

    private void StartQuestion()
    {
        // Get a random question and store its info
        int questionCode = GetComponent<QuizQuestionPicker>().chooseQuestion();

        // Stores the correct answer
        correctAnswer = GetComponent<QuizQuestionPicker>().giveAnswer(questionCode);

        // Sends the questionbox the current question
        questionBox.GetComponent<QuizQuestionManager>().setCurrentQuestion(questionCode);

        // Shows the correct answer for debug info
        correctAnswerShower.GetComponent<QuizAnswerShower>().setCorrectAnswer(correctAnswer);

        votingAllowed = true;

        startTimer(20);

    }



    private void startTimer(float timerLength)
    {
        timerEndTime = Time.time + timerLength;
        timerStarted = true;
    }


    public void answeredReceived(int answerGiven, int playerNumber)
    {
        if (!votingAllowed) { return; }

        // If the person hasn't answered already don't allow it to
        if (givenAnswers[playerNumber, currentPhase] != 0) { return; }

        // Increment the number of answers
        currentAnswerCount++;
        // Store and show the answer given
        givenAnswers[playerNumber, currentPhase] = answerGiven;
        correctAnswerShower.GetComponent<QuizAnswerShower>().setPlayerAnswer(currentPhase, playerNumber, answerGiven);


        // If everyone voted move to next phase
        if (currentAnswerCount == playerCount)
        {

            startNextPhase();


        }
    }


    public void startNextPhase()
    {
        if (currentPhase == 0)
        {
            Debug.Log($"Started Phase {currentPhase + 1}");
            revealAnswers();
            currentAnswerCount = 0;
            currentPhase += 1;
            startTimer(30);
        }
        else if (currentPhase == 1)
        {
            countPoints();
        }




    }


    public void startNextRound()
    {
        currentRound++;

        if (currentRound == (maxRounds -1))
        {
            Debug.Log($"Starting next round of questions");

            currentAnswerCount = 0;
            currentPhase = 0;

            answerChanges = new int[5];
            givenAnswers = new int[playerCount, 2];


            for (int i = 0; i < playerCount; i++)
            {
                playersObjects[i].GetComponent<QuizCharacterScript>().hideAnswers();

            }



            StartQuestion();
        }
        else
        {
            assignWinners();


            Invoke(nameof(moveToItems), 5f);
        }
    }



    public void moveToItems()
    {
        Debug.Log($"Moving to item Scene");
        assignWinners();

        SceneManager.LoadScene("Item Selection Scene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(1);
    }

    private void revealAnswers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            playersObjects[i].GetComponent<QuizCharacterScript>().revealAnswer(givenAnswers[i, currentPhase], currentPhase);
        }
    }





    private void countPoints()
    {
        Debug.Log("End of Quiz");
        revealAnswers();
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
            // If correct in the second phase get 2 points
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



        Invoke(nameof(startNextRound), 5f);
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