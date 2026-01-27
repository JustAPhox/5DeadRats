using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRegistration : MonoBehaviour
{
    public float weight = 1f;
    public float radius = 1f;

    private CinemachineTargetGroup Target_Group;
    
    // Start is called before the first frame update
    void Start()
    {
        Target_Group = FindObjectOfType<CinemachineTargetGroup>();
        Target_Group.AddMember(transform, weight, radius);
    }

    
    void OnDestroy()
    {
        if (Target_Group != null)
        {
            Target_Group.RemoveMember(transform);
        }
    }
}
