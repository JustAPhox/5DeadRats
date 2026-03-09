using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCharacterScript : MonoBehaviour
{
    //Set via code when created
    public GameObject QuizMaster;


    private PlayerConfig playerConfig;

    private PlayerControls controls;

    private int playerCharacter;

    private int prevPoints = 0;

    [SerializeField]
    private Image ratImage;


    [SerializeField]
    private Image podiumBacking;

    [SerializeField]
    private Sprite[] spriteMouthOpen;

    [SerializeField]
    private Sprite[] spriteMouthClose;

    [SerializeField]
    private TextMeshProUGUI pointText;

    [SerializeField]
    private TextMeshProUGUI cheeseText;

    [SerializeField]
    private GameObject[] voteShowers;
    [SerializeField]
    private GameObject[] noVoteShowers;


    [SerializeField]
    private AudioSource audioPlayer;

    [SerializeField]
    private AudioClip[] answerChangeSFX;

    [SerializeField]
    private AudioClip[] answerSelectSFX;

    [SerializeField]
    private AudioClip[] pointGetSFX;

    private void Awake()
    {
        // [IMPORTANT] Gets the controlls and changes the action map
        controls = new PlayerControls();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // [IMPORTANT] Sets up the object to store data on the player
    public void initialisePlayer(PlayerConfig config)
    {
        playerConfig = config;

        // Stores and shows the player character (okay just a number but thats like half the work)
        playerCharacter = config.playerCharacter;

        // [IMPORTANT] Adds a way to detect the C# events the players are creating.
        playerConfig.playerInput.onActionTriggered += PlayerInput_onActionTriggered;

        playerConfig.playerInput.SwitchCurrentActionMap("Quiz");

        ratImage.sprite = spriteMouthClose[playerCharacter - 1];

        cheeseText.SetText(playerConfig.winPoints.ToString());

        podiumBacking.color = Color.black;

    }

    // [IMPORTANT] Triggers whenever this player does an action.
    private void PlayerInput_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // Checks if the action was specifically performed (and not one of 2 other stages that can also fire and mess things up. Def needed for things you want triggered once. Don't know about movement.)
        if (obj.performed)
        {
            // Checks if action performed is the same as a cirtain action and if so does that function.
            if (obj.action.name == controls.Quiz.Answer1.name)
            {
                questionAnswered(1);
            }
            else if (obj.action.name == controls.Quiz.Answer2.name)
            {
                questionAnswered(2);
            }
            else if (obj.action.name == controls.Quiz.Answer3.name)
            {
                questionAnswered(3);
            }
            else if (obj.action.name == controls.Quiz.Answer4.name)
            {
                questionAnswered(4);
            }
        }
    }






    //Tells QuizScript a player voted
    public void questionAnswered(int answerGiven)
    {
        Debug.Log($"Player {playerConfig.playerIndex} voted on {answerGiven}");

        podiumBacking.color = Color.green;

        bool successfullyAnswered = QuizMaster.GetComponent<QuizScript>().answeredReceived(answerGiven, playerConfig.playerIndex);

        if (!successfullyAnswered) 
        { 
            podiumBacking.color = Color.black;
        }

        if (successfullyAnswered)
        {
            audioPlayer.clip = answerSelectSFX[Random.Range(0, answerSelectSFX.Length - 1)];
            audioPlayer.Play();
        }


    }



    public void updatePointTotal(int currentPoints)
    {
        pointText.SetText(currentPoints.ToString());


        if (currentPoints > prevPoints)
        {
            audioPlayer.clip = pointGetSFX[Random.Range(0, pointGetSFX.Length - 1)];
            audioPlayer.Play();
        }

    }



    public void hideAnswers()
    {
        for (int i = 0; i < 2; i++)
        {
            voteShowers[i].SetActive(false);
            noVoteShowers[i].SetActive(false);
        }
    }


    public void revealAnswer(int answer, int timesAnswered)
    {
        Debug.Log($"Show Answer for player {playerConfig.playerIndex}");

        podiumBacking.color = Color.black;

        if (answer == 0)
        {
            noVoteShowers[timesAnswered].SetActive(true);  
        }
        else if (answer == 1)
        {
            voteShowers[timesAnswered].transform.rotation = Quaternion.Euler(Vector3.forward * 0);
            voteShowers[timesAnswered].SetActive(true);
        }
        else if (answer == 2)
        {
            voteShowers[timesAnswered].transform.rotation = Quaternion.Euler(Vector3.forward * 90);
            voteShowers[timesAnswered].SetActive(true);
        }
        else if (answer == 3)
        {
            voteShowers[timesAnswered].transform.rotation = Quaternion.Euler(Vector3.forward * 270);
            voteShowers[timesAnswered].SetActive(true);
        }
        else if (answer == 4)
        {
            voteShowers[timesAnswered].transform.rotation = Quaternion.Euler(Vector3.forward * 180);
            voteShowers[timesAnswered].SetActive(true);
        }




    }
}
