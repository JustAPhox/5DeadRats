using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManagerScript : MonoBehaviour
{
    public static CameraShakeManagerScript instance;
    [SerializeField] private float Global_Shake_Force = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource Impulse_Source)
    {
        Impulse_Source.GenerateImpulseWithForce(Global_Shake_Force);
    }
}
