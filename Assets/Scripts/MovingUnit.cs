using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUnit : Unit
{
    public float topSpeed;
    public float accelleration;

    protected void Update()
    {
        base.Update();

        Vector3 towardsEnemy = this.owner.enemyBase.transform.position - this.rigidbody.position;
        towardsEnemy.Normalize();
        float accellerationMagnitude = (1 - this.rigidbody.velocity.magnitude/topSpeed) * accelleration;

        if (accellerationMagnitude > 0) {
            // TODO don't just blindly travel towards enemy base, must be facing the right direction.
            this.rigidbody.AddForce(towardsEnemy*accellerationMagnitude, ForceMode.Impulse);
        }
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
