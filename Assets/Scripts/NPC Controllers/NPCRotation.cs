using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotation : MonoBehaviour
{
    private Animator animator;
    private NPCWeaponRotation weaponRotation;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponRotation = GetComponentInChildren<NPCWeaponRotation>();
    }

    void Update()
    {
        animator.SetFloat("Facing", weaponRotation.RotationAngle);
    }
}
