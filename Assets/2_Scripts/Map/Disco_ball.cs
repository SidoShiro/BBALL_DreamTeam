using UnityEngine;
using System.Collections;

public class Disco_ball : MonoBehaviour {

    public int Rotate_speed;

	// Update is called once per frame
	void Update () {
       // transform.Rotate(Vector3.right * Time.deltaTime * Rotate_speed);
        transform.Rotate(Vector3.up * Time.deltaTime * Rotate_speed);
        transform.Rotate(Vector3.left * Time.deltaTime * Rotate_speed / 2);
    }
}
