using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerCharacterMenu;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerJoined(int playerIndex)
    {
        playerCharacterMenu[playerIndex].GetComponent<PlayerCharacterMenu>().PlayerJoined();
    }
}
