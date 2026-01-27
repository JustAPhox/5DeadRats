using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    public GameObject titleScreen;
    public Slider QuizPlayerCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void openQuiz()
    {
        // Loads the scene
        SceneManager.LoadScene("Quiz Scene", LoadSceneMode.Additive);

        // Stores the playercount set via the slider
        gameManager.setPlayerCount(Convert.ToInt32(QuizPlayerCount.value));

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
