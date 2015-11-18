using UnityEngine;
using UnityEngine.Networking;

public class Panier : NetworkBehaviour {

    public ScoreGM scoreGM;

    public PlayerStats.Team adv; // Team des adversaires

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision");
        GameObject hitObject = collider.gameObject;
        if (hitObject.tag == "Player")
        {
            Debug.Log("IsPlayer");
            if (hitObject.GetComponent<PlayerStats>().playerTeam == adv)
            {
                Debug.Log("Adv");
                scoreGM.TeamScored(adv);
            }
        }
        
    }

    
}
