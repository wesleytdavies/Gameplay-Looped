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

    public Transform Barrel //the location where the bullet is shot from. Weapon must have a child named [barrelName]
    {
        get => _barrel;
        private set => _barrel = value;
    }
    private Transform _barrel;

    private static readonly string barrelName = "Barrel"; //the name of all barrels attached to weapons

    private void Awake()
    {
        Initialize();
        Barrel = gameObject.transform.Find(barrelName).transform; //finds the barrel's transform, which must be a child of weapon
    }

    public abstract void Initialize();

    public virtual void Fire()
    {
        GameObject firedBullet = Instantiate(bullet, Barrel.position, transform.rotation);
        firedBullet.GetComponent<LoopableObject>().originator = gameObject; //sets this as the bullet's originator
        bulletCount--;
    }

    public virtual void Recall(GameObject recalledBullet) //if facing one of this weapon's reversing bullets, can recall and load it back into magazine
    {
        Destroy(recalledBullet);
        bulletCount++;
    }
}
