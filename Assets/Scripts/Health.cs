using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHitPoints;
    public float currentHitPoints;
    private static readonly int damagingLayer = 8;

    private SpriteRenderer spriteRenderer;
    private Color baseColor; //the normal color of the sprite
    private Color hitColor; //the color the sprite turns when hit

    void Start()
    {
        currentHitPoints = maxHitPoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        hitColor = Color.red;
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
                currentHitPoints -= collision.GetComponent<LoopableObject>().DamagePerFrame;
                spriteRenderer.color = hitColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = baseColor;
    }
}
