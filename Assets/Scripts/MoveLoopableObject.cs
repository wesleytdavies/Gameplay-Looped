using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLoopableObject : MonoBehaviour //moves a loopable object
{
    private LoopableObject loopableObject;
    public PistolBullet pistolBullet = new PistolBullet();

    void Start()
    {
        
    }

    void Update()
    {
        loopableObject.MoveForward();
    }
}
