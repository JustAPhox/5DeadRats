using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSetUpMenuControl : MonoBehaviour
{
    private int index;

    [SerializeField]
    private TextMeshProUGUI playerIndexText;


    private float ignoreInputs;
    private bool inputsAllowed = false;

    // Sets the index + shows player number + stops inputs from joining + inputing
    public void SetPlayerIndex (int givenIndex)
    {
        index = givenIndex;
        playerIndexText.SetText($"Player { (givenIndex + 1).ToString()}");
        ignoreInputs = Time.time + 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Reallows inputs
        if (Time.time > ignoreInputs && inputsAllowed == false)
        {
            inputsAllowed = true;
        }
    }


    // Tells the player config manager to change the player character for this player
    public void setCharacter(int character) 
    {
        if (!inputsAllowed) { return; }
        PlayerConfigManager.instance.setPlayerCharacter(index, character);
    }


    // Tells the player config manager this player is ready
    public void readyPlayer()
    {
        if (!inputsAllowed) { return; }
        PlayerConfigManager.instance.playerReady(index);
    }


}
