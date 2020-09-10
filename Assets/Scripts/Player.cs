using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // UI Elements
    public Text moneyText;

    // Constants
    public Color color;
    public string name;
    public Transform playerBase;
    public Transform enemyBase;
    public GameController gameController;
    public GameObject nukePrefab;

    // Public Variables
    public int money;
    public int income;
    public int lives;
    private int timeUntilNuke = -1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EarnIncome", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        string nukeText = "";
        if (this.timeUntilNuke == 0) {
            nukeText = "Nuke ready!";
        } else if (this.timeUntilNuke != -1) {
            nukeText = "Nuke ready in " + this.timeUntilNuke + " second.";
        }

        moneyText.text = 
                "$" + this.money + "\n" +
                "$" + this.income + " Income" + "\n" +
                this.lives + " Lives" + "\n" +
                nukeText;
    }

    void EarnIncome() {
        this.money += this.income;
    }

    public void LoseALife() {
        this.lives--;
        if (this.lives <= 0) {
            this.gameController.LoseGame(this.name);
            Destroy(this.playerBase.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void BuyUnit(GameObject prefab) {
        int cost = prefab.GetComponent<Unit>().cost;
        if (this.money >= cost) {
            this.money -= cost;
        } else {
            // Can't afford.
            return;
        }

        this.CreateUnit(prefab);
    }

    public void CreateUnit(GameObject prefab) {
        Vector3 initialPosition = this.playerBase.transform.position;

        if (prefab.GetComponent<CannonUnit>() != null) {
            initialPosition += new Vector3(0, 3, 0);
        }

        GameObject unit = Object.Instantiate(
            prefab, 
            initialPosition,
            new Quaternion(0, 0, 0, 0)
        ) as GameObject;

        unit.GetComponent<Unit>().owner = this;

        foreach (Renderer renderer in unit.GetComponentsInChildren<Renderer>()) {
            renderer.material.color = this.color;
        }
    }

    public void BuyBank() {
        if (this.money >= 50) {
            this.money -= 50;
            this.income += 5;
        }
    }

    public void BuyOrUseNuke() {
        if (this.timeUntilNuke == 0) {
            this.timeUntilNuke = 20;
            this.CreateUnit(this.nukePrefab);
        } else if (this.timeUntilNuke == -1 && this.money >= 200) {
            this.money -= 200;
            this.timeUntilNuke = 0;
            InvokeRepeating("NukeCountdown", 1f, 1f);
        }
    }

    private void NukeCountdown() {
        if (this.timeUntilNuke > 0) {
            this.timeUntilNuke--;
        }
    }
}
