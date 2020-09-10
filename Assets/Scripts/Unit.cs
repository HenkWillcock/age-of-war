using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;
    public Rigidbody rigidbody;
    public float topSpeed;
    public int cost;

    void Start() {
        this.rigidbody.transform.LookAt(this.owner.enemyBase);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (this.owner == null) {
            Destroy(this.gameObject);
        }

        if (this.owner.enemyBase == null) {
            return;
        }

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
