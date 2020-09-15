using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliUnit : MovingUnit
{
    public Transform rotorBlades;
    public bool flying;
    public bool enabled = true;
    public GameObject cannonBallPrefab;

    private float targetAltitude = 5;

    public void Update() {
        base.Update();

        if (Vector3.Distance(this.rigidbody.position, this.owner.playerBase.position) > 3) {
            this.flying = true;
        }

        if (this.flying && this.enabled) {
            this.rotorBlades.RotateAround(rotorBlades.position, rotorBlades.up, 20);

            float lift = (1 - this.rigidbody.position.y/this.targetAltitude) * 0.5f;
            this.rigidbody.AddForce(Vector3.up * lift, ForceMode.Impulse);
            this.rigidbody.velocity *= 0.9f;

            this.rigidbody.angularVelocity *= 0.9f;

            if (this.readyToFire && this.NearestEnemyDistance() < 15) {
                Vector3 enemyLocation = this.NearestEnemyLocation();
                this.FireProjectileAtTarget(this.cannonBallPrefab, enemyLocation, 20);
                this.readyToFire = false;
                StartCoroutine(this.Reload(1));
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit != null && unit.owner != this.owner) {
            this.enabled = false;
        }
    }
}
