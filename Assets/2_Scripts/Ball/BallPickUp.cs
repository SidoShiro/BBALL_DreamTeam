using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is attached to the ball and is used to detect player picking up the ball
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class BallPickUp : NetworkBehaviour
{
    /// <summary>
    /// Called when something enters trigger
    /// </summary>
    /// <param name="col">Collision information</param>
    void OnTriggerEnter(Collider col)
    {
        if (isServer)   //We only want to check triggers on the server
        {
            GameObject playerRigidBody = col.transform.parent.gameObject;           //Find the parent of the collider
            if (playerRigidBody.tag == "Player")
            {
                playerRigidBody.GetComponent<PlayerBallHandle>().Rpc_PickBall();    //Tell the player he picked the ball on all clients
                Rpc_DestroyBall();                                                  //Destroy the ball on all clients
            }
        }
    }
    
    /// <summary>
    /// Called to destroy the ball on all clients
    /// </summary>
    [ClientRpc]
    void Rpc_DestroyBall()
    {
        Destroy(gameObject);
    }
}