using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour //base class for all weapons
{
    public int bulletCount; //how many bullets are left in the magazine

    public int MagazineSize //maximum amount of bullets this weapon can hold
    {
        get => _magazineSize;
        protected set => _magazineSize = value;
    }
    private int _magazineSize;

    public float RateOfFire //how quickly this weapon shoots bullets
    {
        get => _rateOfFire;
        protected set => _rateOfFire = value;
    }
    private float _rateOfFire;

    protected GameObject bullet;

    private void Awake()
    {
        Initialize();
    }

    public abstract void Initialize();

    public virtual void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        bulletCount--;
    }
}
