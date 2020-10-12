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
        this.MoveTowardsTarget(this.accelleration, this.topSpeed, this.owner.enemyBase.transform.position);
    }
}
