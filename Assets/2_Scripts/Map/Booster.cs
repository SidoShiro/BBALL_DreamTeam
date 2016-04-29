using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

    public int force = 10;
	
	void OnCollisionEnter (Collision player) {

        player.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.VelocityChange);
	
	}
}
