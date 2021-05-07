using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Animator animator;
    private PlayerWeaponRotation weaponRotation;
    [SerializeField] private GameObject weaponOrigin;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponRotation = weaponOrigin.GetComponent<PlayerWeaponRotation>();
    }

    void Update()
    {
        animator.SetFloat("Facing", weaponRotation.RotationAngle);
    }
}
