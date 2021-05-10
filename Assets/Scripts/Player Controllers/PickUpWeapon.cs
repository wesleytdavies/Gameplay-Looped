using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpWeapon : MonoBehaviour
{
    private Inventory inventory;
    private PlayerRotation playerRotation;
    private GameObject swapWeaponsText;
    private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        playerRotation = GetComponent<PlayerRotation>();
        swapWeaponsText = GameObject.Find("Swap Weapons Text");
        textMeshProUGUI = swapWeaponsText.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Weapon>() != null)
        {
            if(collision.GetComponent<Weapon>().holder == null)
            {
                textMeshProUGUI.text = "Press 'E' to Swap Weapons";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SwitchWeapons(collision.GetComponent<Weapon>());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textMeshProUGUI.text = "";
    }

    private void SwitchWeapons(Weapon weapon)
    {
        inventory.weaponInventory[0].GetComponent<Weapon>().holder = null;
        inventory.weaponInventory[0] = weapon.gameObject;
        weapon.holder = gameObject;
        playerRotation.weaponOrigin = weapon.transform.parent.gameObject;
    }
}
