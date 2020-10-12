using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneUnit : Unit
{
    private enum Status {
        READY, ENGAGING, SHOOTING, RETURNING, RELOADING
    }

    public GameObject engines;
    public GameObject cannonBallPrefab;

    private float targetAltitude = 5;
    private Status status = Status.READY;
    private int bullets = 100;

    protected void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {   
        base.Update();
        if (this.status != Status.READY) {
            float lift = (1 - this.rigidbody.position.y/this.targetAltitude) * 1f;
            this.rigidbody.AddForce(Vector3.up * lift, ForceMode.Impulse);
            this.rigidbody.velocity *= 0.9f;
        }

        if (this.status == Status.ENGAGING) {
            this.MoveTowardsTarget(0.5f, 10, this.owner.enemyBase.transform.position);
            if (this.NearestEnemyDistance() < 7.5f) {
                this.ExecuteStrike();
            }

        } else if (this.status == Status.SHOOTING) {
            Vector3 enemyLocation = this.NearestEnemyLocation();
            this.FireProjectileAtTarget(
                this.cannonBallPrefab,
                enemyLocation,
                10
            );
            this.bullets--;
            if (this.bullets == 0) {
                this.status = Status.RETURNING;
                this.bullets = 15;
            }

        } else if (this.status == Status.RETURNING) {
            this.MoveTowardsTarget(0.5f, 10, this.owner.playerBase.transform.position);
            if (Vector3.Distance(this.rigidbody.position, this.owner.playerBase.transform.position) < 6f) {
                StartCoroutine(this.ReloadStrike());
            }
        }
    }

    public void StartStrike() {
        if (this.status != Status.READY) {
            return;
        }
        this.status = Status.ENGAGING;
    }

    private void ExecuteStrike() {
        this.status = Status.SHOOTING;
        foreach (Renderer renderer in this.engines.GetComponentsInChildren<Renderer>()) {
            renderer.enabled = false;
        }
    }

    private IEnumerator ReloadStrike() {
        this.status = Status.RELOADING;
        yield return new WaitForSeconds(10);
        this.status = Status.READY;
        foreach (Renderer renderer in this.engines.GetComponentsInChildren<Renderer>()) {
            renderer.enabled = true;
        }
    }
}
