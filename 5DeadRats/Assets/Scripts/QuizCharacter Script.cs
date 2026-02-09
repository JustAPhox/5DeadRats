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



    private void Awake()
    {
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



    public void initialisePlayer(PlayerConfig config)
    {
        playerConfig = config;
        playerCharacter = config.playerCharacter;
        characterText.SetText(playerCharacter.ToString());
        playerConfig.playerInput.onActionTriggered += PlayerInput_onActionTriggered;


    }

    private void PlayerInput_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
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
}
