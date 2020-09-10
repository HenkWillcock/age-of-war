using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;
    public Rigidbody rigidbody;
    public int cost;

    protected void Start() {
        this.rigidbody.transform.LookAt(this.owner.enemyBase);
    }

    protected void Update()
    {
        if (this.owner == null) {
            Destroy(this.gameObject);
        }

        if (this.owner.enemyBase == null) {
            return;
        }
    }
}
