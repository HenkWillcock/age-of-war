using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonUnit : Unit
{
    public GameObject cannonBallPrefab;

    void Update()
    {
        base.Update();

        if (this.readyToFire) {
            Vector3 enemyLocation = this.NearestEnemyLocation();

            if (Vector3.Distance(this.rigidbody.position, enemyLocation) < 15) {
                this.FireProjectileAtTarget(this.cannonBallPrefab, enemyLocation, 50);
                this.readyToFire = false;
                StartCoroutine(this.Reload(15));
            }
        }
    }
}
