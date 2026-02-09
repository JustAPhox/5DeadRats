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
    private Text questionBox;
    // Shows the current answers
    [SerializeField]
    private Text[] answerBoxes;

    private int playerCount;


    // Used for inputting actions without a controller
    [SerializeField]
    private GameObject fakeController;
    [SerializeField]
    private GameObject controllerSpace;

    // Debug info
    [SerializeField]
    private Text correctAnswerBox;
    [SerializeField]
    private Text[] currentAnswers;

    // Stores info about answers
    private int[] givenAnswers;
    private int correctAnswer;

    // Shows the current voting type
    [SerializeField]
    private Text answerTimeText;
    [SerializeField]
    private Text voteTimeText;

    // How many people have answered
    private int currentAnswerCount = 0;





    // Start is called before the first frame update
    void Start()
    {
        // Given Answers will always be the same length as number of players

        SetUpPlayers();


        givenAnswers = new int[playerCount];

        StartQuestion();
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


        // For each player
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            // Create an instance for them to control (I'll rename later)
            var player = Instantiate(fakeController, controllerSpace.transform);

            // [IMPORTANT] Give them a player config to initialise with 
            player.GetComponent<QuizCharacterScript>().initialisePlayer(playerConfigs[i]);

            // Set parent and give them the quizmaster to report back to.
            player.transform.SetParent(controllerSpace.transform);
            player.GetComponent<QuizCharacterScript>().QuizMaster = gameObject;
        }
    }

    void StartQuestion()
    {
        // Get a random question and store its info
        string[] questionDetails = GetComponent<QuizQuestionPicker>().getQuestion();

        questionBox.text = questionDetails[0];
        answerBoxes[0].text = questionDetails[1];
        answerBoxes[1].text = questionDetails[2];
        answerBoxes[2].text = questionDetails[3];
        answerBoxes[3].text = questionDetails[4];
        correctAnswer = Convert.ToInt32(questionDetails[5]);

        correctAnswerBox.text = correctAnswer.ToString();
    }



    public void answeredReceived(int answerGiven, int playerNumber)
    {
        // If the person hasn't answered already don't allow it to
        if (givenAnswers[playerNumber] != 0) { return; }

        // Increment the number of answers
        currentAnswerCount++;
        // Store and show the answer given
        givenAnswers[playerNumber] = answerGiven;
        currentAnswers[playerNumber].text = answerGiven.ToString();


        // If everyone voted move to next phase
        if (currentAnswerCount == playerCount)
        {
            voteTime();
        }
    }


    void voteTime()
    {
        Debug.Log("Questions Cleared");
        // Change which text is shown. (Need to change rest of UI)
        answerTimeText.GetComponent<Text>().enabled = false;
        voteTimeText.GetComponent<Text>().enabled = true;

        // Reset answer count
        currentAnswerCount= 0;

        // Each player's answers are removed
        for (int i = 0; i< playerCount; i++)
        {
            givenAnswers[i] = 0;
            currentAnswers[i].text = "0";
        }
    }
}