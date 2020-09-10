using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnits : MonoBehaviour
{
    public Player bluePlayer;
    public Player redPlayer;

    public GameObject soldierPrefab;

    void Update()
    {
        if (Input.GetKeyUp("q")) {
            this.BuyUnit(this.soldierPrefab, this.bluePlayer);
        }

        if (Input.GetKeyUp("[7]") || Input.GetKeyUp("home")) {
            this.BuyUnit(this.soldierPrefab, this.redPlayer);
        }
    }

    void BuyUnit(GameObject prefab, Player player) {
        int cost = prefab.GetComponent<Unit>().cost;
        if (player.money >= cost) {
            player.money -= cost;
        } else {
            // Can't afford.
            return;
        }

        GameObject unit = Object.Instantiate(
            this.soldierPrefab, 
            player.playerBase.transform.position,
            new Quaternion(0, 0, 0, 0)
        ) as GameObject;

        Unit unitComponent = unit.GetComponent<Unit>();
        unitComponent.owner = player;

        foreach (Renderer renderer in unit.GetComponentsInChildren<Renderer>()) {
            renderer.material.color = player.color;
        }
    }
}
