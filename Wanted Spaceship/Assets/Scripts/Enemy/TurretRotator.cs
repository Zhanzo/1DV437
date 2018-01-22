using UnityEngine;

public class TurretRotator : MonoBehaviour {

    private PlayerController player;

    void Start() {
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        player = tmp.GetComponent<PlayerController>();
        if (player == null) {
            Debug.LogError("Unable to find the PlayerController script");
        }
    }

    void FixedUpdate() {
        if (player != null) {
            Quaternion startRotation = Quaternion.Euler(0, -180, 0);
            Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position) * startRotation;
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }      
    }
}
