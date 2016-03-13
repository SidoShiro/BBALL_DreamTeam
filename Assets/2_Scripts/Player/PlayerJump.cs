using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public int force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {

        col.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.VelocityChange);
    }
}
