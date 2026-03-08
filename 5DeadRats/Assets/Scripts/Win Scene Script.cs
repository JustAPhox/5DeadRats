using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneScript : MonoBehaviour
{
    public GameObject Ruby_Wins;
    public GameObject Pablo_Wins;
    public GameObject Winona_Wins;
    public GameObject John_Wins;
    public GameObject Steven_Wins;
    public Vector2 Placement_Position;
    public Canvas This_Canvas;
    void Start()
    {
        var Player_Configs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        int Winning_Player = Player_Configs[0].playerCharacter;
        int Winning_Point_Total = Player_Configs[0].winPoints;

        foreach(PlayerConfig Player in Player_Configs)
        {
            if(Player.winPoints > Winning_Point_Total)
            {
                Winning_Point_Total = Player.winPoints;
                Winning_Player = Player.playerCharacter;
            }
        }

        if(Winning_Player == 0)
        {
            GameObject Winner_Icon = Instantiate(Ruby_Wins, This_Canvas.transform);
            RectTransform Rect = Winner_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Placement_Position;
        }
        else if(Winning_Player == 1)
        {
            GameObject Winner_Icon = Instantiate(Ruby_Wins, This_Canvas.transform);
            RectTransform Rect = Winner_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Placement_Position;
        }
        else if(Winning_Player == 2)
        {
            GameObject Winner_Icon = Instantiate(Pablo_Wins, This_Canvas.transform);
            RectTransform Rect = Winner_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Placement_Position;
        }
        else if(Winning_Player == 3)
        {
            GameObject Winner_Icon = Instantiate(Winona_Wins, This_Canvas.transform);
            RectTransform Rect = Winner_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Placement_Position;
        }
        else if(Winning_Player == 4)
        {
            GameObject Winner_Icon = Instantiate(John_Wins, This_Canvas.transform);
            RectTransform Rect = Winner_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Placement_Position;
        }
        else if(Winning_Player == 5)
        {
            GameObject Winner_Icon = Instantiate(Steven_Wins, This_Canvas.transform);
            RectTransform Rect = Winner_Icon.GetComponent<RectTransform>();
            Rect.anchoredPosition = Placement_Position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
