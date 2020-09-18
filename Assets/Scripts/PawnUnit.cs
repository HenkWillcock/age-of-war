using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnUnit : MovingUnit
{
    public bool standing = true;

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        if (this.standing) {
            this.rigidbody.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    void OnCollisionEnter(Collision collision) {
        TankUnit tank = collision.gameObject.GetComponent<TankUnit>();
        HeliUnit heli = collision.gameObject.GetComponent<HeliUnit>();

        if (tank != null && tank.IsUpsideDown() && tank.owner == this.owner) {
            tank.Repair();
            Destroy(this.gameObject);

        } else if (heli != null && heli.enabled == false && heli.owner == this.owner) {
            heli.Repair();
            heli.enabled = true;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Unit>() != null) {
            this.standing = false;
        }
    }
}
