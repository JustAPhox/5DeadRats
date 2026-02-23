using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFenceTrigger : MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    public Color Flash_Colour = Color.yellow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dammageable"))
        {
            MazePlayerController Script = collision.GetComponent<MazePlayerController>();
            if(Script != null)
            {
                if (Script.Is_Invicible == false && Script.Current_HP > 0)
                {
                    if (Script.Current_HP > 1)
                        {
                            Script.Play_Sound_From_Array(Script.Ouch_Noises, 0.7f, 1f);
                        }
                        else
                        {
                            Script.Play_Sound_From_Array(Script.Death_Noises, 15.5f, 15.8f);
                        }
                        Script.Take_Dammage(1);
                        Script.Call_Stun_Frames();
                        Script.Call_Dammage_Flash(Flash_Colour);
                        Script.Call_Invincibilty_Frames();
                        Script.Spawn_Hurt_Particles();
                }
            }
        }
    }
}
