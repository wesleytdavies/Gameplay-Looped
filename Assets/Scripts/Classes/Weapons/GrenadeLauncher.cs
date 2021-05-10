using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 3;
        rateOfFire = 1f;
        bullet = Resources.Load("Prefabs/" + Grenade.prefabName) as GameObject;
    }
}
