using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrossbow : Weapon
{
    public override void Initialize()
    {
        MagazineSize = 1;
        rateOfFire = 1f;
        bullet = Resources.Load("Prefabs/" + EnemyCrossbowBolt.prefabName) as GameObject;
    }
}
