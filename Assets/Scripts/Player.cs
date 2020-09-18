using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // UI Elements
    public Text moneyText;
    public Image background;

    // Constants
    public Color color;
    public string name;
    public Transform playerBase;
    public Transform enemyBase;
    public GameController gameController;
    public GameObject nukePrefab;

    // Public Variables
    public int money;
    public int banks;
    public int lives;

    // Private Variables
    private int timeUntilNuke = -1;
    public int techLevel = 1;
    private int numberOfCannons = 0;
    private PlaneUnit plane = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EarnIncome", 5f, 5f);
        this.background.color = Color.Lerp(this.color, Color.white, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        string techLevelText = "";

        if (this.techLevel <= 2) {
            techLevelText = "Tech Level " + this.techLevel + " ($" + this.GetTechImproveCost() + " To Improve)";
        } else if (this.techLevel == 3 && this.timeUntilNuke == 0) {
            techLevelText = "Nuke ready!";
        } else if (this.timeUntilNuke != -1) {
            techLevelText = "Nuke ready in " + this.timeUntilNuke + " second.";
        }

        string livesText = "";
        if (this.lives == 0) {
            livesText = "Last life!";
        } else {
            livesText = "Lives: ";
            for (int i = 0; i < this.lives; i++) {
                livesText += "# ";
            }
        }

        moneyText.text = 
                livesText + "\n" +
                "$" + this.money + "\n" +
                this.banks + " Banks ($" + this.Income() + " every 5 secs)" + "\n" +
                techLevelText;
    }

    private int Income() {
        return this.banks*5;
    }

    void EarnIncome() {
        this.money += this.Income();
    }

    public void LoseALife() {
        this.lives--;

        // Still alive on 0 lives.
        if (this.lives < 0) {
            this.gameController.LoseGame(this.name);
            Destroy(this.playerBase.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void BuyUnit(GameObject prefab) {
        int unitTechLevel = prefab.GetComponent<Unit>().techLevel;
        int cost = prefab.GetComponent<Unit>().cost;

        if (this.techLevel >= unitTechLevel && this.money >= cost) {
            this.money -= cost;
            this.CreateUnit(prefab);
        }        
    }

    public void CreateUnit(GameObject prefab) {
        Vector3 initialPosition = this.playerBase.transform.position;

        if (prefab.GetComponent<CannonUnit>() != null) {
            if (this.plane != null) {
                this.plane.rigidbody.position += new Vector3(0, 0.6f, 0);
            }
            initialPosition += this.playerBase.transform.up*(3 + this.numberOfCannons*0.5f) + this.playerBase.transform.right*1;
            this.numberOfCannons++;
        }

        if (prefab.GetComponent<PlaneUnit>() != null) {
            initialPosition += this.playerBase.transform.up*(3 + this.numberOfCannons*0.5f) + this.playerBase.transform.right*1.5f;
        }

        GameObject unit = Object.Instantiate(
            prefab, 
            initialPosition,
            new Quaternion(0, 0, 0, 0)
        ) as GameObject;

        unit.GetComponent<Unit>().owner = this;

        if (unit.GetComponent<PlaneUnit>() != null) {
            this.plane = unit.GetComponent<PlaneUnit>();
        }

        foreach (Renderer renderer in unit.GetComponentsInChildren<Renderer>()) {
            renderer.material.color = this.color;
        }
    }

    public void BuyBank() {
        if (this.techLevel >= 1 && this.money >= 50) {
            this.money -= 50;
            this.banks += 1;
        }
    }

    private int GetTechImproveCost() {
        if (this.techLevel == 1) {
            return 100;
        } else if (this.techLevel == 2) {
            return 250;
        } else {
            return 0;
        }
    }

    private int GetTechLivesIncrease() {
        if (this.techLevel == 1) {
            return 2;
        } else if (this.techLevel == 2) {
            return 3;
        } else {
            return 0;
        }
    }

    public void ImproveTechOrUseNuke() {
        if (this.techLevel <= 2 && this.money >= this.GetTechImproveCost()) {
            this.money -= this.GetTechImproveCost();
            this.lives += this.GetTechLivesIncrease();
            this.techLevel++;
        }

        if (this.techLevel == 3) {
            if (this.timeUntilNuke == -1) {
                this.timeUntilNuke = 0;
                InvokeRepeating("NukeCountdown", 1f, 1f);

            } else if (this.timeUntilNuke == 0) {
                this.timeUntilNuke = 30;
                this.CreateUnit(this.nukePrefab);
            }
        }
    }

    private void NukeCountdown() {
        if (this.timeUntilNuke > 0) {
            this.timeUntilNuke--;
        }
    }
}
