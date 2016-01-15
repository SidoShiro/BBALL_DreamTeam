using UnityEngine;
using UnityEngine.Networking;

public class Panier : NetworkBehaviour {

    public ScoreGM scoreGM;

    public PlayerStats.Team adv; // Team des adversaires


    /*
    Premier bug : yavais pas de event system sur la map pour que l'UI fonctionne
    Deuxieme : La collision recupere le collider et pas le player donc jai recupéré le PlayerStats dans le parent avec GetcomponentInparent
    */

    void OnTriggerEnter(Collider collider)
    {
        GameObject hitObject = collider.gameObject;
        if (hitObject.tag == "PlayerCollider")
        {
            if (hitObject.GetComponentInParent<PlayerStats>().playerTeam == adv)
            {
                Debug.Log("Adv");
                scoreGM.TeamScored(adv);
            }
        }
        
    }

    
}
