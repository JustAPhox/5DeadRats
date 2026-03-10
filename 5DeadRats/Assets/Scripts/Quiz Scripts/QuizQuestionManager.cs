using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuizQuestionManager : MonoBehaviour
{
    // Used to show the current question
    [SerializeField]
    private TextMeshProUGUI questionTextBox;

    Question question;

    [SerializeField]
    private TextMeshProUGUI categoryTextBox;

    // Shows the current answers
    [SerializeField]
    private TextMeshProUGUI[] answerBoxText;

    // Changes correct answer to green
    [SerializeField]
    private GameObject[] answerBox;

    [SerializeField]
    private GameObject quizMaster;


    public void setCurrentQuestion()
    {
        question = quizMaster.GetComponent<QuizQuestionPicker>().getQuestion();

        questionTextBox.SetText(question.question[0]);
        categoryTextBox.SetText(question.category);

        // Set Correct Answer Text

        if (!question.allWrong)
        {
            answerBoxText[question.correctAnswerPos[0] - 1].SetText(question.correctAnswer[0]);
        }


        // Set Random Wrong Answers
        bool[] wrongAnswersUsed = new bool[question.wrongAnswer.Length];

        for (int i = 0; i < 4; i++)
        {
            if (!question.allWrong && i == question.correctAnswerPos[0] - 1) { continue; }

            int wrongAnswerIndex = UnityEngine.Random.Range(0, question.wrongAnswer.Length);

            while (wrongAnswersUsed[wrongAnswerIndex] == true)
            {
                wrongAnswerIndex = UnityEngine.Random.Range(0, question.wrongAnswer.Length);
            }


            answerBoxText[i].SetText(question.wrongAnswer[wrongAnswerIndex]);
            wrongAnswersUsed[wrongAnswerIndex] = true;
        }







        for (int i = 0; i < 4; i++)
        {
            answerBox[i].GetComponent<Image>().color = Color.black;


        }

    }





    public void revealCorrectAnswer()
    {
        if (!question.allWrong)
        {
            answerBox[question.correctAnswerPos[0] - 1].GetComponent<Image>().color = Color.green;
        }
    }





}
