using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using System.Linq.Expressions;

public class MazePlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;
    private PlayerControls controls;
    public int playerCharacter;
    
    public float Speed = 5f;
    public Vector2 Movement_Input;
    public Vector2 Facing_Direction = Vector2.down;//default direction
    public float Attack_Range = 1.5f;
    public int Base_Dammage = 1;

    private Rigidbody2D rb;
    [ColorUsageAttribute(true, true)]
    public Color Flash_Colour = Color.red;
    [ColorUsageAttribute(true, true)]
    public Color Flash_Swap_Colour = Color.white;
    [SerializeField] public Color Ruby_Colour;
    [SerializeField] public Color Pablo_Colour;
    [SerializeField] public Color Winona_Colour;
    [SerializeField] public Color John_Colour;
    [SerializeField] public Color Steven_Colour;
    [ColorUsageAttribute(true, true)]
    public Color Flash_Heal_Colour = Color.green;
    private float Flash_Time = 1f;
    private SpriteRenderer Sprite_Renderer;
    private Material material;
    private Coroutine Dammage_Flash_Coroutine;

    public float Invincibility_Time = 20.5f;
    public bool Is_Invicible = false;
    private Coroutine Invicibility_Coroutine;

    public float Stun_Time = 20f;
    private bool Is_Stunned = false;
    private Coroutine Stun_Coroutine;

    public AudioSource Bite_Noise;
    public AudioSource[] Bite_Noises;
    public AudioSource Ouch_Noise;
    public AudioSource[] Ouch_Noises;
    public AudioSource[] Death_Noises;

    public ParticleSystem Hurt_Particles;

    public int Max_HP = 12;
    public int Current_HP;

    public bool Is_Speed_Buffed = false;
    public bool Is_Dammage_Buffed = false;

    public int Crit_Chance = 0;

    private int Berserk_Dammage = 0;
    private int Revives = 0;

    private bool Can_Have_Heart_Attack = false;
    private int Heart_Attack_Value = 0;

    private Light2D Light_Refference;

    private bool Used_Active = false;

    private Canvas This_Canvas;
    public GameObject Explosion_UI;
    public Vector2 Zero_Zero;


    RaycastHit2D[] Hit_Buffer = new RaycastHit2D[16];// this is the number things that can be hit by the attack raycast in 1 attack

    private CinemachineImpulseSource Impulse_Source;

    void Awake()
    {
        controls = new PlayerControls();

        rb = GetComponent<Rigidbody2D>();
        Sprite_Renderer = GetComponent<SpriteRenderer>();
        material = Sprite_Renderer.material;

        Impulse_Source = GetComponent<CinemachineImpulseSource>();
    }

    void FixedUpdate()
    {
        Vector2 movement = Movement_Input * Speed * Time.fixedDeltaTime;
        if (Is_Stunned == false)
        {
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        Update_Sprite_Rotation();

        if (Can_Have_Heart_Attack == true && Current_HP > 0)
        {
            if (Random.Range(0, 250) <= Heart_Attack_Value)
            {
                Take_Dammage(Current_HP);
                Call_Stun_Frames();
                Call_Dammage_Flash(Flash_Colour);
                Call_Invincibilty_Frames();
                Spawn_Hurt_Particles();
            }
            Call_Heart_Attack_Chance();
        }
    }

    public void intitialisePlayer(PlayerConfig config)
    {
        playerConfig = config;
        playerCharacter = config.playerCharacter;
        playerConfig.playerInput.onActionTriggered += PlayerInput_onActionTriggered;

        playerConfig.playerInput.SwitchCurrentActionMap("Maze");

        Max_HP = Max_HP + playerConfig.playerHealthStat;

        Current_HP = Max_HP;

        Base_Dammage = Base_Dammage + playerConfig.playerDammageStat;

        Speed = Speed + playerConfig.playerSpeedStat;

        Crit_Chance = Crit_Chance + playerConfig.playerCritStat;

        Light_Refference = GetComponentInChildren<Light2D>();

        Light_Refference.pointLightInnerRadius = Light_Refference.pointLightInnerRadius + playerConfig.playerVisionStat;
        Light_Refference.pointLightOuterRadius = Light_Refference.pointLightOuterRadius + playerConfig.playerVisionStat;

        if (playerConfig.playerCharacter == 0)
        {
            material.SetColor("_CharacterColour", Ruby_Colour);
        }
        else if (playerConfig.playerCharacter == 1)
        {
            material.SetColor("_CharacterColour", Ruby_Colour);//ruby
        }
        else if (playerConfig.playerCharacter == 2)
        {
            material.SetColor("_CharacterColour", Pablo_Colour);//pablo
        }
        else if (playerConfig.playerCharacter == 3)
        {
            material.SetColor("_CharacterColour", Winona_Colour);//winnona
        }
        else if (playerConfig.playerCharacter == 4)
        {
            material.SetColor("_CharacterColour", John_Colour);//jhon
        }
        else if (playerConfig.playerCharacter == 5)
        {
            material.SetColor("_CharacterColour", Steven_Colour);//Steven
        }
    }

    public void Start()
    {
        This_Canvas = FindAnyObjectByType<Canvas>();
        
        if (playerConfig.playerItems.IndexOf("holy_cheese") != -1)
        {
            foreach (string Item in playerConfig.playerItems)
            {
                if (Item == "holy_cheese")
                {
                    Revives = Revives + 1;
                    Debug.Log(Revives);
                }
            }
        }

        if (playerConfig.playerItems.IndexOf("overclocked_pacemaker") != -1)
        {
            foreach (string Item in playerConfig.playerItems)
            {
                if (Item == "overclocked_pacemaker")
                {
                    Base_Dammage = Base_Dammage + 3;
                    Max_HP = Max_HP + 3;

                    if (Max_HP > 24)
                    {
                        Max_HP = 24;
                    }

                    Current_HP = Max_HP;
                    Speed = Speed + 3;
                    Crit_Chance = Crit_Chance + 3;
                    Can_Have_Heart_Attack = true;
                    Heart_Attack_Value = Heart_Attack_Value + 1;
                }
            }
        }
    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        if(context.action.name == controls.Maze.Attack.name)
        {
            OnAttack(context);
        }
        else if (context.action.name == controls.Maze.Move.name)
        {
            OnMove(context);
        }
        else if(context.action.name == controls.Maze.Active.name)
        {
            OnActive(context);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (Is_Stunned == false)
        {
            Movement_Input = ctx.ReadValue<Vector2>();
            if (Movement_Input != Vector2.zero)
            {
                Facing_Direction = Movement_Input.normalized;
            }
        }
    }

    public void OnActive(InputAction.CallbackContext ctx)
    {
        if (Is_Stunned == false && Used_Active == false && Current_HP > 0)
        {
            if (playerConfig.playerItems.IndexOf("medicine_drug") != -1)
            {
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "medicine_drug" && Current_HP + 1 <= Max_HP)
                    {
                        Current_HP = Current_HP + 1;
                    }
                }
                Call_Dammage_Flash(Flash_Heal_Colour);
            }

            if (playerConfig.playerItems.IndexOf("rusty_syringe") != -1)
            {
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "rusty_syringe" && Current_HP + 3 <= Max_HP)
                    {
                        Current_HP = Current_HP + 3;
                    }
                }
                Call_Dammage_Flash(Flash_Heal_Colour);
            }

            if (playerConfig.playerItems.IndexOf("blindness") != -1)
            {
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "blindness")
                    {
                        bool Is_Player1 = false;
                        bool Is_Player2 = false;
                        bool Is_Player3 = false;
                        bool Is_Player4 = false;

                        GameObject Player_Manager = GameObject.Find("PlayerManager");
                        RatManager Script = Player_Manager.GetComponent<RatManager>();

                        if (Script.Player_Objects.Length > 0)
                        {
                            Is_Player1 = true;

                            if (gameObject == Script.Player_Objects[0])
                            {
                                Is_Player1 = false;
                            }
                        }

                        if (Script.Player_Objects.Length > 1)
                        {
                            Is_Player2 = true;

                            if (gameObject == Script.Player_Objects[1])
                            {
                                Is_Player2 = false;
                            }
                        }

                        if (Script.Player_Objects.Length > 2)
                        {
                            Is_Player3 = true;

                            if (gameObject == Script.Player_Objects[2])
                            {
                                Is_Player3 = false;
                            }
                        }

                        if (Script.Player_Objects.Length > 3)
                        {
                            Is_Player4 = true;

                            if (gameObject == Script.Player_Objects[3])
                            {
                                Is_Player4 = false;
                            }
                        }

                        bool Check = false;
                        
                        while (Check == false)
                        {
                            int Random_Interger = Random.Range(0, 4);

                            if (Random_Interger == 0 && Is_Player1 == true)
                            {
                                Light2D Player_Light = Script.Player_Objects[0].GetComponentInChildren<Light2D>();
                                Player_Light.pointLightInnerRadius = 0;
                                Player_Light.pointLightOuterRadius = 0;
                                Check = true;
                            }
                            else if (Random_Interger == 1 && Is_Player2 == true)
                            {
                                Light2D Player_Light = Script.Player_Objects[1].GetComponentInChildren<Light2D>();
                                Player_Light.pointLightInnerRadius = 0;
                                Player_Light.pointLightOuterRadius = 0;
                                Check = true;
                            }
                            else if (Random_Interger == 2 && Is_Player3 == true)
                            {
                                Light2D Player_Light = Script.Player_Objects[2].GetComponentInChildren<Light2D>();
                                Player_Light.pointLightInnerRadius = 0;
                                Player_Light.pointLightOuterRadius = 0;
                                Check = true;
                            }
                            else if (Random_Interger == 3 && Is_Player4 == true)
                            {
                                Light2D Player_Light = Script.Player_Objects[3].GetComponentInChildren<Light2D>();
                                Player_Light.pointLightInnerRadius = 0;
                                Player_Light.pointLightOuterRadius = 0;
                                Check = true;
                            }
                        }
                    }
                }
            }

            if (playerConfig.playerItems.IndexOf("pied_piper_pipe") != -1)
            {
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "pied_piper_pipe")
                    {
                        GameObject Player_Manager = GameObject.Find("PlayerManager");
                        RatManager Script = Player_Manager.GetComponent<RatManager>();

                        Transform Current_Pos = gameObject.transform;

                        for (int i = 0; i < Script.Player_Objects.Length; i++)
                        {
                            if (gameObject != Script.Player_Objects[i])
                            {
                                Vector2 Direction = (Current_Pos.position - Script.Player_Objects[i].transform.position).normalized;
                                Rigidbody2D Rat_Rigidbody = Script.Player_Objects[i].GetComponent<Rigidbody2D>();
                                Rat_Rigidbody.AddForce(Direction * 5000);
                            }
                        }
                    }
                }
            }

            if (playerConfig.playerItems.IndexOf("teleporter") != -1)
            {
                GameObject Player_Manager = GameObject.Find("PlayerManager");
                RatManager Script = Player_Manager.GetComponent<RatManager>();

                List<Transform> Rat_Transforms = new List<Transform>();
                List<Vector3> Original_Positions = new List<Vector3>();

                // Store transforms and their positions
                foreach (GameObject Rat in Script.Player_Objects)
                {
                    Rat_Transforms.Add(Rat.transform);
                    Original_Positions.Add(Rat.transform.position);
                    Rat.GetComponent<MazePlayerController>().Call_Dammage_Flash(Flash_Swap_Colour);
                }

                // Shuffle positions (Fisher-Yates shuffle)
                for (int i = 0; i < Original_Positions.Count; i++)
                {
                    int Random_Index = Random.Range(i, Original_Positions.Count);

                    Vector3 temp = Original_Positions[i];
                    Original_Positions[i] = Original_Positions[Random_Index];
                    Original_Positions[Random_Index] = temp;
                }

                // Assign shuffled positions back
                for (int i = 0; i < Rat_Transforms.Count; i++)
                {
                    Rat_Transforms[i].position = Original_Positions[i];
                }
            }

            if (playerConfig.playerItems.IndexOf("big_nuclear_bomb_that_kills_everyone") != -1)
            {
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "big_nuclear_bomb_that_kills_everyone")
                    {
                        GameObject Player_Manager = GameObject.Find("PlayerManager");
                        RatManager Script = Player_Manager.GetComponent<RatManager>();

                        foreach (GameObject Rat in Script.Player_Objects)
                        {
                            MazePlayerController Player_Script = Rat.GetComponent<MazePlayerController>();
                            Player_Script.Play_Sound_From_Array(Ouch_Noises, 0.7f, 1f);
                            Player_Script.Take_Dammage(Player_Script.Current_HP);
                            Player_Script.Call_Stun_Frames();
                            Player_Script.Call_Dammage_Flash(Flash_Colour);
                            Player_Script.Call_Invincibilty_Frames();
                            Player_Script.Spawn_Hurt_Particles();
                        }
                        GameObject Explosion = Instantiate(Explosion_UI, This_Canvas.transform);
                        RectTransform Rect = Explosion.GetComponent<RectTransform>();
                        Rect.anchoredPosition = Zero_Zero;
                    }
                }

                playerConfig.playerItems.RemoveAll(i => i == "big_nuclear_bomb_that_kills_everyone");
            }

            if (playerConfig.playerItems.IndexOf("stopwatch") != -1)
            {
                int Count = 0;
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "stopwatch")
                    {
                        This_Canvas.GetComponent<MazeTimerScript>().Remaining_Time = 1;
                        break;
                    }
                    Count++;
                }
                playerConfig.playerItems.RemoveAt(Count);
            }

                Used_Active = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (Is_Stunned == false)
        {
            Nibble();
        }
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
                    if (Script.Is_Invicible == false && Script.Current_HP > 0)
                    {  
                        int Actual_Dammage = Base_Dammage;

                        Actual_Dammage = Base_Dammage + Berserk_Dammage;
                        if(Random.Range(1, 20) <= Crit_Chance)
                        {
                            Actual_Dammage = Base_Dammage * 2;
                        }
                        
                        if (Current_HP > 0)
                        {
                            if (Script.Current_HP > Actual_Dammage)
                            {
                                Script.Play_Sound_From_Array(Ouch_Noises, 0.7f, 1f);
                            }
                            else
                            {
                                Script.Play_Sound_From_Array(Death_Noises, 15.5f, 15.8f);
                            }
                            Script.Take_Dammage(Actual_Dammage);
                            Canabalistic_Urges_Effect();
                            //Script.Hit_Knockback(Attack_Direction);
                            Script.Call_Stun_Frames();
                            Script.Call_Dammage_Flash(Flash_Colour);
                            Script.Call_Invincibilty_Frames();
                            Script.Spawn_Hurt_Particles();
                        }
                        else
                        {
                            if (Script.Current_HP > 1)
                            {
                                Script.Play_Sound_From_Array(Ouch_Noises, 0.7f, 1f);
                                Script.Take_Dammage(1);
                                //Script.Hit_Knockback(Attack_Direction);
                                Script.Call_Stun_Frames();
                                Script.Call_Dammage_Flash(Flash_Colour);
                                Script.Call_Invincibilty_Frames();
                                Script.Spawn_Hurt_Particles();
                            }
                            else
                            {
                                Script.Play_Sound_From_Array(Death_Noises, 15.5f, 15.8f);
                                int HP_Storage = Script.Current_HP;
                                Vector2 Position_Storage = Hit.collider.transform.position;
                                Script.Current_HP = Current_HP;
                                Current_HP = HP_Storage;
                                Hit.collider.transform.position = transform.position;
                                transform.position = Position_Storage;
                                Script.Make_Ghost();
                                Make_Not_Ghost();
                                Call_Invincibilty_Frames();
                                Script.Call_Dammage_Flash(Flash_Swap_Colour);
                                Call_Dammage_Flash(Flash_Swap_Colour);
                                Script.Spawn_Hurt_Particles();
                            }
                        }
                    }
                }

                break;//makes it stop at first valid target, might be removed later so can hit more than 1 rat at a time
            }
        }

        GameObject Canvas = GameObject.Find("Canvas");
        MazeTimerScript Timer_Script = Canvas.GetComponent<MazeTimerScript>();
        if(Timer_Script.Delay_Over == true)
        {
            Timer_Script.openQuiz();
        }
    }

    public void Make_Ghost()
    {
        gameObject.layer = 6;

        material.SetFloat("_Transparency", 0.52f);

        Speed = 3.5f;
    }

    public void Make_Not_Ghost()
    {
        gameObject.layer = 0;

        material.SetFloat("_Transparency", 1f);

        Speed = 5f;

        if (Is_Speed_Buffed == true)
        {
            Speed = Speed + 1f;
        }
    }

    public void Take_Dammage(int Dammage_Amount)
    {
        CameraShakeManagerScript.instance.CameraShake(Impulse_Source);
        
        if (Current_HP <= Dammage_Amount)
        {
            Current_HP = 0;
        }
        else
        {
            Current_HP = Current_HP - Dammage_Amount;
        }

        if (Current_HP == 0)
        {
            if(Revives <= 0)
            {
                Make_Ghost();
            }
            else
            {
                Revives = Revives - 1;
                int Count = 0;
                foreach (string Item in playerConfig.playerItems)
                {
                    if (Item == "holy_cheese")
                    {
                        playerConfig.playerItems.RemoveAt(Count);
                        break;
                    }
                    Count++;
                }
                Current_HP = Max_HP;
            }
        }

        if (playerConfig.playerItems.IndexOf("berserker_helmet") != -1)
        {
            foreach (string Item in playerConfig.playerItems)
            {
                if (Item == "berserker_helmet")
                {
                    Berserk_Dammage = Berserk_Dammage + 1;
                }
            }
        }
    }
    
    public void Hit_Knockback(Vector2 Knockback_Direction)
    {
        rb.AddForce(Knockback_Direction.normalized * 1500f, ForceMode2D.Force);
    }

    public void Call_Stun_Frames()
    {
        Stun_Coroutine = StartCoroutine(Stun_Frames());
    }

    private IEnumerator Stun_Frames()
    {
        //yield return new WaitForSeconds(5f);
        //Movement_Input = Vector2.zero;
        //rb.velocity = Vector2.zero;
        rb.simulated = false;
        Is_Stunned = true;
        yield return new WaitForSeconds(Stun_Time);
        rb.simulated = true;
        Is_Stunned = false;
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
    public void Call_Dammage_Flash(Color Colour_Select)
    {
        Dammage_Flash_Coroutine = StartCoroutine(Dammage_Flasher(Colour_Select));
    }
    private IEnumerator Dammage_Flasher(Color Colour_Select)
    {
        material.SetColor("_FlashColour", Colour_Select);

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

    public void Update_Sprite_Rotation()
    {
        if (Facing_Direction == Vector2.zero)
            return;

        float Angle = Mathf.Atan2(-Facing_Direction.x, Facing_Direction.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, Angle);
    }

    public void Play_Sound_From_Array(AudioSource[] Sound_Effect_Array, float Min_Volume_Variation, float Max_Volume_Variation)
    {
        int Sound_Effect_Location = Random.Range(0, Sound_Effect_Array.Length - 1);
        AudioSource Sound_Effect = Sound_Effect_Array[Sound_Effect_Location];
        Sound_Effect.volume = Random.Range(Min_Volume_Variation, Max_Volume_Variation);
        Sound_Effect.pitch = Random.Range(0.7f, 1.0f);
        Sound_Effect.Play();
    }

    public void Spawn_Hurt_Particles()
    {
        Instantiate(Hurt_Particles, transform.position, transform.rotation);
    }

    public void Add_Win_Point()
    {
        playerConfig.winPoints = playerConfig.winPoints + 1;
    }

    public void Canabalistic_Urges_Effect()
    {
        if (playerConfig.playerItems.IndexOf("cannibalistic_urges") != -1)
        {
            foreach (string Item in playerConfig.playerItems)
            {
                if (Item == "cannibalistic_urges" && Current_HP + 1 <= Max_HP)
                {
                    Current_HP = Current_HP + 1;
                }
            }

            Call_Dammage_Flash(Flash_Colour);
        }
    }

    public void Call_Heart_Attack_Chance()
    {
        StartCoroutine(Heart_Attack_Chance());
    }

    private IEnumerator Heart_Attack_Chance()
    {
        Can_Have_Heart_Attack = false;
        yield return new WaitForSeconds(Random.Range(1, 10));
        Can_Have_Heart_Attack = true;
    }
}
