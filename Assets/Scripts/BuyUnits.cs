using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnits : MonoBehaviour
{
    public Player bluePlayer;
    public Player redPlayer;

    public GameObject soldierPrefab;
    public GameObject tankPrefab;

    void Update()
    {
        if (Input.GetKeyUp("q")) {
            this.bluePlayer.BuyUnit(this.soldierPrefab);
        }

        if (Input.GetKeyUp("[7]") || Input.GetKeyUp("home")) {
            this.redPlayer.BuyUnit(this.soldierPrefab);
        }

        if (Input.GetKeyUp("w")) {
            this.bluePlayer.BuyBank();
        }

        if (Input.GetKeyUp("[8]") || Input.GetKeyUp("up")) {
            this.redPlayer.BuyBank();
        }

        if (Input.GetKeyUp("e")) {
            this.bluePlayer.BuyUnit(this.tankPrefab);
        }

        if (Input.GetKeyUp("[9]") || Input.GetKeyUp("page up")) {
            this.redPlayer.BuyUnit(this.tankPrefab);
        }
    }
}
