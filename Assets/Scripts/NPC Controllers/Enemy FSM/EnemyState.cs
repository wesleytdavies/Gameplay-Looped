using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected bool hasPath;

    public abstract void Enter(Unit unit);

    public abstract void Update(Unit unit);

    public abstract void Exit(Unit unit);
}
