using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class QuizQuestionPicker : MonoBehaviour
{

    private string[] categoryNames = { "Media and Entertainment", "Science and Nature", "History and the Outside World", "Our Glorious Kind", "Geography", "Bonus Round" };

    [SerializeField]
    private TextAsset itemJSON;

    private QuestionLists questionLists;

    private Question currentQuestion;

    private int prevCategory;


    private void Awake()
    {
        questionLists = JsonUtility.FromJson<QuestionLists>(itemJSON.text);
    }


    public Question getQuestion()
    {
        return currentQuestion;
    }

    public void makeQuestion()
    {
        List<int[]> seenQuestions = PlayerConfigManager.instance.getSeenQuestions();

        int randomCategory = UnityEngine.Random.Range(0, 6);
        
        // Rerolls category if its the same as the last one
        while (randomCategory == prevCategory)
        {
            randomCategory = UnityEngine.Random.Range(0, 6);
        }

        // THe list of questions from the category being used
        List<Question> randomQuestionList = questionLists.ProvideQuestionList(randomCategory);


        int questionIndex = 0;

        bool newQuestionFound = false;

        int attempts = 0;

        while (!newQuestionFound) {
            questionIndex = UnityEngine.Random.Range(0, randomQuestionList.Count);

            newQuestionFound = !seenQuestions.Contains(new int[] { randomCategory, questionIndex });

            attempts++;

            if (attempts > 10)
            {
                newQuestionFound = true;
            }
        }


        currentQuestion = randomQuestionList[questionIndex];

        PlayerConfigManager.instance.AddSeenQuestion(new int[2]{randomCategory, questionIndex});


        // 0 = Media and Entertainment
        // 1 = Science and Nature
        // 2 = History and the Outside World
        // 3 = Our Glorious Kind (rats)
        // 4 = Geography
        // 5 = Bonus Round

        currentQuestion.category = categoryNames[randomCategory];

        if (!currentQuestion.allWrong)
        {
            currentQuestion.correctAnswerPos[0] = UnityEngine.Random.Range(1, 5);
        }
    }
}





[System.Serializable]
public class QuestionLists
{
    public QuestionLists()
    {
        media = new List<Question>();
        science = new List<Question>();
        history = new List<Question>();
        rats = new List<Question>();
        geography = new List<Question>();
        bonus = new List<Question>();

    }
    public List<Question> media;
    public List<Question> science;
    public List<Question> history;
    public List<Question> rats;
    public List<Question> geography;
    public List<Question> bonus;


    public List<Question> ProvideQuestionList(int wantedCategory)
    {
        switch (wantedCategory)
        {
            case 0:
                return media;
            case 1:
                return science;
            case 2:
                return history;
            case 3:
                return rats;
            case 4:
                return geography;
            case 5:
                return bonus;
        }


        return media;
    }

}


[System.Serializable]
public class Question
{
    public Question()
    {
        question = new string[] { "Question?"};
        correctAnswer = new string[] { "Correct" };
        wrongAnswer = new string[] { "Wrong1", "Wrong2", "Wrong3", "Wrong4", "Wrong5" };
        correctAnswerPos = new int[1];

        emptyCorrect = false;
        allWrong = false;
    }


    public string[] question;
    public string[] correctAnswer;
    public string[] wrongAnswer;
    public string category;

    public int[] correctAnswerPos;


    public bool emptyCorrect;
    public bool allWrong;
}