using UnityEngine;

public class EnemyBoltController : MonoBehaviour {

    public int playerDamage;
    private PlayerHealth playerHealth;

    void Start() {
        GameObject tmp = GameObject.FindWithTag("Player");
        playerHealth = tmp.GetComponent<PlayerHealth>();
        if (playerHealth == null) {
            Debug.LogError("Unable to find the PlayerHealth script");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" || other.tag == "StrongPlayerBolt" || other.tag == "PlayerBolt" || other.tag == "EnemyBolt") {
            // The bolt is always destroyed on contact
            Destroy(gameObject);

            if (other.tag == "Player") {
                if (!playerHealth.IsInvincible())
                {
                    playerHealth.DamagePlayer(playerDamage);
                }
            } else if (other.tag == "PlayerBolt" || other.tag == "EnemyBolt") { // strong bolts are not destroyed
                Destroy(other.gameObject);
            }
        }
    }
}