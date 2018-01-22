using UnityEngine;

public class Invincible : MonoBehaviour {

    public GameObject collected;
    private PlayerHealth player;

    void Start() {
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        player = tmp.GetComponent<PlayerHealth>();
        if (player == null) {
            Debug.LogError("Unable to find the PlayerHealth script");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player.StartCoroutine(player.Invincible());
            Destroy(gameObject);
            GameObject tmp = Instantiate(collected, transform.position, transform.rotation);
            Destroy(tmp, 1);
        }
    }
}
