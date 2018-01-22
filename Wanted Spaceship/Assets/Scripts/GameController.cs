using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject coin;
    public Vector3 spawnValues;
    public float startWait;
    public float coinSpawnWait;

    public GameObject asteroid;
    public float asteroidSpawnWait;
    private float asteroidSpawnWaitCounter;

    public GameObject turret;
    public float turretSpawnWait;
    private float turretSpawnWaitCounter;

    public GameObject enemySpaceship;
    public float enemySpaceshipSpawnWait;
    private float enemySpaceshipSpawnWaitCounter;

    public GameObject[] powerups = new GameObject[4];
    public float powerupSpawnWait;
    private float powerupSpawnWaitCounter;
    public Image powerupImage;

    public Text scoreText;
    public int score = 0;

    public Text endText;
    private bool gameEnded;
    private IEnumerator routine;

    public Text startText;

	void Start () {
        endText.gameObject.SetActive(false);
        gameEnded = false;
        scoreText.text = "Score: 0";
        routine = SpawnWaves();
        StartCoroutine(routine);
	}

    void Update() {
        if (gameEnded) {
            StopCoroutine(routine);
            StartCoroutine(LoadMenuAfterDelay()); 
        }
    }

    IEnumerator LoadMenuAfterDelay() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    } 

    public void AddScore(int points) {
        score += points;
        scoreText.text = "Score: " + score; 
    }

    public void EndGame() {
        PlayerPrefs.SetInt("Score", score); // save the score for the next scene
        gameEnded = true;
        endText.gameObject.SetActive(true);
    }

    IEnumerator SpawnWaves() {   
        for (int i = 0; i <= startWait; i++) { // Countdown until game starts
            startText.text = "" + (startWait - i);
            yield return new WaitForSeconds(1);
        }

        startText.gameObject.SetActive(false);
        int shipSpawnTimer = 10;

        while(true) {
            Spawn(coin);
            if (asteroidSpawnWaitCounter > asteroidSpawnWait) {
                Spawn(asteroid);
                asteroidSpawnWait += 2;
            }
            if (turretSpawnWaitCounter > turretSpawnWait) {
                Spawn(turret);
                turretSpawnWait += 4;
            }
            if (enemySpaceshipSpawnWaitCounter > enemySpaceshipSpawnWait) {
                Spawn(enemySpaceship);
                enemySpaceshipSpawnWait += shipSpawnTimer;
                if (enemySpaceshipSpawnWait > 300) { // if the player has survived for too long, make the ships spawn more frequently
                    shipSpawnTimer = 5;
                }
            }
            if (powerupSpawnWaitCounter > powerupSpawnWait) {
                GameObject powerup = powerups[Random.Range(0, 4)]; // randomly select a powerup to spawn
                Spawn(powerup);
                powerupSpawnWait += 40;
            }
            yield return new WaitForSeconds(coinSpawnWait);
            asteroidSpawnWaitCounter++;
            turretSpawnWaitCounter++;
            enemySpaceshipSpawnWaitCounter++;
            powerupSpawnWaitCounter++;
        }
    }

    void Spawn(GameObject item) {
        int xLimit = (int)spawnValues.x;
        int xPosition = Random.Range(-xLimit, xLimit+1);
        
        Vector3 spawnAt = new Vector3(xPosition, spawnValues.y, spawnValues.z);
        Collider[] spawnColliders =  Physics.OverlapSphere(spawnAt, 4, 8);
        if (spawnColliders.Length == 0) { // spawn enemy/collectible if area is clear
            Instantiate(item, spawnAt, Quaternion.identity);
        } else {
            Spawn(item);
        } 
    }

    public void ShowPowerup(int r, int g, int b) {
        powerupImage.GetComponent<Image>().color = new Color(r, g, b, 100);
    }

    public void HidePowerup() {
        powerupImage.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
}

