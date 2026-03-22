using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player_Prefab;
    public GameObject[] Player_Objects;

    private int Player_Count;

    public Transform[] Spawn_Points;

    private Transform Spawn_Location;

    private List<Transform> Selected_Spawn_Points = new List<Transform>();
    [SerializeField]
    private GameObject Pause_Menu;
    
    // Start is called before the first frame update
    void Start()
    {
        var PlayerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        Player_Count = PlayerConfigs.Length;
        Player_Objects = new GameObject[Player_Count];

        for (int i = 0; i < PlayerConfigs.Length; i++)
        {
            do
            {
                Spawn_Location = Spawn_Points[Random.Range(0, Spawn_Points.Length)];
            }
            
            while(Selected_Spawn_Points.Contains(Spawn_Location));

            Selected_Spawn_Points.Add(Spawn_Location);
            
            var player = Instantiate(
                Player_Prefab,
                Spawn_Location.position,
                Spawn_Location.rotation
            );

            player.GetComponent<MazePlayerController>().Pause_Menu = Pause_Menu;
            player.GetComponent<MazePlayerController>().intitialisePlayer(PlayerConfigs[i]);

            Player_Objects[i] = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
