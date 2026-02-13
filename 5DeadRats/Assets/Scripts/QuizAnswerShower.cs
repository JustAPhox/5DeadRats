using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuizAnswerShower : MonoBehaviour
{
    // Debug info
    [SerializeField]
    private Text correctAnswerBox;
    [SerializeField]
    private Text[] answeredBoxRound1;
    [SerializeField]
    private Text[] answeredBoxRound2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCorrectAnswer(int correctAnswer)
    {
        correctAnswerBox.text = correctAnswer.ToString();
    }


    public void setPlayerAnswer(int round, int playerIndex, int answer)
    {
        if (round == 0)
        {
            answeredBoxRound1[playerIndex].text = answer.ToString();
        }
        else if (round == 1) 
        {
            answeredBoxRound2[playerIndex].text = answer.ToString();
        }
        else
        {
            Debug.Log("Tried to set player answer for a round that didn't exist");
        }
    }



}
