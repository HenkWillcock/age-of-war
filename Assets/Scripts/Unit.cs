using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Team {RED, BLUE}

    public Team team;

    public Rigidbody rigidbody;


    public float topSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (this.rigidbody.velocity.magnitude < this.topSpeed) {
            this.rigidbody.AddForce(
                new Vector3(1 - this.rigidbody.velocity.magnitude/topSpeed, 0, 0), 
                ForceMode.Impulse
            );
        }
    }
}
