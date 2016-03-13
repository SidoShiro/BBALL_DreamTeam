using UnityEngine;
using System.Collections;

public class Robot_tuto : MonoBehaviour {

    public Object game;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {

        Destroy(game);
	}
}
