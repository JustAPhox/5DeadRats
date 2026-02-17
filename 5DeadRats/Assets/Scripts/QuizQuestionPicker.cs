using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class QuizQuestionPicker : MonoBehaviour
{

    private int[] choosenCategories = new int[6];
    private int maxChoosen;

    // Okay this should be better but like no
    //2d array of questions
    string[,] mediaQuestions = {
    { "What is the name of the monkey who played Dexter in Night at the Museum?", "Peggy", "Crystal", "Bonzo", "Belle", "2" },
    { "What is the name of Patty and Selma’s lizard from The Simpsons?", "Jub-Jub", "Chirpy Boy", "Mojo", "Raymond", "1" },
    { "Which of these events have NOT been featured in the Olympic Games?", "Steeplechase", "Basque Pelota", "Korfball", "Tejo", "4" },
    { "By how many pixels is the Minecraft toolbar off centre by in bedrock edition?", "2 pixels", "A single pixel", "Over 10 pixels", "Trick question, it’s on center", "2" }
    };

    string[,] scienceQuestions = {
    { "What is the diameter of the planet Pluto in inches?", "93.56 million", "2.1 billion", "44.32 million", "9.1 billion", "1" },
    { "How many species of fly are there in the UK?", "Around 1,000", "Around 7,000", "Around 150,000", "Around 4,500", "2" }
    };

    string[,] historyQuestions = {
    { "Who designed the Indian palace Malik Bagh?", "Eckart Muthesius", "Ickbar Bigglestein", "Jacob Burckhardt", "Datheid Bluterost", "1" },
    { "Which one of Stalin’s opponents in the power struggle died from an icepick to the head?", "Lev Kamenev", "Grigory Zinoviev", "Leon Trotsky", "Nikolai Bukharin", "3" }
    };

    string[,] ratQuestions = {
    { "What is the name of the first rat to go to space?", "Hector", "Splinter", "Victor", "Mickey", "1" },
    { "On average, how close are you to a rat right this second?", "One mile", "Six feet", "Nine metres", "FIfteen centimetres", "2" }

    };

    string[,] geographyQuestions = {
    { "How many states does Brazil have?", "26", "29", "35", "42", "1" },
    { "If the U.S. states were to be listed alphabetically, which would be listed 26th?", "Missouri", "Montana", "Nebraska", "Mississippi", "2" }
    };

    string[,] bonusQuestions = {
    { "Why are my parents arguing?", "It’s your fault", "It’s my fault", "Political disagreement", "Don’t worry about it", "4" },
    { "What number am I thinking of?", "23", "91", "68", "3889", "5" }
    };


    /// <summary>
    /// Gives a random question as an array.
    /// </summary>
    /// <returns>Random Question</returns>
    public string[] getQuestion(int[] questionCode)
    {
        string[,] questionList = findCategoryQuestionList(questionCode[0]);


        string[] questionGiven = { questionList[questionCode[1], 0], questionList[questionCode[1], 1], questionList[questionCode[1], 2], questionList[questionCode[1], 3], questionList[questionCode[1], 4], questionList[questionCode[1], 5] };

        return questionGiven;
    }


    private string[,] findCategoryQuestionList(int category)
    {
        switch (category)
        {
            case 0:
                return mediaQuestions;
            case 1:
                return scienceQuestions;
            case 2:
                return historyQuestions;
            case 3:
                return ratQuestions;
            case 4:
                return geographyQuestions;
            case 5:
                return bonusQuestions;
            default:
                return mediaQuestions;
        }


    }


    private int chooseCategory()
    {
        // 0 = Media and Entertainment
        // 1 = Science and Nature
        // 2 = History and the Outside World
        // 3 = Our Glorious Kind (rats)
        // 4 = Geography
        // 5 = Bonus Round


        // If all at the max times choosen increment
        bool allMaxxed = true;

        foreach (var item in choosenCategories)
        {
            if (item != maxChoosen) 
            {
                allMaxxed = false;
            }
        }

        if (allMaxxed == true)
        {
            maxChoosen++;
        }

        int wantedCategorey = UnityEngine.Random.Range(0, 6);

        // If at max times choosen reroll
        while (choosenCategories[wantedCategorey] == maxChoosen)
        {
            wantedCategorey = UnityEngine.Random.Range(0, 6);
        }



         return wantedCategorey;
    }

    private int chooseQuestion(int subject)
    {
        string[,] questionList = findCategoryQuestionList(subject);


        int questionCode = UnityEngine.Random.Range(0, questionList.GetLength(0)) ;
        return questionCode;
    }


    public int[] chooseFullQuestion()
    {
        int subject = chooseCategory();
        int[] questionCode = { subject, chooseQuestion(subject) };
        return questionCode;
    }

    public int giveAnswer(int[] questionCode)
    {
        string[,] questionList = findCategoryQuestionList(questionCode[0]);


        int correctAnswer = Convert.ToInt32(questionList[questionCode[1], 5]);
        return correctAnswer;
    }
}
