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

        // X/2: Harrier  TODO
        if (Input.GetKeyUp("x")) {}
        if (Input.GetKeyUp("[2]") || Input.GetKeyUp("down")) {}

        // C/3: Nuke
        if (Input.GetKeyUp("c")) {this.bluePlayer.BuyOrUseNuke();}
        if (Input.GetKeyUp("[3]") || Input.GetKeyUp("page down")) {this.redPlayer.BuyOrUseNuke();}

        if (Input.GetKeyUp("escape")) {
            SceneManager.LoadScene("Scene");
        }
    }

    public void LoseGame(string loserName) {
        this.gameControllerText.text = 
                "Game over. " + loserName + " lost.\n" +
                "Press 'escape' to restart.";
    }
}
