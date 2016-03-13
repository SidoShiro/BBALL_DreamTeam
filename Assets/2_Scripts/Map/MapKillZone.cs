using UnityEngine;
using UnityEngine.Networking;

public class MapKillZone : NetworkBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        GameObject playerRigidBody = col.gameObject;
        if (isServer && playerRigidBody.tag == "Player")
        {
            playerRigidBody.GetComponent<PlayerCommand>().Rpc_KillPlayer();
        }
    }
}
