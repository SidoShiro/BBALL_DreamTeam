using UnityEngine;
using System.Collections;

public class ScoreGM : MonoBehaviour {

    // Paniers 
    public Panier panier_blue;
    public Panier panier_red;

    // Scores
    public int score_blue;
    public int score_red;


    private void checkPoint(ref int score, Panier panier)
    {
       if (panier.valid_point)
        {
            score++;
            panier.valid_point = false;
        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        checkPoint(ref score_blue, panier_red);
        checkPoint(ref score_red, panier_blue);
	}
}
