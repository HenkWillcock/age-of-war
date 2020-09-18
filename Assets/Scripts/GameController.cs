using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player bluePlayer;
    public Player redPlayer;

    public GameObject pawnPrefab;
    public GameObject ninjaPrefab;

    public GameObject tankPrefab;
    public GameObject heliPrefab;
    public GameObject planePrefab;

    public GameObject cannonPrefab;

    public Text gameControllerText;

    void Update()
    {
        // Q/7: Pawn
        if (Input.GetKeyUp("q")) {this.bluePlayer.BuyUnit(this.pawnPrefab);}
        if (Input.GetKeyUp("[7]") || Input.GetKeyUp("home")) {this.redPlayer.BuyUnit(this.pawnPrefab);}

        // W/8: Tank
        if (Input.GetKeyUp("w")) {this.bluePlayer.BuyUnit(this.tankPrefab);}
        if (Input.GetKeyUp("[8]") || Input.GetKeyUp("up")) {this.redPlayer.BuyUnit(this.tankPrefab);}

        // E/9: Bank
        if (Input.GetKeyUp("e")) {this.bluePlayer.BuyBank();}
        if (Input.GetKeyUp("[9]") || Input.GetKeyUp("page up")) {this.redPlayer.BuyBank();}

        // A/4: Jumper
        if (Input.GetKeyUp("a")) {this.bluePlayer.BuyUnit(this.ninjaPrefab);}
        if (Input.GetKeyUp("[4]") || Input.GetKeyUp("left")) {this.redPlayer.BuyUnit(this.ninjaPrefab);}

        // S/5: Heli
        if (Input.GetKeyUp("s")) {this.bluePlayer.BuyUnit(this.heliPrefab);}
        if (Input.GetKeyUp("[5]")) {this.redPlayer.BuyUnit(this.heliPrefab);}

        // D/6: Cannon
        if (Input.GetKeyUp("d")) {this.bluePlayer.BuyUnit(this.cannonPrefab);}
        if (Input.GetKeyUp("[6]") || Input.GetKeyUp("right")) {this.redPlayer.BuyUnit(this.cannonPrefab);}

        // Z/1: Commando  TODO
        if (Input.GetKeyUp("z")) {}
        if (Input.GetKeyUp("[1]") || Input.GetKeyUp("end")) {}

        // X/2: Plane
        if (Input.GetKeyUp("x")) {this.bluePlayer.BuyUnit(this.planePrefab);}
        if (Input.GetKeyUp("[2]") || Input.GetKeyUp("down")) {this.redPlayer.BuyUnit(this.planePrefab);}

        // C/3: Nuke
        if (Input.GetKeyUp("c")) {this.bluePlayer.ImproveTechOrUseNuke();}
        if (Input.GetKeyUp("[3]") || Input.GetKeyUp("page down")) {this.redPlayer.ImproveTechOrUseNuke();}

        if (Input.GetKeyUp("escape")) {
            if (Input.GetKey("left shift")) {
                this.bluePlayer.money += 10000;
                this.redPlayer.money += 10000;
            } else {
                SceneManager.LoadScene("Scene");
            }
        }

        int highestTechLevel = Math.Max(this.redPlayer.techLevel, this.bluePlayer.techLevel);

        string mainText = "Pawn ($10)   Tank ($50)      Bank ($50)\n\n";

        if (highestTechLevel >= 2) {
            mainText += "Ninja ($30)    Heli ($100)      Cannon ($100)";
        }
        mainText += "\n\n";

        if (highestTechLevel >= 3) {
            mainText += "Hero ($200)   Plane ($150)   Improve Tech";
        } else {
            mainText += "                                             Improve Tech";
        }

        this.gameControllerText.text = mainText;
    }

    public void LoseGame(string loserName) {
        this.gameControllerText.text = 
                "Game over. " + loserName + " lost.\n" +
                "Press 'escape' to restart.";
    }
}
