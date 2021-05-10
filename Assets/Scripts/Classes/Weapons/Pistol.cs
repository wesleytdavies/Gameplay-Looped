using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 5;
        rateOfFire = 0.25f;
        bullet = Resources.Load("Prefabs/" + PistolBullet.prefabName) as GameObject;
    }
}
