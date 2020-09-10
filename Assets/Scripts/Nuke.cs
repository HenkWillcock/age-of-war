using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MovingUnit
{
    void Start() {
        base.Start();
        this.rigidbody.AddForce(100000 * transform.forward);
    }
}
