using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour //base class for all weapons
{
    public int magazineCount; //how many bullets are left in the magazine

    public int MagazineSize //maximum amount of bullets this weapon can hold
    {
        get
        {
            return _magazineSize;
        }
        protected set
        {
            _magazineSize = value;
        }
    }
    private int _magazineSize;

    public LoopableObject BulletType
    {
        get
        {
            return _bulletType;
        }
        protected set
        {
            _bulletType = value;
        }
    }
    private LoopableObject _bulletType;

    public float RateOfFire //how quickly this weapon shoots bullets
    {
        get
        {
            return _rateOfFire;
        }
        protected set
        {
            _rateOfFire = value;
        }
    }
    private float _rateOfFire;

    //protected PistolBullet pistol = new PistolBullet();

    public abstract void Initialize();
}
