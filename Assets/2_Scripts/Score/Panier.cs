using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class Panier : NetworkBehaviour {

    public ScoreGM scoreGM;
    public ParticleSystem particlesBLU;
    public ParticleSystem particlesRED;


    public PlayerStats.Team adv; // Team des adversaires

    /*
    Premier bug : yavais pas de event system sur la map pour que l'UI fonctionne
    Deuxieme : La collision recupere le collider et pas le player donc jai recupéré le PlayerStats dans le parent avec GetcomponentInparent
    */

    void OnTriggerEnter(Collider collider)
    {
        if (isServer)
        {
            GameObject hitObject = collider.gameObject;
            if (hitObject.tag == "PlayerCollider")
            {
                if (hitObject.GetComponentInParent<PlayerStats>().playerTeam == adv)
                {
                    scoreGM.TeamScored(adv);
                    Rpc_ParticlePlay(adv); 
                }
            }
        }
        
        
    }

    void Rpc_ParticlePlay(PlayerStats.Team adv)
    {
        if (adv == PlayerStats.Team.BLU)
        {
            particlesRED.Stop();
            particlesRED.time = 0;
            particlesRED.Play();
        }
        else if (adv == PlayerStats.Team.RED)
        {
            particlesBLU.Stop();
            particlesBLU.time = 0;
            particlesBLU.Play();
        }
    }

    
}
