using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    // Used to show the current question
    public Text questionBox;
    // Shows the current answers
    public Text[] answerBoxes;

    int playerCount = gameManager.getPlayerCount();

    // Used for inputting actions without a controller
    public GameObject fakeController;
    public GameObject controllerSpace;

    // Debug info
    public Text correctAnswerBox;
    public Text[] currentAnswers;

    // Stores info about answers
    int[] givenAnswers;
    int correctAnswer;

    // Shows the current voting type
    public Text answerTimeText;
    public Text voteTimeText;

    // How many people have answered
    int currentAnswerCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Given Answers will always be the same length as number of players
        givenAnswers = new int[playerCount];

        SetUpPlayers();
        StartQuestion();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetUpPlayers()
    {
        // For each player create a fake controller
        for (int i = 0; i < playerCount; i++)
        {
            GameObject characterController;
            characterController = Instantiate(fakeController);

            characterController.transform.SetParent(controllerSpace.transform);
            characterController.GetComponent<RectTransform>().anchoredPosition = new Vector2(110 + 220*i,55);
            characterController.GetComponent<QuizCharacterScript>().QuizMaster = gameObject;
            characterController.GetComponent<QuizCharacterScript>().playerNumber = i;

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
        // If the person hasn't answered already increment the counter
        if (givenAnswers[playerNumber] == 0)
        {
            currentAnswerCount++;

        }

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