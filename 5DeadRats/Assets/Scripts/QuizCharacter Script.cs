using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizCharacterScript : MonoBehaviour
{
    //Set via code when created
    public GameObject QuizMaster;


    private PlayerConfig playerConfig;

    private PlayerControls controls;

    private int playerCharacter;

    [SerializeField]
    private TextMeshProUGUI characterText;


    [SerializeField]
    private TextMeshProUGUI pointText;


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
        characterText.SetText(playerCharacter.ToString());

        // [IMPORTANT] Adds a way to detect the C# events the players are creating.
        playerConfig.playerInput.onActionTriggered += PlayerInput_onActionTriggered;

        playerConfig.playerInput.SwitchCurrentActionMap("Quiz");
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

        QuizMaster.GetComponent<QuizScript>().answeredReceived(answerGiven, playerConfig.playerIndex);
    }



    public void updatePointTotal(int currentPoints)
    {
        pointText.SetText($"Points: {currentPoints.ToString()}");
    }
}
