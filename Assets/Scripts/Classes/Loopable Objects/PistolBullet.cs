using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : LoopableObject
{
    public static readonly string prefabName = "Pistol Bullet"; //name of prefab in the Resources folder

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = .25f;
        movementFunction = Linear;
        DamagePerSecond = 1f;
        if (isPowered)
        {
            Speed *= powerShotFactor;
            DamagePerSecond *= powerShotFactor;
        }
    }
}
