using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> WeaponInventory
    {
        get => _weaponInventory;
        private set => _weaponInventory = value;
    }
    [SerializeField] private List<GameObject> _weaponInventory = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
