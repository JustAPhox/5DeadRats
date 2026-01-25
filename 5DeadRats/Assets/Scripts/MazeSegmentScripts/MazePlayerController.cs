using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazePlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 Movement_Input;
    public Vector2 Facing_Direction = Vector2.down;//default direction
    public float Attack_Range = 1.5f;

    private Rigidbody2D rb;
    [ColorUsageAttribute(true, true)]
    public Color Flash_Colour = Color.red;
    private float Flash_Time = 0.25f;
    private SpriteRenderer Sprite_Renderer;
    private Material material;
    private Coroutine Dammage_Flash_Coroutine;

    public float Invincibility_Time = 1f;
    private bool Is_Invicible = false;
    private Coroutine Invicibility_Coroutine;

    public AudioSource Bite_Noise;
    public AudioSource Ouch_Noise;


    RaycastHit2D[] Hit_Buffer = new RaycastHit2D[16];// this is the number things that can be hit by the attack raycast in 1 attack

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Sprite_Renderer = GetComponent<SpriteRenderer>();
        material = Sprite_Renderer.material;

    }

    void FixedUpdate()
    {
        Vector2 movement = Movement_Input * Speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
    
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Movement_Input = ctx.ReadValue<Vector2>();

        if (Movement_Input != Vector2.zero)
        {
            Facing_Direction = Movement_Input.normalized;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Nibble();
    }

    void Nibble()
    {
        Vector2 Attack_Direction = Facing_Direction;
        Vector2 Attack_Origin = (Vector2)transform.position + Attack_Direction * 0.05f;// this is to reduce the amount of selfhits

        Debug.DrawRay(
            Attack_Origin,
            Attack_Direction * Attack_Range,
            Color.red,
            0.2f
        );

        int Hit_Count = Physics2D.RaycastNonAlloc(
            Attack_Origin,
            Attack_Direction,
            Hit_Buffer,
            Attack_Range
        );

        Bite_Noise.Play();

        for (int i = 0; i < Hit_Count; i++)
        {
            RaycastHit2D Hit = Hit_Buffer[i];

            if (Hit.collider.transform.root == transform.root)
                continue;//makes it so it skips itself if it hit itself
            
            if (Hit.collider.CompareTag("Dammageable"))
            {
                Debug.Log(Hit.collider.name);
                MazePlayerController Script = Hit.collider.GetComponent<MazePlayerController>();

                if (Script != null)
                {
                    if (Script.Is_Invicible == false)
                    {  
                            Script.Play_Ouch_Sound();
                            Script.Hit_Knockback(Attack_Direction);
                            Script.Call_Dammage_Flash();
                            Script.Call_Invincibilty_Frames();
                    }
                }

                break;//makes it stop at first valid target, might be removed later so can hit more than 1 rat at a time
            }
        }
    }

    public void Hit_Knockback(Vector2 Knockback_Direction)
    {
        rb.AddForce(Knockback_Direction.normalized * 1000f, ForceMode2D.Force);
    }
    public void Call_Invincibilty_Frames()
    {
        Invicibility_Coroutine = StartCoroutine(Invincibility_Frames());
    }
    private IEnumerator Invincibility_Frames()
    {
        Is_Invicible = true;
        yield return new WaitForSeconds(Invincibility_Time);
        Is_Invicible = false;
    }
    public void Call_Dammage_Flash()
    {
        Dammage_Flash_Coroutine = StartCoroutine(Dammage_Flasher());
    }
    private IEnumerator Dammage_Flasher()
    {
        material.SetColor("_FlashColour", Flash_Colour);

        float Current_Flash_Amount = 0f;
        float Elapsed_Time = 0f;

        while (Elapsed_Time < Flash_Time)
        {
            Elapsed_Time += Time.deltaTime;

            Current_Flash_Amount = Mathf.Lerp(1f, 0f, (Elapsed_Time / Flash_Time));
            material.SetFloat("_FlashAmount", Current_Flash_Amount);

            yield return null;
        }
    }

    public void Play_Ouch_Sound()
    {
        Ouch_Noise.Play();
    }
}
