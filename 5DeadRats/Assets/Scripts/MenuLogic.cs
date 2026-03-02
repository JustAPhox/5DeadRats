using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject titleScreen;
    [SerializeField]
    private GameObject characterMenu;

    [SerializeField]
    private GameObject PlayerConfigManager;

    [SerializeField]
    private Camera menuCamera;
    [SerializeField]
    private AudioListener menuAudioListener;

    [SerializeField]
    TextAsset jsonFile;

    public void startGame()
    {
        Debug.Log("Start Game Activated");

        // Hides title screen
        titleScreen.SetActive(false);

        // Opens Character Menu
        characterMenu.SetActive(true);

        PlayerConfigManager.GetComponent<PlayerConfigManager>().allowJoining(true);
    }

    public void charactersChoosen()
    {
        // Opens title screen
        titleScreen.SetActive(true);
        // Hides Character Menu
        characterMenu.SetActive(false);


    }


    public void readjSON()
    {
        JsonUtility.FromJson<List<ItemInfo>>(jsonFile.text);


        

        //Debug.Log(JsonUtility.FromJson<List<ItemInfo>>(jsonFile.text));
    }

    public void openQuiz()
    {
        // Loads the scene
        SceneManager.LoadScene("Quiz Scene");
    }


    public void openItemMenu()
    {
        // Loads the scene
        SceneManager.LoadScene("Item Selection Scene");
    }



    public void openMaze()
    {
        // Loads the scene
        SceneManager.LoadScene("Maze1");
    }

}

[Serializable]
public class ItemInfo
{
    ItemInfo() 
    {
        name = "Name";
        code = "code";
        description = "Stuff About the Item";

        health = 0;
        damage = 0;
        speed = 0;
        vision = 0;
        crit = 0;    
    }  


    public string name;
    public string code;
    public string description;

    public int health;
    public int damage;
    public int speed;
    public int vision;
    public int crit;
}