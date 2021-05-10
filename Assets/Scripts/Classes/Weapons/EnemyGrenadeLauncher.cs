using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeLauncher : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 3;
        rateOfFire = 2f;
        bullet = Resources.Load("Prefabs/" + EnemyGrenade.prefabName) as GameObject;
    }
}
