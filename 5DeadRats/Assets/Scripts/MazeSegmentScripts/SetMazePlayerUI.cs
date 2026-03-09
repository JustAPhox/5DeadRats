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

    public GameObject[] Item_UI_Icons;
    public Canvas This_Canvas;

    public Vector2 P1_Item_Pos;
    public Vector2 P2_Item_Pos;
    public Vector2 P3_Item_Pos;
    public Vector2 P4_Item_Pos;

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
            Set_Up(Player_1_Icon, Player_1_CheeseCount, Player1.GetComponent<MazePlayerController>(), P1_Item_Pos);

        if (Script.Player_Objects.Length > 0)
            Player2 = Script.Player_Objects[1];
            Player_2_UI_Pannel.SetActive(true);
            Set_Up(Player_2_Icon, Player_2_CheeseCount, Player2.GetComponent<MazePlayerController>(), P2_Item_Pos);

        if (Script.Player_Objects.Length > 1)
            Player3 = Script.Player_Objects[2];
            Player_3_UI_Pannel.SetActive(true);
            Set_Up(Player_3_Icon, Player_3_CheeseCount, Player3.GetComponent<MazePlayerController>(), P3_Item_Pos);

        if (Script.Player_Objects.Length > 2)
            Player4 = Script.Player_Objects[3];
            Player_4_UI_Pannel.SetActive(true);
            Set_Up(Player_4_Icon, Player_4_CheeseCount, Player4.GetComponent<MazePlayerController>(), P4_Item_Pos);
    }

    void Set_Up(GameObject Icon, GameObject Cheese_Count, MazePlayerController Script, Vector2 Item_Pos)
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
        else if(Script.playerCharacter == 5)
        {
            Player_Image.sprite = Steven_Icon;
        }

        TextMeshProUGUI Cheese_Text = Cheese_Count.GetComponent<TextMeshProUGUI>();
        Cheese_Text.text = string.Format("x {0}", Script.playerConfig.winPoints);

        Set_Items_UI(Script.playerConfig.playerItems, Item_Pos);
    }

    public void Set_Items_UI(List<string> Item_List, Vector2 Item_Pos)
    {
        //Test_Get_Item(Item_List);
        //Item_List.Add("stopwatch");//temporary
        //Item_List.Add("big_nuclear_bomb_that_kills_everyone");//temporary
        //Item_List.Add("teleporter");//temporary
        //Item_List.Add("pied_piper_pipe");//temporary
        //Item_List.Add("blindness");//temporary
        //Item_List.Add("rusty_syringe");//temporary
        Item_List.Add("cannibalistic_urges");//temporary

        Debug.Log(Item_List[0]);

        foreach (string Item in Item_List)
        {
            if (Item == "berserker_helmet")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[0], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "cannibalistic_urges")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[1], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "high_intensity_lubricant")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[2], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "holy_cheese")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[3], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "olive_oil")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[4], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "overclocked_pacemaker")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[5], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "stopwatch")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[6], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "big_nuclear_bomb_that_kills_everyone")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[7], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "teleporter")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[8], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "pied_piper_pipe")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[9], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "blindness")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[10], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "rusty_syringe")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[11], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "medicine_drug")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[12], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }
            else if (Item == "iv_drip")
            {
                GameObject Item_Icon = Instantiate(Item_UI_Icons[13], This_Canvas.transform);
                RectTransform Rect = Item_Icon.GetComponent<RectTransform>();
                Rect.anchoredPosition = Item_Pos;
            }

            Item_Pos.x = Item_Pos.x + 7;
        }
    }

    public void Test_Get_Item(List<string> Item_List)
    {
        for (int i = 0; i < 3; i++)
        {
            int Selection_Number = Random.Range(1, 5);

            if (Selection_Number == 1)
            {
                Item_List.Add("overclocked_pacemaker");//temporary
            }

            if (Selection_Number == 2)
            {
                Item_List.Add("berserker_helmet");//temporary
            }

            if (Selection_Number == 3)
            {
                Item_List.Add("cannibalistic_urges");//temporary
            }

            if (Selection_Number == 4)
            {
                Item_List.Add("holy_cheese");//temporary
            }
        }
    }
}
