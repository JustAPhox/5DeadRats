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

    public void SetPlayerIndex (int givenIndex)
    {
        index = givenIndex;
        playerIndexText.SetText($"Player { (givenIndex + 1).ToString()}");
        ignoreInputs = Time.time + 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ignoreInputs && inputsAllowed == false)
        {
            inputsAllowed = true;
        }
    }


    public void setCharacter(int character) 
    {
        if (!inputsAllowed) { return; }
        PlayerConfigManager.instance.setPlayerCharacter(index, character);
    }


    public void readyPlayer()
    {
        if (!inputsAllowed) { return; }
        PlayerConfigManager.instance.playerReady(index);
    }


}
