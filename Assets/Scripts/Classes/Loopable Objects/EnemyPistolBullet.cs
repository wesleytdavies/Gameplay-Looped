using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistolBullet : LoopableObject
{
    public static readonly string prefabName = "Enemy Pistol Bullet"; //name of prefab in the Resources folder

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = .25f;
        movementFunction = Linear;
        DamagePerFrame = 1f;
    }
}
