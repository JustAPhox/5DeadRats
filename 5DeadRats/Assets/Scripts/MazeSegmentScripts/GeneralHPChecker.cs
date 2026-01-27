using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHPChecker : MonoBehaviour
{
    public int HP_Threshold = 12;
    //private GameObject Player;
    private MazePlayerController Script;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindWithTag("Dammageable");
        Script = GetComponentInParent<MazePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Script.Current_HP < HP_Threshold)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
