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
    private PlayerStats playerStats;
    [SerializeField]
    private GameObject playerBallModel;
    [SerializeField]
    private TrailRenderer balltrailRenderer;

    [Header("Parameters")]
    [SerializeField]
    private Material BLUTrailMaterial;
    [SerializeField]
    private Material REDTrailMaterial;

    public bool isCarrying; //Ball carrying toggle

    void Start()
    {
        switch (playerStats.playerTeam)
        {
            case Team.BLU:
                balltrailRenderer.material = BLUTrailMaterial;
                break;

            case Team.RED:
                balltrailRenderer.material = REDTrailMaterial;
                break;

            default:
                balltrailRenderer.material = BLUTrailMaterial;
                break;
        }
    }

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