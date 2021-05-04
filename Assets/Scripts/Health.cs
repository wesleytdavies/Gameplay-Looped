using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHitPoints;
    public float currentHitPoints;
    private static readonly int damagingLayer = 8;

    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == damagingLayer)
        {
            if (collision.GetComponent<LoopableObject>() != null)
            {
                currentHitPoints -= collision.GetComponent<LoopableObject>().DamagePerSecond;
            }
        }
    }
}
