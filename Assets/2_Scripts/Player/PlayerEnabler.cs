using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to enable client specific components that are disabled by default server wise:
/// We only want to control our player not others
/// We only want one camera active: our player's camera
/// etc...
/// </summary>
public class PlayerEnabler : NetworkBehaviour
{
    [Header("Enable on client")]
    public Component[] scripts; //List of scripts (Mainly inputs scripts) to enable client side

    [Header("Others")]
    public GameObject playerModel;      //Self-explanatory
    public Rigidbody playerRigidBody;   //Self-explanatory
    public Camera playerCamera;         //Self-explanatory

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        if (isLocalPlayer)
        {
            //Enable client side scripts (So you only control this player and not others)
            foreach (MonoBehaviour mono in scripts)
            {
                mono.enabled = true;
            }

            //Enable client side objects
            if (GetComponent<PlayerStats>().playerTeam != PlayerStats.Team.SPE) //TODO : Clean that shit
            {
                playerRigidBody.isKinematic = false; 
            }    
            else
            {
                playerRigidBody.isKinematic = true;
            }
            playerModel.gameObject.layer = 10;      //Place PlayerModel on "SPE" layer to disable rendering for this client
            playerCamera.enabled = true;            //Enables the camera component of this player
        }
    }
}
