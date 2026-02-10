using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCharacterMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI joinedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerJoined()
    {
        joinedText.SetText("Yes PLayer :D");
    }
}
