using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuizQuestionManager : MonoBehaviour
{
    // Used to show the current question
    [SerializeField]
    private Text questionTextBox;

    // Shows the current answers
    [SerializeField]
    private Text[] answerBoxText;

    // Changes correct answer to green
    [SerializeField]
    private GameObject[] answerBox;

    [SerializeField]
    private GameObject quizMaster;

    private int correctAnswer;

    public void setCurrentQuestion(int questionCode)
    {
        string[] questionDetails = quizMaster.GetComponent<QuizQuestionPicker>().getQuestion(questionCode);

        questionTextBox.text = questionDetails[0];
        answerBoxText[0].text = questionDetails[1];
        answerBoxText[1].text = questionDetails[2];
        answerBoxText[2].text = questionDetails[3];
        answerBoxText[3].text = questionDetails[4];
        correctAnswer = Convert.ToInt32(questionDetails[5]);

    }





    public void revealCorrectAnswer()
    {
        answerBox[correctAnswer - 1].GetComponent<Image>().color = Color.green;
    }





}
