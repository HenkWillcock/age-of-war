using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;
    public Rigidbody rigidbody;
    public float topSpeed;
    public int cost;

    // Update is called once per frame
    protected void Update()
    {
        if (this.rigidbody.velocity.magnitude < this.topSpeed) {
            Vector3 accelleration = new Vector3(1 - this.rigidbody.velocity.magnitude/topSpeed, 0, 0);

            if (this.owner.reverseDirection) {
                accelleration *= -1;
            }

            this.rigidbody.AddForce(accelleration, ForceMode.Impulse);
        }
    }
}
