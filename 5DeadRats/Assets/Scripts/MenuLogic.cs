using System;
using System.Collections;
using System.Collections.Generic;
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


    public void openQuiz()
    {
        // Loads the scene
        SceneManager.LoadScene("Quiz Scene", LoadSceneMode.Additive);

        menuCamera.enabled = false;
        menuAudioListener.enabled = false;


        // Hides title screen
        titleScreen.SetActive(false);
    }



    public void openMaze()
    {
        // Loads the scene
        SceneManager.LoadScene("2D Maze Scene", LoadSceneMode.Additive);

        menuCamera.enabled = false;
        menuAudioListener.enabled = false;


        // Hides title screen
        titleScreen.SetActive(false);
    }

}
