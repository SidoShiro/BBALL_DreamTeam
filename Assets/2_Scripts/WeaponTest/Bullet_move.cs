using UnityEngine;
using System.Collections;

public class Bullet_move : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime;
	
	}
}
