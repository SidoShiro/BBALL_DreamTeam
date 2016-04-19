using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to handle the ball on the player when playing BBALL
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerBallHandle : NetworkBehaviour
{
    [Header("References(Player)")]
    #region References(Player)
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private GameObject playerBallModel;
    [SerializeField]
    private TrailRenderer balltrailRenderer;
    #endregion

    [Header("Parameters")]
    #region Parameters
    [SerializeField]
    private Material BLUTrailMaterial;
    [SerializeField]
    private Material REDTrailMaterial;
    #endregion

    public bool isCarrying; //Ball carrying toggle

    /// <summary>
    /// Called once when enabled
    /// </summary>
    void Start()
    {
        InitializeTrail();
    }

    /// <summary>
    /// Initialize trail component of ball according to team
    /// </summary>
    void InitializeTrail()
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

    /// <summary>
    /// Called to make the player carry a ball
    /// </summary>
    [ClientRpc]
    public void Rpc_PickBall()
    {
        isCarrying = true;
        playerBallModel.SetActive(true);
    }

    /// <summary>
    /// Called to make the player drop the ball (works even if he has none)
    /// </summary>
    [ClientRpc]
    public void Rpc_DropBall()
    {
        isCarrying = false;
        playerBallModel.SetActive(false);
    }
}