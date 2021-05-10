using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Animator animator;
    private PlayerWeaponRotation weaponRotation;
    public GameObject weaponOrigin;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        weaponRotation = weaponOrigin.GetComponent<PlayerWeaponRotation>();
        animator.SetFloat("Facing", weaponRotation.RotationAngle);
    }
}
