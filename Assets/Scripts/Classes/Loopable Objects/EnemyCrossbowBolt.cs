﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrossbowBolt : LoopableObject
{
    public static readonly string prefabName = "Enemy Crossbow Bolt"; //name of prefab in the Resources folder

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = .5f;
        movementFunction = Linear;
        DamagePerFrame = 5f;
    }
}
