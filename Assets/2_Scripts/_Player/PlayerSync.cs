using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0f)]
public class PlayerSync : NetworkBehaviour
{
    [Header("References(Player)")]
    [SerializeField]
    private Transform playerTransform;

    [SyncVar]
    private Vector3 position;
    [SyncVar]
    private Quaternion rotation;

    void LateUpdate()
    {
        if (isLocalPlayer)
        {
            Cmd_SendTransform(playerTransform.position, playerTransform.rotation);
        }
        else
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, position, 0.5f);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, rotation, 0.5f);
        }
    }

    [Command]
    private void Cmd_SendTransform(Vector3 newposition, Quaternion newrotation)
    {
        position = newposition;
        rotation = newrotation;
    }
}
