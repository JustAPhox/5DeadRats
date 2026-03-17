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

    [SerializeField]
    private GameObject pauseMenu;

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

    private Question question;

    private int[] playerScores;

    private int currentPhase = 0;
    private int currentRound = 0;
    private int maxRounds = 2;

    // How many people have answered
    private int currentAnswerCount = 0;

    [SerializeField]
    private TextMeshProUGUI phaseText;


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
            startNextPhase();
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

        if (PlayerConfigManager.instance.GetDebugMode())
        {
            correctAnswerShower.SetActive(true);
        }


    }

    private void StartQuestion()
    {
        // Get a random question and store its info

        GetComponent<QuizQuestionPicker>().makeQuestion();

        // Stores the current question
        question = GetComponent<QuizQuestionPicker>().getQuestion();

        // Sends the questionbox the current question
        questionBox.GetComponent<QuizQuestionManager>().setCurrentQuestion();




        if (PlayerConfigManager.instance.GetDebugMode())
        {
            // Shows the correct answer for debug info
            correctAnswerShower.GetComponent<QuizAnswerShower>().setCorrectAnswer(question);
        }

        votingAllowed = true;

        phaseText.SetText("Answer The Question");

        startTimer(20);

    }



    private void startTimer(float timerLength)
    {
        timerEndTime = Time.time + timerLength;
        timerStarted = true;
    }
    private void stopTimer()
    {
        timerText.SetText($"00:00");
        timerStarted = false;
    }


    public void togglePause()
    {
        pauseMenu.GetComponent<PauseLogic>().TogglePause();
    }    
    public void pauseSelect()
    {
        pauseMenu.GetComponent<PauseLogic>().selectSelection();
    }    
    public void pauseChange()
    {
        pauseMenu.GetComponent<PauseLogic>().changeSelection();
    }


    public bool answeredReceived(int answerGiven, int playerNumber)
    {
        if (!votingAllowed) { return false; }

        // If the person hasn't answered already don't allow it to
        if (givenAnswers[playerNumber, currentPhase] != 0) { return true; }

        // Increment the number of answers
        currentAnswerCount++;
        // Store and show the answer given
        givenAnswers[playerNumber, currentPhase] = answerGiven;

        if (PlayerConfigManager.instance.GetDebugMode())
        {
            correctAnswerShower.GetComponent<QuizAnswerShower>().setPlayerAnswer(currentPhase, playerNumber, answerGiven);

        }


        // If everyone voted move to next phase
        if (currentAnswerCount == playerCount)
        {

            startNextPhase();


        }

        return true;
    }


    public void startNextPhase()
    {
        stopTimer();
        if (currentPhase == 0)
        {
            Debug.Log($"Started Phase {currentPhase + 1}");
            revealAnswers();
            phaseText.SetText("Debate And Reanswer");
            currentAnswerCount = 0;
            currentPhase += 1;
            startTimer(90);
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


            Invoke(nameof(moveToItems), 3f);
        }
    }



    private void moveToItems()
    {
        Debug.Log($"Moving to item Scene");
        assignWinners();

        SceneManager.LoadScene("Item Selection Scene");
    }

    private void revealAnswers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            playersObjects[i].GetComponent<QuizCharacterScript>().revealAnswer(givenAnswers[i, currentPhase], currentPhase);
        }
    }


    private bool checkVoteCorrect(int vote)
    {
        if (!question.allWrong && vote == question.correctAnswerPos[0])
        {
            return true;
        }
        else if (vote == 0 && question.emptyCorrect)
        {
            return true;
        }
        else
        {
            return false;
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
            if (checkVoteCorrect(givenAnswers[i, 1]))
            {
                playerScores[i] += 2;

                // Get a bonus point if starts correct
                if (checkVoteCorrect(givenAnswers[i, 0]))
                {
                    playerScores[i] += 1;
                }
            }


            // Gain score based on the number of people who changed to your first answer
            // Unless you didn't vote (unless not doing so was correct.)

            if (givenAnswers[i, 0] != 0 || question.emptyCorrect)
            {
                playerScores[i] += answerChanges[givenAnswers[i, 0]];

            }

            playersObjects[i].GetComponent<QuizCharacterScript>().updatePointTotal(playerScores[i]);
        }



        Invoke(nameof(startNextRound), 5f);
    }



    private void assignWinners()
    {

        // All players with best score win
        for (int i = 0; i < playerCount; i++)
        {
            PlayerConfigManager.instance.GetComponent<PlayerConfigManager>().setQuizScore(i, playerScores[i]);
        }



    }

}