using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        moveToMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void moveToMaze()
    {
        Debug.Log($"Moving to maze Scene");

        SceneManager.LoadScene("2D Maze Scene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(3);
    }
}
