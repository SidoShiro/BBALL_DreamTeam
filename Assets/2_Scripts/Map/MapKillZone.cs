using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class MapKillZone : NetworkBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        GameObject playerRigidBody = col.gameObject;

        if (playerRigidBody.tag == "Player")
        {
            playerRigidBody.GetComponent<PlayerCall>().Call_KillPlayer("");
        }
    }
}
