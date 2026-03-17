using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseLogic : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject backing;

    private int selection;

    [SerializeField]
    private Image[] buttons;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            
            buttons[selection].color = Color.white;


            backing.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;


            buttons[selection].color = Color.red;
            selection = 0;

            backing.SetActive(true);
        }
    }


    public void changeSelection()
    {
        buttons[selection].color = Color.white;


        if (selection == 0)
        {
            selection = 1;
        }
        else
        {
            selection = 0;
        }

        buttons[selection].color = Color.red;

    }

    public void selectSelection()
    {
        if (selection == 0)
        {
            TogglePause();
        }
        else
        {
            QuitGame();
        }
    }


    public void QuitGame()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}
