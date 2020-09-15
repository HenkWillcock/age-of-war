using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;
    public Rigidbody rigidbody;
    public int cost;

    protected bool readyToFire = true;

    protected void Start() {
        this.rigidbody.transform.LookAt(this.owner.enemyBase);
    }

    protected void Update()
    {
        if (this.owner == null) {
            Destroy(this.gameObject);
        }

        if (this.owner.enemyBase == null) {
            return;
        }
    }

    protected Vector3 NearestEnemyLocation() {
        Unit[] units = Object.FindObjectsOfType<MovingUnit>();

        Unit closestEnemyUnit = null;
        float closestEnemyDistance = 99999999999;

        foreach (Unit unit in units) {
            float distanceToUnit = Vector3.Distance(this.rigidbody.position, unit.rigidbody.position);

            if (distanceToUnit < closestEnemyDistance && unit.owner != this.owner) {
                closestEnemyDistance = distanceToUnit;
                closestEnemyUnit = unit;
            }
        }

        return closestEnemyUnit.rigidbody.position;
    }

    protected float NearestEnemyDistance() {
        return Vector3.Distance(this.rigidbody.position, this.NearestEnemyLocation());
    }

    protected void FireProjectileAtTarget(GameObject projectilePrefab, Vector3 target, float speed) {
        Vector3 towardsTarget = target - this.rigidbody.position;
        towardsTarget.Normalize();

        Vector3 initialPosition = this.rigidbody.position + towardsTarget * 2;

        GameObject projectile = Object.Instantiate(
            projectilePrefab, 
            initialPosition,
            new Quaternion(0, 0, 0, 0)
        ) as GameObject;

        projectile.GetComponent<Unit>().owner = this.owner;
        projectile.GetComponent<Renderer>().material.color = this.owner.color;
        projectile.GetComponent<Rigidbody>().velocity = towardsTarget * speed;
    }

    protected IEnumerator Reload(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        this.readyToFire = true;
    }

    protected bool IsUpsideDown() {
        return Vector3.Dot(transform.up, Vector3.down) > 0;
    }
}
