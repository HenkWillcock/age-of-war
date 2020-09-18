using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUnit : MovingUnit
{
    // Update is called once per frame
    void Update()
    {
        if (!this.IsUpsideDown()) {
            base.Update();
        }
    }
}
