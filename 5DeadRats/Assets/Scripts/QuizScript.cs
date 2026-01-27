using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    public Text questionBox;



    public Text[] answerBoxes;

    public Text answerCountCorrect;
    public Text[] answerCounters;

    int[] givenAnswers = {0,0,0,0};
    int correctAnswer;


    public Text answerTimeText;
    public Text voteTimeText;


    int playerCount = gameManager.getPlayerCount();
    int currentAnswers = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }



    public void questionAnswered(int answerGiven)
    {
        currentAnswers++;
        givenAnswers[answerGiven] += 1;


        answerCounters[answerGiven].text = givenAnswers[answerGiven].ToString();
        answerCountCorrect.text = givenAnswers[correctAnswer].ToString();



        if (currentAnswers == playerCount)
        {
            voteTime();
        }
    }


    void voteTime()
    {
        answerTimeText.GetComponent<Text>().enabled = false;
        voteTimeText.GetComponent<Text>().enabled = true;
        currentAnswers = 0;
    }

}
