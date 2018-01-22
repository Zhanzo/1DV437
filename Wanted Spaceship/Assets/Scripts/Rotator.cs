using UnityEngine;

public class Rotator : MonoBehaviour {

    public Vector3 torque;

	void Start () {
        GetComponent<Rigidbody>().AddTorque(torque.x, torque.y, torque.z);
    }
}
