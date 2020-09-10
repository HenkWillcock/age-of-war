using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnits : MonoBehaviour
{
    public Object soldierPrefab;

    void Update()
    {
        if (Input.GetKeyUp("q")) {
            Object.Instantiate(
                this.soldierPrefab, 
                new Vector3(-14, 1.5f, 0),
                new Quaternion(0, 0, 0, 0)
            );
        }

        if (Input.GetKeyUp("[7]") || Input.GetKeyUp("home")) {
            GameObject soldier = Object.Instantiate(
                this.soldierPrefab, 
                new Vector3(14, 1.5f, 0),
                new Quaternion(0, 0, 0, 0)
            ) as GameObject;;
            SoldierUnit soldierComponent = soldier.GetComponent<SoldierUnit>();
            soldierComponent.team = Unit.Team.RED;
        }
    }
}
