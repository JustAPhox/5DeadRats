using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeTimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Timer_Text;
    [SerializeField] float Remaining_Time;
    float Start_Time;
    VertexGradient Original_Gradient;
    public bool Times_Up = false;
    public GameObject End_Segement_Button;

    private void Start()
    {
        Start_Time = Remaining_Time;
        Timer_Text.enableVertexGradient = true;
        Original_Gradient = Timer_Text.colorGradient;
        End_Segement_Button.SetActive(false);
    }

    void Update()
    {
        if (Times_Up == false)
        {
            Remaining_Time -= Time.deltaTime;
            int Minutes = Mathf.FloorToInt(Remaining_Time / 60);
            int Seconds = Mathf.FloorToInt(Remaining_Time % 60);
            Timer_Text.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);

            float T = Remaining_Time / Start_Time;

            Timer_Text.colorGradient = new VertexGradient(
                Color.Lerp(Color.red, Original_Gradient.topLeft, T),
                Color.Lerp(Color.red, Original_Gradient.topRight, T),
                Color.Lerp(Color.red, Original_Gradient.bottomLeft, T),
                Color.Lerp(Color.red, Original_Gradient.bottomRight, T)
            );

            if (Seconds <= 0 && Minutes <= 0)
            {
                Debug.Log("times up");
                Times_Up = true;
                //EditorApplication.ExitPlaymode();//will be replaced later
                GameObject Player_Manager = GameObject.Find("PlayerManager");
                RatManager Script = Player_Manager.GetComponent<RatManager>();
                GameObject Player1 = null;
                GameObject Player2 = null;
                GameObject Player3 = null;
                GameObject Player4 = null;

                if (Script.Player_Objects.Length > 0)
                    Player1 = Script.Player_Objects[0];

                if (Script.Player_Objects.Length > 1)
                    Player2 = Script.Player_Objects[1];

                if (Script.Player_Objects.Length > 2)
                    Player3 = Script.Player_Objects[2];

                if (Script.Player_Objects.Length > 3)
                    Player4 = Script.Player_Objects[3];

                int Player1_HP = 0;
                int Player2_HP = 0;
                int Player3_HP = 0;
                int Player4_HP = 0;
                if (Player1 != null)
                {
                    MazePlayerController P1_Script = Player1.GetComponent<MazePlayerController>();
                    Player1_HP = P1_Script.Current_HP;
                }
                if (Player2 != null)
                {
                    MazePlayerController P2_Script = Player2.GetComponent<MazePlayerController>();
                    Player2_HP = P2_Script.Current_HP;
                }
                if (Player3 != null)
                {
                    MazePlayerController P3_Script = Player3.GetComponent<MazePlayerController>();
                    Player3_HP = P3_Script.Current_HP;
                }
                if (Player4 != null)
                {
                    MazePlayerController P4_Script = Player4.GetComponent<MazePlayerController>();
                    Player4_HP = P4_Script.Current_HP;
                }

                if (Player1_HP > 0 || Player2_HP > 0 || Player3_HP > 0 || Player4_HP > 0)//
                {
                    int[] Player_Hps = { Player1_HP, Player2_HP, Player3_HP, Player4_HP };
                    int Highest_HP = 0;

                    foreach (int HP in Player_Hps)
                    {
                        if (HP > Highest_HP)
                        {
                            Highest_HP = HP;
                        }
                    }

                    if (Player1_HP == Highest_HP)
                    {
                        MazePlayerController P1_Script = Player1.GetComponent<MazePlayerController>();
                        P1_Script.Add_Win_Point();
                        Debug.Log("Player1 gained a point");
                    }
                    if (Player2_HP == Highest_HP)
                    {
                        MazePlayerController P2_Script = Player2.GetComponent<MazePlayerController>();
                        P2_Script.Add_Win_Point();
                        Debug.Log("Player2 gained a point");
                    }
                    if (Player3_HP == Highest_HP)
                    {
                        MazePlayerController P3_Script = Player3.GetComponent<MazePlayerController>();
                        P3_Script.Add_Win_Point();
                        Debug.Log("Player3 gained a point");
                    }
                    if (Player4_HP == Highest_HP)
                    {
                        MazePlayerController P4_Script = Player4.GetComponent<MazePlayerController>();
                        P4_Script.Add_Win_Point();
                        Debug.Log("Player4 gained a point");
                    }
                }
                End_Segement_Button.SetActive(true);
            }
        }
    }


    public void openQuiz()
    {
        SceneManager.LoadScene("Quiz Scene", LoadSceneMode.Single);
    }
}
