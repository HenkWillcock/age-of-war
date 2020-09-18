using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneUnit : Unit
{
    private float targetAltitude = 10;

    // Update is called once per frame
    void Update()
    {   
        base.Update();

        float lift = (1 - this.rigidbody.position.y/this.targetAltitude) * 1f;
        this.rigidbody.AddForce(Vector3.up * lift, ForceMode.Impulse);
        this.rigidbody.velocity *= 0.9f;
    }
}
