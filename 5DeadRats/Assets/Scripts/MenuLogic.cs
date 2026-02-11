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


    public void startGame()
    {
        // Hides title screen
        titleScreen.SetActive(false);

        // Opens Character Menu
        characterMenu.SetActive(true);

        PlayerConfigManager.GetComponent<PlayerConfigManager>().allowJoining(true);
    }

    public void charactersChoosen()
    {
        // Hides title screen
        Debug.Log($"Title Screen Before: {titleScreen.activeSelf}");
        titleScreen.SetActive(true);
        Debug.Log($"Title Screen After: {titleScreen.activeSelf}");
        // Opens Character Menu
        Debug.Log($"Character Screen Before: {characterMenu.activeSelf}");
        characterMenu.SetActive(false);
        Debug.Log($"Character Screen After: {characterMenu.activeSelf}");


    }


    public void openQuiz()
    {
        // Loads the scene
        SceneManager.LoadScene("Quiz Scene", LoadSceneMode.Additive);

        // Hides title screen
        titleScreen.SetActive(false);
    }



    public void openMaze()
    {
        // Loads the scene
        SceneManager.LoadScene("Maze Scene", LoadSceneMode.Additive);

        // Hides title screen
        titleScreen.SetActive(false);
    }

}
