using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetMazePlayerUI : MonoBehaviour
{
    public GameObject Player_1_UI_Pannel;
    public GameObject Player_1_Icon;
    public GameObject Player_1_CheeseCount;
    public GameObject Player_2_UI_Pannel;
    public GameObject Player_2_Icon;
    public GameObject Player_2_CheeseCount;
    public GameObject Player_3_UI_Pannel;
    public GameObject Player_3_Icon;
    public GameObject Player_3_CheeseCount;
    public GameObject Player_4_UI_Pannel;
    public GameObject Player_4_Icon;
    public GameObject Player_4_CheeseCount;

    public Sprite Ruby_Icon;
    public Sprite Pablo_Icon;
    public Sprite Winona_Icon;
    public Sprite John_Icon;
    public Sprite Steven_Icon;

    GameObject Player1 = null;
    GameObject Player2 = null;
    GameObject Player3 = null;
    GameObject Player4 = null;

    void Start()
    {
        Player_1_UI_Pannel.SetActive(false);
        Player_2_UI_Pannel.SetActive(false);
        Player_3_UI_Pannel.SetActive(false);
        Player_4_UI_Pannel.SetActive(false);

        GameObject Player_Manager = GameObject.Find("PlayerManager");
        RatManager Script = Player_Manager.GetComponent<RatManager>();

        if (Script.Player_Objects.Length > 0)
            Player1 = Script.Player_Objects[0];
            Player_1_UI_Pannel.SetActive(true);
            Set_Up(Player_1_Icon, Player_1_CheeseCount, Player1.GetComponent<MazePlayerController>());

        if (Script.Player_Objects.Length > 0)
            Player2 = Script.Player_Objects[1];
            Player_2_UI_Pannel.SetActive(true);
            Set_Up(Player_2_Icon, Player_2_CheeseCount, Player2.GetComponent<MazePlayerController>());

        if (Script.Player_Objects.Length > 1)
            Player3 = Script.Player_Objects[2];
            Player_3_UI_Pannel.SetActive(true);
            Set_Up(Player_3_Icon, Player_3_CheeseCount, Player3.GetComponent<MazePlayerController>());

        if (Script.Player_Objects.Length > 2)
            Player4 = Script.Player_Objects[3];
            Player_4_UI_Pannel.SetActive(true);
            Set_Up(Player_4_Icon, Player_4_CheeseCount, Player4.GetComponent<MazePlayerController>());
    }

    void Set_Up(GameObject Icon, GameObject Cheese_Count, MazePlayerController Script)
    {
        Image Player_Image = Icon.GetComponent<Image>();
        if(Script.playerCharacter == 0)
        {
            Player_Image.sprite = Ruby_Icon;
        }
        else if(Script.playerCharacter == 1)
        {
            Player_Image.sprite = Ruby_Icon;
        }
        else if(Script.playerCharacter == 2)
        {
            Player_Image.sprite = Pablo_Icon;
        }
        else if(Script.playerCharacter == 3)
        {
            Player_Image.sprite = Winona_Icon;
        }
        else if(Script.playerCharacter == 4)
        {
            Player_Image.sprite = John_Icon;
        }
        else if(Script.playerCharacter == 0)
        {
            Player_Image.sprite = Steven_Icon;
        }

        TextMeshProUGUI Cheese_Text = Cheese_Count.GetComponent<TextMeshProUGUI>();
        Cheese_Text.text = string.Format("x {0}", Script.playerConfig.winPoints);
    }
}
