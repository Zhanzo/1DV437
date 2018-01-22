using UnityEngine;

public class SpaceshipFollowPlayer : MonoBehaviour {

    public int tilt;
    public int speed;
    private PlayerController player;

    void Start () {
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        player = tmp.GetComponent<PlayerController>();
        if (player == null) {
            Debug.LogError("Unable to find the PlayerController script");
        }
    }
	
	void FixedUpdate () {
        if (player) {
            Rigidbody rb = GetComponent<Rigidbody>();
            float moveHorizontal = player.transform.position.x - transform.position.x;
            Vector3 movement = rb.velocity;
            movement.x = moveHorizontal * speed;
            rb.velocity = movement;  
        }
    }
}
