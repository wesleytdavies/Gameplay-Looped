using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 1;
        rateOfFire = 0f;
        bullet = Resources.Load("Prefabs/" + CrossbowBolt.prefabName) as GameObject;
    }
}
