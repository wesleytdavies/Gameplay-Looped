using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public abstract int MagazineSize();
    public abstract int MagazineCount();
    public abstract float BulletSpeed();
}
