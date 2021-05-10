using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHuntingPlayer : EnemyState
{
    public override void Enter(Unit unit)
    {
        unit.target = unit.player.transform;
        hasPath = false;
    }

    public override void Update(Unit unit)
    {
        Vector2 distanceToTarget = new Vector2(unit.target.position.x - unit.Rb.position.x, unit.target.position.y - unit.Rb.position.y);
        float hypotenuseToTarget = Mathf.Sqrt(Mathf.Pow(distanceToTarget.x, 2) + Mathf.Pow(distanceToTarget.y, 2));
        if (hypotenuseToTarget > unit.EnemyShootDistance)
        {
            unit.mustPathfind = true;
            if (!hasPath)
            {
                PathRequestManager.RequestPath(unit.Rb.position, unit.target.position, unit.OnPathFound);
                hasPath = true;
            }
            if (unit.PlayerMovement.IsMoving)
            {
                hasPath = false;
            }
        }
        else
        {
            unit.mustPathfind = false;
            RaycastHit2D playerSightline = Physics2D.Raycast(unit.Rb.position, distanceToTarget, unit.EnemySightDistance, unit.PlayerMask);
            RaycastHit2D wallSightline = Physics2D.Raycast(unit.Rb.position, distanceToTarget, unit.EnemySightDistance, unit.UnwalkableMask);
            Debug.DrawRay(unit.Rb.position, distanceToTarget, Color.red);
            if (playerSightline.collider != null && wallSightline.collider == null)
            {
                if (!unit.CurrentWeapon.IsCoolingDown)
                {
                    unit.CurrentWeapon.Fire(false);
                }
            }
        }

        if (unit.CurrentWeapon.BulletCount <= 0) //if enemy is out of bullets, it will try to recall all of them
        {
            unit.ChangeState(unit.stateRecallingBullets);
        }
    }

    public override void Exit(Unit unit)
    {

    }
}
