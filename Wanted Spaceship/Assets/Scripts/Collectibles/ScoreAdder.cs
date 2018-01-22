using UnityEngine;

public class ScoreAdder : MonoBehaviour {

    public GameObject collected;
    public int score;
    private GameController controller;

    void Start () {
        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<GameController>();
        if (controller == null) {
            Debug.LogError("Unable to find the GameController script");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            controller.AddScore(score);
            Destroy(gameObject);
            GameObject tmp = Instantiate(collected, transform.position, transform.rotation);
            Destroy(tmp, 1);
        }
    }
}
