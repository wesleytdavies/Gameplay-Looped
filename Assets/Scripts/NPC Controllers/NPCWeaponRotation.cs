using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWeaponRotation : MonoBehaviour
{
    public float RotationAngle
    {
        get => _rotationAngle;
        private set => _rotationAngle = value;
    }
    private float _rotationAngle;

    private GameObject holder;
    private Weapon weapon;
    private SpriteRenderer weaponSpriteRenderer;
    private Unit unit;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        holder = weapon.holder;
        unit = holder.GetComponent<Unit>();
    }

    void Update()
    {
        if (holder != null)
        {
            Vector2 targetPosition = unit.target.position;
            targetPosition.x -= transform.position.x;
            targetPosition.y -= transform.position.y;
            RotationAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationAngle));

            //put sprite either behind or in front of NPC
            if (RotationAngle >= 0 && RotationAngle <= 180)
            {
                weaponSpriteRenderer.sortingOrder = -1;
            }
            else
            {
                weaponSpriteRenderer.sortingOrder = 1;
            }
        }
    }

    private void LateUpdate()
    {
        if (holder != null)
        {
            transform.position = holder.transform.position;
        }
    }
}
