using UnityEngine;

public class CollissionController : MonoBehaviour {

    public GameObject explosion;
    public int score;
    public int health;
    private GameController controller;
    private PlayerHealth playerHealth;

    void Start () {
        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<GameController>();
        if (controller == null) {
            Debug.LogError("Unable to find the GameController script");
        }

        tmp = GameObject.FindGameObjectWithTag("Player");
        playerHealth = tmp.GetComponent<PlayerHealth>();
        if (playerHealth == null) {
            Debug.LogError("Unable to find the PlayerHealth script");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (playerHealth.IsInvincible()) {
                Destroy(gameObject);
                GameObject tmp = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(tmp, 1);
                controller.AddScore(score);
            } else {
                Destroy(gameObject);
                GameObject tmp = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(tmp, 1);
                playerHealth.DamagePlayer(100);
            }
        } else if (other.tag == "StrongPlayerBolt") {
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameObject tmp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(tmp, 1);
            controller.AddScore(score);
        } else if (other.tag == "PlayerBolt") {
            Destroy(other.gameObject);
            health--;
            if (health == 0) {
                Destroy(gameObject);
                GameObject tmp = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(tmp, 1);
                controller.AddScore(score);
            }
        } else if (other.tag == "Enemy") {
            Destroy(gameObject);
            GameObject tmp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(tmp, 1);
        }
    }
}
