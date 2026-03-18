using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Toggle debugMode;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject exitButton;

    private void Start()
    {
        startButton.GetComponent<Button>().Select();
    }


    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != startButton  && EventSystem.current.currentSelectedGameObject != exitButton)
        {
            startButton.GetComponent<Button>().Select();
        }
    }


    public void startGame()
    {
        Debug.Log("Start Game Activated");

        // Hides title screen
        titleScreen.SetActive(false);

        // Opens Character Menu
        characterMenu.SetActive(true);

        PlayerConfigManager.GetComponent<PlayerConfigManager>().SetDebugMode(debugMode.isOn);

        PlayerConfigManager.GetComponent<PlayerConfigManager>().allowJoining(true);
    }

    public void charactersChoosen()
    {
        // If debug mode is on go back to menu
        if (PlayerConfigManager.GetComponent<PlayerConfigManager>().GetDebugMode())
        {
            // Opens title screen
            titleScreen.SetActive(true);
            // Hides Character Menu
            characterMenu.SetActive(false);
        }
        // If not it goes to quiz
        else
        {
            openQuiz();
        }


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


    public void closeGame()
    {
        Application.Quit();
    }


    public void openMaze()
    {
        // Loads the scene
        SceneManager.LoadScene("Maze1");
    }

}


