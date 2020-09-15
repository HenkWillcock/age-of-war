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

    protected MovingUnit NearestEnemyUnit() {
        MovingUnit[] units = Object.FindObjectsOfType<MovingUnit>();

        MovingUnit closestEnemyUnit = null;
        float closestEnemyDistance = 99999999999;

        foreach (MovingUnit unit in units) {
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

    protected bool IsUpsideDown() {
        return Vector3.Dot(transform.up, Vector3.down) > 0;
    }

    void OnTriggerEnter(Collider collider) {
        Base baseComponent = collider.gameObject.GetComponent<Base>();

        if (baseComponent != null) {
            Player baseOwner = baseComponent.owner;
            if (this.owner != baseOwner) {
                baseOwner.LoseALife();
                Destroy(this.gameObject);
            }
        }
    }
}
