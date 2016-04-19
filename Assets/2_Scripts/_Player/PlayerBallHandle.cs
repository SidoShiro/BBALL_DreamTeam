using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerBallHandle : NetworkBehaviour
{
    //TODO : Comment
    [Header("References(Player)")]
    [SerializeField]
    private GameObject playerBallModel;

    public bool isCarrying; //Ball carrying toggle

    [ClientRpc]
    public void Rpc_PickBall()
    {
        isCarrying = true;
        playerBallModel.SetActive(true);
    }

    [ClientRpc]
    public void Rpc_DropBall()
    {
        isCarrying = false;
        playerBallModel.SetActive(false);
    }
}