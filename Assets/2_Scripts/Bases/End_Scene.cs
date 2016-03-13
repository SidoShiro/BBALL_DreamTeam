using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End_Scene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {

        SceneManager.LoadScene("_MainMenu");
    }
}
