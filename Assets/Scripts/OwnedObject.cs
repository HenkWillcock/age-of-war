using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedObject : MonoBehaviour
{
    public Player owner;
    public Rigidbody rigidbody;

    protected void Update()
    {
        if (this.rigidbody.position.y < -20) {
            Destroy(this.gameObject);
        }

        if (this.owner == null) {
            Destroy(this.gameObject);
        }

        if (this.owner.enemyBase == null) {
            return;
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
