using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 5;
        RateOfFire = 5f;
        bullet = Resources.Load("Prefabs/" + PistolBullet.prefabName) as GameObject;
    }
}
