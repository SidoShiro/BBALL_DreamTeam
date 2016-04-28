using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

    public int force;
	
	void OnTriggerStay (Collider player) {

        player.GetComponent<Rigidbody>().velocity *= 3;
	
	}
}
