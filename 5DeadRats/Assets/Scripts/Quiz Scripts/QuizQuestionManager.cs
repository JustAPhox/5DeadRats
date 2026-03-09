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

    private int[] correctAnswer;

    public void setCurrentQuestion()
    {
        Question question = quizMaster.GetComponent<QuizQuestionPicker>().getQuestion();

        questionTextBox.SetText(question.question[0]);
        categoryTextBox.SetText(question.category);
        correctAnswer = question.correctAnswerPos;

        // Set Correct Answer Text
        answerBoxText[correctAnswer[0] - 1].SetText(question.correctAnswer[0]);


        // Set Random Wrong Answers
        bool[] wrongAnswersUsed = new bool[question.wrongAnswer.Length];

        for (int i = 0; i < 4; i++)
        {
            if (i == correctAnswer[0] - 1) { continue; }

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
        if (correctAnswer[0] != 0 && correctAnswer[0] != 5)
        {
            answerBox[correctAnswer[0] - 1].GetComponent<Image>().color = Color.green;
        }
    }





}
