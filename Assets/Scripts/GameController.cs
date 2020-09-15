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
    public GameObject jumperPrefab;

    public GameObject tankPrefab;
    public GameObject heliPrefab;

    public GameObject cannonPrefab;

    public Text gameControllerText;

    void Update()
    {
        // Q/7: Pawn
        if (Input.GetKeyUp("q")) {this.bluePlayer.BuyUnit(this.pawnPrefab);}
        if (Input.GetKeyUp("[7]") || Input.GetKeyUp("home")) {this.redPlayer.BuyUnit(this.pawnPrefab);}

        // W/8: Jumper
        if (Input.GetKeyUp("w")) {this.bluePlayer.BuyUnit(this.jumperPrefab);}
        if (Input.GetKeyUp("[8]") || Input.GetKeyUp("up")) {this.redPlayer.BuyUnit(this.jumperPrefab);}

        // E/9: Commando  TODO
        if (Input.GetKeyUp("e")) {}
        if (Input.GetKeyUp("[9]") || Input.GetKeyUp("page up")) {}

        // A/4: Tank
        if (Input.GetKeyUp("a")) {this.bluePlayer.BuyUnit(this.tankPrefab);}
        if (Input.GetKeyUp("[4]") || Input.GetKeyUp("left")) {this.redPlayer.BuyUnit(this.tankPrefab);}

        // S/5: Chopper
        if (Input.GetKeyUp("s")) {this.bluePlayer.BuyUnit(this.heliPrefab);}
        if (Input.GetKeyUp("[5]")) {this.redPlayer.BuyUnit(this.heliPrefab);}

        // D/6: Harrier  TODO
        if (Input.GetKeyUp("d")) {}
        if (Input.GetKeyUp("[6]") || Input.GetKeyUp("right")) {}

        // Z/1: Bank
        if (Input.GetKeyUp("z")) {this.bluePlayer.BuyBank();}
        if (Input.GetKeyUp("[1]") || Input.GetKeyUp("end")) {this.redPlayer.BuyBank();}

        // X/2: Cannon
        if (Input.GetKeyUp("z")) {this.bluePlayer.BuyUnit(this.cannonPrefab);}
        if (Input.GetKeyUp("[1]") || Input.GetKeyUp("end")) {this.redPlayer.BuyUnit(this.cannonPrefab);}

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
