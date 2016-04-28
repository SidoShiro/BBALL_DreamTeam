using UnityEngine;
using System.Collections;

public class Tremplin : MonoBehaviour {

    public Vector3 force;

	
	void OnTriggerEnter (Collider player) {
        player.GetComponent<Rigidbody>().AddForce(force);
	}
}
