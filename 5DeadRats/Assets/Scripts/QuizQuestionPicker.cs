using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestionPicker : MonoBehaviour
{


    // Okay this should be better but like no
    //2d array of questions
    string[,] questions = {
    { "What is the name of the monkey who played Dexter in Night at the Museum?", "Peggy", "Crystal", "Bonzo", "Belle", "2" },
    { "What is the name of Patty and Selma’s lizard from The Simpsons?", "Jub-Jub", "Chirpy Boy", "Mojo", "Raymond", "1" },
    { "Which of these events have NOT been featured in the Olympic Games?", "Steeplechase", "Basque Pelota", "Korfball", "Tejo", "4" },
    { "By how many pixels is the Minecraft toolbar off centre by in bedrock edition?", "2 pixels", "A single pixel", "Over 10 pixels", "Trick question, it’s on center", "2" }
    };


    /// <summary>
    /// Gives a random question as an array.
    /// </summary>
    /// <returns>Random Question</returns>
    public string[] getQuestion(int questionCode)
    {
        string[] questionGiven = { questions[questionCode, 0], questions[questionCode, 1], questions[questionCode, 2], questions[questionCode, 3], questions[questionCode, 4], questions[questionCode, 5] };

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
