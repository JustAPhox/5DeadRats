using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject titleScreen;

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
