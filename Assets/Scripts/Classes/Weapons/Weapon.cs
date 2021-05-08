using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour //base class for all weapons
{
    public int BulletCount //how many bullets are left in the magazine
    {
        get => _bulletCount;
        private set => _bulletCount = value;
    }
    private int _bulletCount;

    public int MagazineSize //maximum amount of bullets the weapon can hold
    {
        get => _magazineSize;
        protected set => _magazineSize = value;
    }
    private int _magazineSize;

    protected float rateOfFire; //how quickly the weapon shoots bullets in seconds

    protected GameObject bullet; //the bullet prefab the weapon shoots

    [SerializeField] private GameObject holder; //the character holding this instance of the weapon

    public Transform Barrel //the location where the bullet is shot from. Weapon must have a child named [barrelName]
    {
        get => _barrel;
        private set => _barrel = value;
    }
    private Transform _barrel;

    private static readonly string barrelName = "Barrel"; //the name of all barrels attached to weapons

    public bool IsCoolingDown
    {
        get => _isCoolingDown;
        private set => _isCoolingDown = value;
    }
    private bool _isCoolingDown = false;

    private static readonly float energyLossSpeed = 0.25f; //how long it takes the weapon to lose its excess kinetic energy in seconds

    public bool HasExcessEnergy
    {
        get => _hasExcessEnergy;
        private set => _hasExcessEnergy = value;
    }
    private bool _hasExcessEnergy = false;

    private void Awake()
    {
        Initialize();
        Barrel = gameObject.transform.Find(barrelName).transform; //finds the barrel's transform, which must be a child of weapon
        BulletCount = MagazineSize;
    }

    public abstract void Initialize();

    public virtual void Fire(bool isPowered)
    {
        GameObject firedBullet = Instantiate(bullet, Barrel.position, transform.rotation);
        firedBullet.GetComponent<LoopableObject>().isPowered = isPowered;
        firedBullet.GetComponent<LoopableObject>().originator = gameObject; //sets this as the bullet's originator
        BulletCount--;
        StartCoroutine(FireCooldown());
    }

    public virtual void Recall(GameObject recalledBullet) //if facing one of this weapon's reversing bullets, can recall and load it back into magazine
    {
        Destroy(recalledBullet);
        BulletCount++;
        StartCoroutine(EnergyLoss());
    }

    IEnumerator FireCooldown() //makes sure weapon can't be fired again before the rate of fire has elapsed
    {
        IsCoolingDown = true;
        yield return new WaitForSeconds(rateOfFire);
        IsCoolingDown = false;
        yield break;
    }

    IEnumerator EnergyLoss() //after being recalled, a bullet retains some excess kinetic energy. If fired again before this cooldown ends, the next shot is a powered shot
    {
        HasExcessEnergy = true;
        yield return new WaitForSeconds(energyLossSpeed);
        HasExcessEnergy = false;
        yield break;
    }
}
