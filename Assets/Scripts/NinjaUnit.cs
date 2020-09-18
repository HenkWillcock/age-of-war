using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaUnit : PawnUnit
{
    public float jumpPower;
    public float jumpDistance;

    void Update()
    {
        base.Update();

        if (this.standing) {
            bool willClearEnemy = false;
            bool enemyIsHeli = false;

            if (this.NearestEnemyUnit() != null) {
                float averageVelocity = (this.NearestEnemyUnit().rigidbody.velocity.magnitude + this.rigidbody.velocity.magnitude)/2;
                willClearEnemy = this.NearestEnemyDistance() < averageVelocity;
                enemyIsHeli = this.NearestEnemyUnit().GetComponent<HeliUnit>() != null;
            }

            bool willTakeDownHeli = enemyIsHeli && this.NearestEnemyDistance() < 3;

            if (this.readyToFire && (willClearEnemy || willTakeDownHeli)) {
                Vector3 currentVelocity = this.rigidbody.velocity;
                currentVelocity.y = this.jumpPower;
                this.rigidbody.velocity = currentVelocity;
                this.readyToFire = false;
                this.StartCoroutine(this.Reload(1.3f));
            }
        }
    }
}
