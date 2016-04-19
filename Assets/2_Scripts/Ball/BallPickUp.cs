using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class BallPickUp : NetworkBehaviour
{
    //TODO : Comment here
    /// <summary>
    /// Called when something collide
    /// </summary>
    /// <param name="col">Collision information</param>
    void OnTriggerEnter(Collider col)
    {
        if (isServer)
        {
            GameObject playerRigidBody = col.transform.parent.gameObject; 
            if (playerRigidBody.tag == "Player")
            {
                playerRigidBody.GetComponent<PlayerBallHandle>().Rpc_PickBall();
                Rpc_DestroyBall();
            }
        }
    }
    
    [ClientRpc]
    void Rpc_DestroyBall()
    {
        Destroy(gameObject);
    }
}