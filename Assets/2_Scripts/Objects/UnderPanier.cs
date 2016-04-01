using UnityEngine;
using UnityEngine.Networking;

public class UnderPanier : MonoBehaviour
{
    public PlayerStats.Team adv;
    private int playerH;
    //public PlayerStats player; 

    //empeche le joueur de passer en dessous du panier sans pour autant l'ampecher de marquer

    void OnTriggerEnter(Collider collider)
    {

        GameObject player = collider.gameObject;
        if (player.tag == "PlayerCollider")
        {
            if (player.GetComponentInParent<PlayerStats>().playerTeam == adv)
            {
                playerH = player.GetComponentInParent<PlayerStats>().playerHealth;
                player.GetComponentInParent<PlayerCall>().Call_DamagePlayer(playerH, " ", PlayerStats.Team.SPE);
                /*un joueur adverse qui passe en dessous, repop sans marquer le point
                un joueur adverse qui passe par dessus, repop après avoir marqué le point -> changer le temps de respawn
                */
                Debug.Log("Refaire apparaitre le joueur autre part sur la map (l'empeche de passer par en-dessous)"); 
            }
        }
    }
}
