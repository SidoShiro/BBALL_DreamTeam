using UnityEngine;

/// <summary>
/// This script, when attached to any object with a collider, will kill any player colliding with it
/// </summary>
public class KillZone : MonoBehaviour
{
    /// <summary>
    /// Called when something collide
    /// </summary>
    /// <param name="col">Collision information</param>
    void OnCollisionEnter(Collision col)
    {
        GameObject playerRigidBody = col.gameObject;    //Get object related to collision 
        if (playerRigidBody.tag == "Player")            //If it's a player, kills it
        {
            playerRigidBody.GetComponent<PlayerCall>().Call_KillPlayer("");
        }
    }
}
