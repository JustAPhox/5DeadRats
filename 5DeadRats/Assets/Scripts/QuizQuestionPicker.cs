using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestionPicker : MonoBehaviour
{


    // Okay this should be better but like no
    //2d array of questions
    string[,] questions = {
    { "Question 1", "Wrong", "Correct", "Still Wrong", "More Wrong", "2" },
    { "Question 2", "Correct", "Wrong", "Still Wrong", "More Wrong", "1" },
    { "Question 3", "Wrong", "Still Wrong", "Correct", "More Wrong", "3" },
    { "Question 4", "Wrong", "More Wrong", "Still Wrong", "Correct", "4" }
    };


    /// <summary>
    /// Gives a random question as an array.
    /// </summary>
    /// <returns>Random Question</returns>
    public string[] getQuestion(int questionCode)
    {
        string[] questionGiven = {"Question", "Answer 0", "Answer 1", "Answer 2", "Answer 3", "Correct Answer Number"};

        questionGiven[0] = questions[questionCode, 0];
        questionGiven[1] = questions[questionCode, 1];
        questionGiven[2] = questions[questionCode, 2];
        questionGiven[3] = questions[questionCode, 3];
        questionGiven[4] = questions[questionCode, 4];
        questionGiven[5] = questions[questionCode, 5];

        return questionGiven;
    }



    public int chooseQuestion()
    {
        int questionCode = UnityEngine.Random.Range(0, questions.GetLength(0));
        return questionCode;
    }

    public int giveAnswer(int questionCode)
    {
        int correctAnswer = Convert.ToInt32(questions[questionCode, 5]);
        return correctAnswer;
    }
}
