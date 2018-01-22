using UnityEngine;

public class EnemyFire : MonoBehaviour {

    // Shots
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    // Update is called once per frame
    void Update() {
        if (Time.time > nextFire && GameObject.FindWithTag("Player")) {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            nextFire = Time.time + fireRate;
        }
    }
}
