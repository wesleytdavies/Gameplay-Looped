using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowBolt : LoopableObject
{
    public static readonly string prefabName = "Crossbow Bolt"; //name of prefab in the Resources folder

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = .25f;
        movementFunction = Linear;
        DamagePerSecond = 5f;
    }
}
