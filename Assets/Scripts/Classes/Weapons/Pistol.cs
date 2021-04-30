using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override int MagazineSize()
    {
        return 5;
    }
    public override int MagazineCount()
    {
        return 0;
    }
    public override float BulletSpeed()
    {
        return 0;
    }
}
