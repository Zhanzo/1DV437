using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public Boundary boundary;
    public float tilt;

    // Shots
    public GameObject shot;
    public GameObject strongShot;
    private bool strongShots = false;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private GameController controller;

    void Start() {
        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<GameController>();
        if (controller == null) {
            Debug.LogError("Unable to find the GameController script");
        }
    }

    void Update() {
        if (Input.GetKey("space") && Time.time > nextFire && !strongShots) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        } else if (Input.GetKey("space") && Time.time > nextFire && strongShots) {
            nextFire = Time.time + fireRate;
            Instantiate(strongShot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    public IEnumerator StrongShots() {
        strongShots = true;
        controller.ShowPowerup(0, 255, 0);
        yield return new WaitForSeconds(10);
        controller.HidePowerup();
        strongShots = false;
    }

    public IEnumerator FireRate() {
        float oldFireRate = fireRate;
        fireRate = 0.1f;
        controller.ShowPowerup(255, 0, 0);
        yield return new WaitForSeconds(10);
        controller.HidePowerup();
        fireRate = oldFireRate;
    }

    void FixedUpdate() {
        Rigidbody rb = GetComponent<Rigidbody>();

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.velocity = movement * speed;

        // Sets the limits on how far the ship can travel to the left and right.
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f);

        // Tilts the player when moving left or right
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
