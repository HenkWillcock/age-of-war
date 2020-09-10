using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // UI Elements
    public Text moneyText;

    // Constants
    public Transform playerBase;
    public Transform enemyBase;
    public Color color;

    // Public Variables
    public int money;
    public int income;
    public int lives;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EarnIncome", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.dead) {
            moneyText.text = "I'm dead :(";
            return;
        }

        moneyText.text = 
                "$" + this.money + "\n" +
                "$" + this.income + " Income" + "\n" +
                this.lives + " Lives";
    }

    void EarnIncome() {
        this.money += this.income;
    }

    public void LoseALife() {
        this.lives--;
        if (this.lives <= 0) {
            Destroy(this.playerBase.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void BuyUnit(GameObject prefab) {
        if (this.dead) {
            return;
        }

        int cost = prefab.GetComponent<Unit>().cost;
        if (this.money >= cost) {
            this.money -= cost;
        } else {
            // Can't afford.
            return;
        }

        GameObject unit = Object.Instantiate(
            prefab, 
            this.playerBase.transform.position,
            new Quaternion(0, 0, 0, 0)
        ) as GameObject;

        Unit unitComponent = unit.GetComponent<Unit>();
        unitComponent.owner = this;

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
}
