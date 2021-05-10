using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Health health;
    private GameObject healthBar;
    private RectTransform healthBarTransform;

    void Start()
    {
        health = GetComponent<Health>();
        healthBar = GameObject.Find("Health Bar");
        healthBarTransform = healthBar.GetComponent<RectTransform>();
    }

    void Update()
    {
        healthBarTransform.sizeDelta = new Vector2(healthBarTransform.sizeDelta.x, Mathf.Lerp(0, 100, health.currentHitPoints / health.maxHitPoints));
    }
}
