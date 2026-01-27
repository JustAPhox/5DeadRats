using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCharacterScript : MonoBehaviour
{
    //Set via code when created
    public GameObject QuizMaster;
    public int playerNumber;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Tells QuizScript a player voted
    public void questionAnswered(int answerGiven)
    {
        QuizMaster.GetComponent<QuizScript>().answeredReceived(answerGiven, playerNumber);
    }
}
