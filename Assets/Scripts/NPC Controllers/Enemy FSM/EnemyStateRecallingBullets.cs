using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRecallingBullets : EnemyState
{
    private List<LoopableObject> firedBullets = new List<LoopableObject>(); //list of all the bullets fired by this enemy
    private LoopableObject closestBullet;
    private Vector2 targetVector;
    private Vector2 currentPosition;
    private Vector2 previousPosition;
    private float fixTimer;

    public override void Enter(Unit unit)
    {
        fixTimer = 0f;
        unit.recallRadius = 1f;
        hasPath = false;
        unit.mustPathfind = true;
        LoopableObject[] allLoopableObjects = Object.FindObjectsOfType<LoopableObject>();
        foreach (LoopableObject loopableObject in allLoopableObjects) //search through all loopable objects in the scene to find this enemy's fired bullets
        {
            if (loopableObject.originator == unit.CurrentWeapon.gameObject)
            {
                firedBullets.Add(loopableObject);
            }
        }
    }

    public override void Update(Unit unit)
    {
        TryRecall(unit);
        if (!hasPath)
        {
            if (closestBullet != null)
            {
                unit.target = closestBullet.transform;
                targetVector = new Vector2(closestBullet.StartPosition.x, closestBullet.StartPosition.y);
                PathRequestManager.RequestPath(unit.Rb.position, targetVector, unit.OnPathFound);
                hasPath = true;
            }
            else
            {
                closestBullet = FindClosestBullet(unit.transform.position);
            }
        }

        if (unit.CurrentWeapon.BulletCount >= unit.CurrentWeapon.MagazineSize)
        {
            unit.ChangeState(unit.stateHuntingPlayer);
        }

        //TODO: this section solely exists to make sure the enemy doesn't get stuck, which happens more than I'd like to admit
        currentPosition = unit.transform.position;
        if(currentPosition == previousPosition)
        {
            fixTimer += Time.deltaTime;
            if (fixTimer > 3f)
            {
                unit.recallRadius = 100f * Time.deltaTime;
            }
        }
        previousPosition = currentPosition;
    }

    public override void Exit(Unit unit)
    {

    }

    public LoopableObject FindClosestBullet(Vector2 currentPosition)
    {
        float shortestDistance = Mathf.Infinity;
        LoopableObject closest = null;
        foreach(LoopableObject bullet in firedBullets)
        {
            if (bullet != null)
            {
                Vector2 distanceFromFirer = bullet.StartPosition - currentPosition;
                float bulletDistance = distanceFromFirer.sqrMagnitude;
                if (bulletDistance < shortestDistance)
                {
                    closest = bullet;
                    shortestDistance = bulletDistance;
                }
            }
        }
        return closest;
    }

    public void TryRecall(Unit unit)
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(unit.transform.position, unit.recallRadius);
        for (int i = 0; i < nearbyObjects.Length; i++)
        {
            if (nearbyObjects[i].GetComponent<LoopableObject>() != null)
            {
                if (nearbyObjects[i].GetComponent<LoopableObject>().IsReversing)
                {
                    if (nearbyObjects[i].GetComponent<LoopableObject>().originator == unit.CurrentWeapon.gameObject)
                    {
                        firedBullets.Remove(closestBullet);
                        closestBullet = null;
                        hasPath = false;
                        unit.CurrentWeapon.Recall(nearbyObjects[i].gameObject);
                        return; //prevents recalling multiple bullets at once
                    }
                }
            }
        }
    }
}
