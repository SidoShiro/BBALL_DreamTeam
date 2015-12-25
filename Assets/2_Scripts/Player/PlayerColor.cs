using UnityEngine;

/// <summary>
/// This script is used to set the player material according to his team
/// </summary>
public class PlayerColor : MonoBehaviour
{
    public GameObject playerModel;  //Self explanatory

    [Header("Teams materials")]
    public Material SPEMaterial;    //Material to use when on SPE team
    public Material BLUMaterial;    //Material to use when on BLU team
    public Material REDMaterial;    //Material to use when on RED team

    private PlayerStats playerStats;

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    /// <summary>
    /// Triggered when script is enabled
    /// </summary>
    void Start()
    {
        //Set material according to team
        switch (playerStats.playerTeam)
        {
            case PlayerStats.Team.BLU:
                playerModel.GetComponent<Renderer>().material = BLUMaterial;
                break;

            case PlayerStats.Team.RED:
                playerModel.GetComponent<Renderer>().material = REDMaterial;
                break;

            default:
                playerModel.GetComponent<Renderer>().material = SPEMaterial;
                break;
        }
    }
}
