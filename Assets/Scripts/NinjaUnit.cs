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
            bool isFlyingEnemy = false;

            if (this.NearestEnemyUnit() != null) {
                float averageVelocity = (this.NearestEnemyUnit().rigidbody.velocity.magnitude + this.rigidbody.velocity.magnitude)/2;
                willClearEnemy = this.NearestEnemyDistance() < averageVelocity;
                Debug.Log(this.NearestEnemyUnit());
                isFlyingEnemy = 
                    this.NearestEnemyUnit().GetComponent<HeliUnit>() != null ||
                    this.NearestEnemyUnit().GetComponent<PlaneUnit>() != null ;
            }

            bool willTakeDownFlyer = isFlyingEnemy && this.NearestEnemyDistance() < 3;
            // Debug.Log(isFlyingEnemy.ToString() + ' ' + willTakeDownFlyer.ToString());

            if (this.readyToFire && (willClearEnemy || willTakeDownFlyer)) {
                Vector3 currentVelocity = this.rigidbody.velocity;
                currentVelocity.y = this.jumpPower;
                this.rigidbody.velocity = currentVelocity;
                this.readyToFire = false;
                this.StartCoroutine(this.Reload(1.3f));
            }
        }
    }
}
