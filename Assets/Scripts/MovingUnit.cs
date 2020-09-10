using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUnit : Unit
{
    public float topSpeed;

    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        Vector3 towardsEnemy = this.owner.enemyBase.transform.position - this.rigidbody.position;
        towardsEnemy.Normalize();
        float accellerationMagnitude = (1 - this.rigidbody.velocity.magnitude/topSpeed) * topSpeed;

        if (accellerationMagnitude > 0) {
            this.rigidbody.AddForce(towardsEnemy*accellerationMagnitude, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider collider) {
        // TODO double lives lost bug
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
