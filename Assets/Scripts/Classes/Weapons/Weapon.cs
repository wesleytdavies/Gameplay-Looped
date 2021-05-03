using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour //base class for all weapons
{
    public int bulletCount; //how many bullets are left in the magazine

    public int MagazineSize //maximum amount of bullets the weapon can hold
    {
        get => _magazineSize;
        protected set => _magazineSize = value;
    }
    private int _magazineSize;

    public float RateOfFire //how quickly the weapon shoots bullets
    {
        get => _rateOfFire;
        protected set => _rateOfFire = value;
    }
    private float _rateOfFire;

    protected GameObject bullet; //the bullet prefab the weapon shoots
    [SerializeField] private GameObject holder; //the character holding this instance of the weapon
    private Transform barrel; //the location where the bullet is shot from. Weapon must have a child named [barrelName]
    private static string barrelName = "Barrel"; //the name of all barrels attached to weapons

    private void Awake()
    {
        Initialize();
        barrel = gameObject.transform.Find(barrelName).transform; //finds the barrel's transform, which must be a child of weapon
    }

    public abstract void Initialize();

    public virtual void Fire()
    {
        Instantiate(bullet, barrel.position, transform.rotation);
        bulletCount--;
    }

    public virtual void Recall() //if at the origin of the weapon's fired bullets, can be recalled and loaded back into magazine
    {
        bulletCount++;
    }
}
