using UnityEngine;
using System.Collections;

public class Robot_tuto : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col) {
        Destroy(gameObject);
	}
}
