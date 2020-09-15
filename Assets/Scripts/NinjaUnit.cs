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

            if (this.NearestEnemyUnit() != null) {
                willClearEnemy = this.NearestEnemyDistance() < this.NearestEnemyUnit().rigidbody.velocity.magnitude;
            }

            bool enemyIsHeli = this.NearestEnemyUnit().GetComponent<HeliUnit>() != null;
            bool willTakeDownHeli = enemyIsHeli && this.NearestEnemyDistance() < 3;

            if (this.readyToFire && (willClearEnemy || willTakeDownHeli)) {
                Vector3 currentVelocity = this.rigidbody.velocity;
                currentVelocity.y = this.jumpPower;
                this.rigidbody.velocity = currentVelocity;
                this.readyToFire = false;
                this.StartCoroutine(this.Reload(2));
            }
        }
    }
}
