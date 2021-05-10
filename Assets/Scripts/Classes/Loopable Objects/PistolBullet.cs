using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : LoopableObject
{
    public static readonly string prefabName = "Pistol Bullet"; //name of prefab in the Resources folder

    public override void Initialize()
    {
        Speed = 0.5f;
        movementFunction = Linear;
        DamagePerFrame = 1f;
    }
}
