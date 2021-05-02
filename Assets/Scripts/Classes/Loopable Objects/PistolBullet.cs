using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : LoopableObject
{
    public override void Initialize()
    {
        StartPosition = transform.position;
        StartDirection = Vector2.right;
        Speed = .05f;
        MovementFunction = Linear;
    }
}
