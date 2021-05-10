using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistol : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 5;
        rateOfFire = 0.25f;
        bullet = Resources.Load("Prefabs/" + EnemyPistolBullet.prefabName) as GameObject;
    }
}
