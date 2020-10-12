using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : OwnedObject
{
    public int cost;
    public int techLevel;

    protected bool readyToFire = true;

    protected void Start() {
        this.rigidbody.transform.LookAt(this.owner.enemyBase);
    }

    protected void Update()
    {
        base.Update();
    }

    protected Unit NearestEnemyUnit() {
        Unit[] units = Object.FindObjectsOfType<Unit>();

        Unit closestEnemyUnit = null;
        float closestEnemyDistance = 99999999999;

        foreach (Unit unit in units) {
            float distanceToUnit = Vector3.Distance(this.rigidbody.position, unit.rigidbody.position);
            bool notYours = unit.owner != this.owner;
            bool notNinja = unit.GetComponent<NinjaUnit>() == null;

            if (distanceToUnit < closestEnemyDistance && notYours && notNinja) {
                closestEnemyDistance = distanceToUnit;
                closestEnemyUnit = unit;
            }
        }

        return closestEnemyUnit;
    }

    protected Vector3 NearestEnemyLocation() {
        if (this.NearestEnemyUnit() != null) {
            return this.NearestEnemyUnit().rigidbody.position;
        }
        return this.owner.enemyBase.position;
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

    public bool IsUpsideDown() {
        return Vector3.Dot(transform.up, Vector3.down) > 0;
    }

    public void Repair() {
        this.rigidbody.rotation = new Quaternion(0, 0, 0, 0).normalized;
        this.rigidbody.transform.LookAt(this.owner.enemyBase.transform.position);
    }

    protected void MoveTowardsTarget(float accelleration, float topSpeed, Vector3 target) {
        Vector3 towardsTarget = target - this.rigidbody.position;
        towardsTarget.Normalize();
        float accellerationMagnitude = (1 - this.rigidbody.velocity.magnitude/topSpeed) * accelleration;

        if (accellerationMagnitude > 0) {
            this.rigidbody.AddForce(towardsTarget*accellerationMagnitude, ForceMode.Impulse);
        }
    }
}
