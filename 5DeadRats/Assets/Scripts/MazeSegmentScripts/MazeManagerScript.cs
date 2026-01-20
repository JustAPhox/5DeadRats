using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazeManagerScript : MonoBehaviour
{
    public bool[] Player_Here_Info = { true, true, false, false, false, false };
    public int[] Player_Char_Info = { 0, 0, 0, 0, 0, 0 };
    public bool[] Player_Disadvantage_Info = { false, false, false, false, false, false };
    public bool[] Player_Advantage_Info = { false, false, false, false, false, false };

    public PlayerInputManager Player_Input_Manager;

    public int RealPlayerNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            SetPlayerPrefs(i);

            if (Player_Here_Info[i] == true)
            {
                RealPlayerNumber = RealPlayerNumber + 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Input_Manager != null)
        {
            //Debug.Log(RealPlayerNumber);
            int totalPlayers = Player_Input_Manager.playerCount;
            if (totalPlayers != RealPlayerNumber)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void SetPlayerPrefs(int Player)
    {
        if (PlayerPrefs.HasKey("Here" + Player.ToString()))
        {
            Player_Here_Info[Player] = bool.Parse(PlayerPrefs.GetString("Here" + Player.ToString()));
        }

        else
        {
            if (Player_Here_Info[Player] == false)
            {
                PlayerPrefs.SetString("Here" + Player.ToString(), "false");
            }
            else
            {
                PlayerPrefs.SetString("Here" + Player.ToString(), "true");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (PlayerPrefs.HasKey("Char" + Player.ToString()))
        {
            Player_Char_Info[Player] = PlayerPrefs.GetInt("Char" + Player.ToString());
        }

        else
        {
            PlayerPrefs.SetInt("Char" + Player.ToString(), Player_Char_Info[Player]);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (PlayerPrefs.HasKey("Disadvantage" + Player.ToString()))
        {
            Player_Disadvantage_Info[Player] = bool.Parse(PlayerPrefs.GetString("Disadvantage" + Player.ToString()));
        }

        else
        {
            if (Player_Disadvantage_Info[Player] == false)
            {
                PlayerPrefs.SetString("Disadvantage" + Player.ToString(), "false");
            }
            else
            {
                PlayerPrefs.SetString("Disadvantage" + Player.ToString(), "true");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (PlayerPrefs.HasKey("Advantage" + Player.ToString()))
        {
            Player_Advantage_Info[Player] = bool.Parse(PlayerPrefs.GetString("Advantage" + Player.ToString()));
        }

        else
        {
            if (Player_Advantage_Info[Player] == false)
            {
                PlayerPrefs.SetString("Advantage" + Player.ToString(), "false");
            }
            else
            {
                PlayerPrefs.SetString("Advantage" + Player.ToString(), "true");
            }
        }

        PlayerPrefs.Save();
    }
}
