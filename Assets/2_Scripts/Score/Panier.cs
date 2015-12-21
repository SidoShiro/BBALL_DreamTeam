using UnityEngine;
using System.Collections;

public class Panier : MonoBehaviour {

    public int team; // Team du panier
    public bool valid_point; // True si un point est marqué.

	// Use this for initialization
	void Start () {
        valid_point = false;
	}

    void OnTriggerEnter(Collider collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player.have_ball)
        {
            valid_point = true;
        }
        
    }

    
}
