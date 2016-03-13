using UnityEngine;
using System.Collections;

public class Fall_solo : MonoBehaviour {

    Vector3 destination = new Vector3(0.608f, 0.21f, -8.083f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {

        col.transform.GetComponent<PlayerMove>().SetGrounded(false);

        col.transform.position = destination;
    }
}
