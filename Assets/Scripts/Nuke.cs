using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : OwnedObject
{
    protected void Start() {
        this.rigidbody.transform.LookAt(this.owner.enemyBase);
        this.rigidbody.AddForce(100000 * transform.forward);
    }
}
