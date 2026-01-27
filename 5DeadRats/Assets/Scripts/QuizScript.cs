using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    public Text questionBox;
    int playerCount = gameManager.getPlayerCount();


    public GameObject fakeController;
    public GameObject controllerSpace;

    public Text[] answerBoxes;

    public Text correctAnswerBox;
    public Text[] answerCounters;

    int[] givenAnswers;
    int correctAnswer;


    public Text answerTimeText;
    public Text voteTimeText;


    int currentAnswers = 0;


    // Start is called before the first frame update
    void Start()
    {
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

        string[] questionDetails = GetComponent<QuizQuestionPicker>().getQuestion();
        //string[] questionDetails = findQuestion();

        questionBox.text = questionDetails[0];
        answerBoxes[0].text = questionDetails[1];
        answerBoxes[1].text = questionDetails[2];
        answerBoxes[2].text = questionDetails[3];
        answerBoxes[3].text = questionDetails[4];
        correctAnswer = Convert.ToInt32(questionDetails[5]);


        Debug.Log(correctAnswer);
        correctAnswerBox.text = correctAnswer.ToString();

    }



    public void answeredReceived(int answerGiven, int playerNumber)
    {
        if (givenAnswers[playerNumber] == 0)
        {
            currentAnswers++;

        }
        givenAnswers[playerNumber] = answerGiven;
        answerCounters[playerNumber].text = answerGiven.ToString();


        // Need to check all answers have been given
        if (currentAnswers == playerCount)
        {
            voteTime();
        }
    }


    void voteTime()
    {
        answerTimeText.GetComponent<Text>().enabled = false;
        voteTimeText.GetComponent<Text>().enabled = true;
        currentAnswers= 0;

        for (int i = 0; i< playerCount; i++)
        {
            givenAnswers[i] = 0;
            answerCounters[i].text = "0";


        }

    }

}
