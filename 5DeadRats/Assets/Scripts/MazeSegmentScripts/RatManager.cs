using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player_Prefab;
    private GameObject[] Player_Objects;

    private int Player_Count;
    
    // Start is called before the first frame update
    void Start()
    {
        var PlayerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        Player_Count = PlayerConfigs.Length;
        Player_Objects = new GameObject[Player_Count];

        for (int i = 0; i < PlayerConfigs.Length; i++)
        {
            var player = Instantiate(Player_Prefab);

            player.GetComponent<MazePlayerController>().intitialisePlayer(PlayerConfigs[i]);

            Player_Objects[i] = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
