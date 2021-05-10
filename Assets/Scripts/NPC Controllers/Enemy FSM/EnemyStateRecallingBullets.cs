using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRecallingBullets : EnemyState
{
    public override void Enter(Unit unit)
    {
        hasPath = false;
    }

    public override void Update(Unit unit)
    {
        if (unit.CurrentWeapon.BulletCount >= unit.CurrentWeapon.MagazineSize)
        {
            unit.ChangeState(unit.stateHuntingPlayer);
        }
    }

    public override void Exit(Unit unit)
    {

    }
}
