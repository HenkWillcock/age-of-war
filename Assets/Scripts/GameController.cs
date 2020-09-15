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
    public GameObject tankPrefab;
    public GameObject cannonPrefab;
    public GameObject heliPrefab;

    public Text gameControllerText;

    void Update()
    {
        // Q/7: Pawn
        if (Input.GetKeyUp("q")) {this.bluePlayer.BuyUnit(this.pawnPrefab);}
        if (Input.GetKeyUp("[7]") || Input.GetKeyUp("home")) {this.redPlayer.BuyUnit(this.pawnPrefab);}

        // W/8: Tank
        if (Input.GetKeyUp("w")) {this.bluePlayer.BuyUnit(this.tankPrefab);}
        if (Input.GetKeyUp("[8]") || Input.GetKeyUp("up")) {this.redPlayer.BuyUnit(this.tankPrefab);}

        // E/9: Tank
        if (Input.GetKeyUp("e")) {this.bluePlayer.BuyUnit(this.heliPrefab);}
        if (Input.GetKeyUp("[9]") || Input.GetKeyUp("page up")) {this.redPlayer.BuyUnit(this.heliPrefab);}

        // A/4: Bank
        if (Input.GetKeyUp("a")) {this.bluePlayer.BuyBank();}
        if (Input.GetKeyUp("[4]") || Input.GetKeyUp("left")) {this.redPlayer.BuyBank();}

        // S/5: Cannon
        if (Input.GetKeyUp("s")) {this.bluePlayer.BuyUnit(cannonPrefab);}
        if (Input.GetKeyUp("[5]")) {this.redPlayer.BuyUnit(cannonPrefab);}

        // D/6: Nuke
        if (Input.GetKeyUp("d")) {this.bluePlayer.BuyOrUseNuke();}
        if (Input.GetKeyUp("[6]") || Input.GetKeyUp("right")) {this.redPlayer.BuyOrUseNuke();}

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
