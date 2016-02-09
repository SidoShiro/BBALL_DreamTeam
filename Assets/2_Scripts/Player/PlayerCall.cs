using UnityEngine;

public class PlayerCall : MonoBehaviour
{
    [Header("References")]
    #region References
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCommand playerCommand;
    [SerializeField]
    private Rigidbody playerRigidBody;
    #endregion

    private GameObject previousUI;

    /// <summary>
    /// Apllies damage to the player
    /// </summary>
    /// <param name="dmg">Damage dealt</param>
    public void Call_DamagePlayer(int dmg)
    {
        if(playerStats.playerHealth - dmg <= 0)
        {
            Call_KillPlayer();
        }
        else
        {
            playerStats.playerHealth -= dmg;
        }
    }

    /// <summary>
    /// Kills Player
    /// </summary>
    public void Call_KillPlayer()
    {
        Destroy(previousUI);
        playerCommand.Cmd_KillPlayer(playerStats.playerTeam);
    }

    /// <summary>
    /// Call for a player respawn in specified team
    /// </summary>
    /// <param name="newteam"></param>
    public void Call_ChangePlayerTeam(PlayerStats.Team newteam)
    {
        playerStats.playerTeam = newteam;
        Call_KillPlayer();
    }

    /// <summary>
    /// Creates new PlayerMenu
    /// </summary>
    public void Call_CreateUI(GameObject playerUI)
    {
        playerUI.GetComponent<PlayerMenu>().playerCall = this;
        previousUI = Instantiate(playerUI);
    }

    /// <summary>
    /// Call for this player to shoot a rocket
    /// </summary>
    /// <param name="rocketBody">Rocket prefab</param>
    /// <param name="playerFireOutputTransform">Transform to spawn the rocket from rocket</param>
    /// <param name="targetrotation">Rotation of the rocket when spawned</param>
    public void Call_ShootRocket(Vector3 targetposition, Quaternion targetrotation, PlayerStats.Team newteam)
    {
        playerCommand.Cmd_ShootRocket(targetposition, targetrotation, newteam);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="explosionpos"></param>
    public void Call_AddExplosionForce(Vector3 explosionpos)
    {
        playerRigidBody.AddExplosionForce(10.0f, explosionpos, 2.0f, 0.1f, ForceMode.VelocityChange);
    }
}
