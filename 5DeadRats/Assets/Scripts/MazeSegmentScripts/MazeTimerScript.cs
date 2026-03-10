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
    public float Remaining_Time;
    float Start_Time;
    VertexGradient Original_Gradient;
    public bool Times_Up = false;
    public GameObject End_Segement_Button;

    public GameObject Ruby_Wins;
    public GameObject Ruby_Looses;
    public GameObject Pablo_Wins;
    public GameObject Pablo_Looses;
    public GameObject Winona_Wins;
    public GameObject Winona_Looses;
    public GameObject John_Wins;
    public GameObject John_Looses;
    public GameObject Steven_Wins;
    public GameObject Steven_Looses;

    public Canvas This_Canvas;

    public Vector2 P1_Pos;
    public Vector2 P2_Pos;
    public Vector2 P3_Pos;
    public Vector2 P4_Pos;

    MazePlayerController P1_Script = null;
    MazePlayerController P2_Script = null;
    MazePlayerController P3_Script = null;
    MazePlayerController P4_Script = null;

    bool Sudden_Death = false;

    private Coroutine Delay_Coroutine;

    public bool Delay_Over = false;

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

            Check_All_Dead();

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
                    P1_Script = Player1.GetComponent<MazePlayerController>();
                    Player1_HP = P1_Script.Current_HP;
                }
                if (Player2 != null)
                {
                    P2_Script = Player2.GetComponent<MazePlayerController>();
                    Player2_HP = P2_Script.Current_HP;
                }
                if (Player3 != null)
                {
                    P3_Script = Player3.GetComponent<MazePlayerController>();
                    Player3_HP = P3_Script.Current_HP;
                }
                if (Player4 != null)
                {
                    P4_Script = Player4.GetComponent<MazePlayerController>();
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
                        P1_Script = Player1.GetComponent<MazePlayerController>();
                        P1_Script.Add_Win_Point();
                        Debug.Log("Player1 gained a point");
                        Place_Win_Icon(P1_Script.playerCharacter, P1_Pos);
                    }
                    else if (Player1 != null)
                    {
                        P1_Script = Player1.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P1_Script.playerCharacter, P1_Pos);
                    }

                    if (Player2_HP == Highest_HP)
                    {
                        P2_Script = Player2.GetComponent<MazePlayerController>();
                        P2_Script.Add_Win_Point();
                        Debug.Log("Player2 gained a point");
                        Place_Win_Icon(P2_Script.playerCharacter, P2_Pos);
                    }
                    else if (Player2 != null)
                    {
                        P2_Script = Player2.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P2_Script.playerCharacter, P2_Pos);
                    }

                    if (Player3_HP == Highest_HP)
                    {
                        P3_Script = Player3.GetComponent<MazePlayerController>();
                        P3_Script.Add_Win_Point();
                        Debug.Log("Player3 gained a point");
                        Place_Win_Icon(P3_Script.playerCharacter, P3_Pos);
                    }
                    else if (Player3 != null)
                    {
                        P3_Script = Player3.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P3_Script.playerCharacter, P3_Pos);
                    }

                    if (Player4_HP == Highest_HP)
                    {
                        P4_Script = Player4.GetComponent<MazePlayerController>();
                        P4_Script.Add_Win_Point();
                        Debug.Log("Player4 gained a point");
                        Place_Win_Icon(P4_Script.playerCharacter, P4_Pos);
                    }
                    else if (Player4 != null)
                    {
                        P4_Script = Player4.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P4_Script.playerCharacter, P4_Pos);
                    }
                }
                else
                {
                    if (Player1 != null)
                    {
                        P1_Script = Player1.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P1_Script.playerCharacter, P1_Pos);
                    }

                    if (Player2 != null)
                    {
                        P2_Script = Player2.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P2_Script.playerCharacter, P2_Pos);
                    }

                    if (Player3 != null)
                    {
                        P3_Script = Player3.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P3_Script.playerCharacter, P3_Pos);
                    }

                    if (Player4 != null)
                    {
                        P4_Script = Player4.GetComponent<MazePlayerController>();
                        Place_Loose_Icon(P4_Script.playerCharacter, P4_Pos);
                    }
                }

                End_Segement_Button.SetActive(true);
                Call_Delay_Timer();
            }
        }
    }


    public void openQuiz()
    {
        //P1_Script.playerConfig.winPoints = 11;
        //P2_Script.playerConfig.winPoints = 10;

        bool Win_Scene_Open = false;
        
        if (P4_Script != null)
        {
            if (P1_Script.playerConfig.winPoints >= 3 || P2_Script.playerConfig.winPoints >= 3 || P3_Script.playerConfig.winPoints >= 3 || P4_Script.playerConfig.winPoints >= 3)
            {
                if (P1_Script.playerConfig.winPoints != P2_Script.playerConfig.winPoints && P1_Script.playerConfig.winPoints != P3_Script.playerConfig.winPoints && P1_Script.playerConfig.winPoints != P4_Script.playerConfig.winPoints)
                {
                    if (P2_Script.playerConfig.winPoints != P3_Script.playerConfig.winPoints && P2_Script.playerConfig.winPoints != P4_Script.playerConfig.winPoints)
                    {
                        if (P3_Script.playerConfig.winPoints != P4_Script.playerConfig.winPoints)
                        {
                            Win_Scene_Open = true;
                        }
                    }
                }
            }
        }

        if (P3_Script != null)
        {
            if (P1_Script.playerConfig.winPoints >= 3 || P2_Script.playerConfig.winPoints >= 3 || P3_Script.playerConfig.winPoints >= 3)
            {
                if (P1_Script.playerConfig.winPoints != P2_Script.playerConfig.winPoints && P1_Script.playerConfig.winPoints != P3_Script.playerConfig.winPoints)
                {
                    if (P2_Script.playerConfig.winPoints != P3_Script.playerConfig.winPoints)
                    {
                        Win_Scene_Open = true;
                    }
                }
            }
        }

        if (P1_Script.playerConfig.winPoints >= 3 || P2_Script.playerConfig.winPoints >= 3)
        {
            if (P1_Script.playerConfig.winPoints != P2_Script.playerConfig.winPoints)
            {
                Win_Scene_Open = true;
            }
        }

        if (Win_Scene_Open == true)
        {
            Debug.Log("Move to Win scene");
            SceneManager.LoadScene("Win Scene", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Quiz Scene", LoadSceneMode.Single);
        }
    }

    public void Check_All_Dead()
    {
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
        int Amount_Of_Living_Players = 0;

        if (Player1 != null)
        {
            MazePlayerController P1_Script = Player1.GetComponent<MazePlayerController>();
            Player1_HP = P1_Script.Current_HP;
            if(Player1_HP > 0)
            {
                Amount_Of_Living_Players++;
            }
        }
        if (Player2 != null)
        {
            MazePlayerController P2_Script = Player2.GetComponent<MazePlayerController>();
            Player2_HP = P2_Script.Current_HP;
            if (Player2_HP > 0)
            {
                Amount_Of_Living_Players++;
            }
        }
        if (Player3 != null)
        {
            MazePlayerController P3_Script = Player3.GetComponent<MazePlayerController>();
            Player3_HP = P3_Script.Current_HP;
            if (Player3_HP > 0)
            {
                Amount_Of_Living_Players++;
            }
        }
        if (Player4 != null)
        {
            MazePlayerController P4_Script = Player4.GetComponent<MazePlayerController>();
            Player4_HP = P4_Script.Current_HP;
            if (Player1_HP > 0)
            {
                Amount_Of_Living_Players++;
            }
        }

        if (Player1_HP == 0 && Player2_HP == 0 && Player3_HP == 0 && Player4_HP == 0)
        {
            Remaining_Time = 0;
        }
        else if (Amount_Of_Living_Players == 1 && Sudden_Death == false && Remaining_Time > 20)
        {
            Remaining_Time = 20;
            Sudden_Death = true;
        }
    }


    public void Place_Win_Icon(int Player_Character, Vector2 Pos)
    {
        if(Player_Character == 0)
        {
            GameObject Current_Player_Icon = Instantiate(Ruby_Wins, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 1)
        {
            GameObject Current_Player_Icon = Instantiate(Ruby_Wins, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 2)
        {
            GameObject Current_Player_Icon = Instantiate(Pablo_Wins, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 3)
        {
            GameObject Current_Player_Icon = Instantiate(Winona_Wins, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 4)
        {
            GameObject Current_Player_Icon = Instantiate(John_Wins, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 5)
        {
            GameObject Current_Player_Icon = Instantiate(Steven_Wins, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
    }

    public void Place_Loose_Icon(int Player_Character, Vector2 Pos)
    {
        if(Player_Character == 0)
        {
            GameObject Current_Player_Icon = Instantiate(Ruby_Looses, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 1)
        {
            GameObject Current_Player_Icon = Instantiate(Ruby_Looses, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 2)
        {
            GameObject Current_Player_Icon = Instantiate(Pablo_Looses, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 3)
        {
            GameObject Current_Player_Icon = Instantiate(Winona_Looses, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 4)
        {
            GameObject Current_Player_Icon = Instantiate(John_Looses, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
        else if(Player_Character == 5)
        {
            GameObject Current_Player_Icon = Instantiate(Steven_Looses, This_Canvas.transform);
            RectTransform Rect = Current_Player_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Pos;
        }
    }

    public void Call_Delay_Timer()
    {
        Delay_Coroutine = StartCoroutine(Delay_Timer());
    }

    private IEnumerator Delay_Timer()
    {
        yield return new WaitForSeconds(3);
        Delay_Over = true;
    }
}
