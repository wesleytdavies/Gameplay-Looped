using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    private PistolBullet pistolBullet;

    public override void Initialize()
    {
        MagazineSize = 5;
        pistolBullet = gameObject.AddComponent<PistolBullet>();
        BulletType = pistolBullet;
        RateOfFire = 5f;
    }
}
