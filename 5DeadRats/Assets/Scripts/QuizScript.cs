using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    public Text questionBox;
    public Text answerOne;
    public Text answerTwo;
    public Text answerThree;
    public Text answerFour;


    public Text answerTimeText;
    public Text voteTimeText;


    int playerCount = 2;
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
        string[] questionDetails = findQuestion();

        questionBox.text = questionDetails[0];
        answerOne.text = questionDetails[1];
        answerTwo.text = questionDetails[2];
        answerThree.text = questionDetails[3];
        answerFour.text = questionDetails[4];



    }




    string[] findQuestion()
    {
        string[] questionDetails = { "What is the inevitable fate of god?", "Consumed by her own creations", "Faded away until its forgotten", "R A T S", "Sealed away by a greater power" };

        return questionDetails;
    }

    public void questionAnswered(int answerGiven)
    {
        currentAnswers++;


        if (currentAnswers == playerCount)
        {
            voteTime();



        }
    }

    void voteTime()
    {
        answerTimeText.enabled = false;
        voteTimeText.enabled = true;
        currentAnswers = 0;
    }

}
