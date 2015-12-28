using UnityEngine;

/// <summary>
/// This script is used to set the player material and layer according to his team
/// </summary>
public class PlayerColor : MonoBehaviour
{
    public PlayerStats playerStats;         //Self-explanatory
    public GameObject playerCollider;       //Self-explanatory
    public Renderer playerModelRenderer;    //Self-explanatory

    [Header("Materials")]
    public Material SPEMaterial;    //Material to use when on SPE team
    public Material BLUMaterial;    //Material to use when on BLU team
    public Material REDMaterial;    //Material to use when on RED team

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        //Set material and layer according to team
        switch (playerStats.playerTeam)
        {
            case PlayerStats.Team.BLU:
                playerCollider.gameObject.layer = 11;
                playerModelRenderer.material = BLUMaterial;
                break;

            case PlayerStats.Team.RED:
                playerCollider.gameObject.layer = 12;
                playerModelRenderer.material = REDMaterial;
                break;

            default:
                playerCollider.gameObject.layer = 10;
                playerModelRenderer.material = SPEMaterial;
                break;
        }
    }
}
