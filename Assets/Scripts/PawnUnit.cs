using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnUnit : MovingUnit
{
    public bool standing = true;

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (this.standing) {
            this.rigidbody.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Unit>() != null) {

            this.standing = false;
        }
    }
}
