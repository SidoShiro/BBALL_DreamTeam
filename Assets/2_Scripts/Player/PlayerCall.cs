using UnityEngine;

/// <summary>
/// This script is used to trigger calls on the player (Damage, kill, etc ...)
/// Those calls will be repercuted on the server via commands.
/// </summary>
public class PlayerCall : MonoBehaviour
{
    [Header("References")]
    #region References
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCommand playerCommand;
    [SerializeField]
    private Rigidbody playerRigidBody;
    #endregion

    //Local
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

    #region AUTOCALLS

    /// <summary>
    /// Creates new PlayerMenu ( !!! Should not be called by anything else than PlayerEnabler !!! )
    /// </summary>
    public void Call_CreateUI(GameObject playerUI)
    {
        playerUI.GetComponent<PlayerMenu>().playerCall = this;
        playerUI.GetComponent<PlayerHUD>().playerStats = playerStats;
        playerUI.GetComponent<PlayerHUD>().enabled = true;
        previousUI = Instantiate(playerUI);
    }

    /// <summary>
    /// Call for this player to shoot a rocket ( !!! Should not be called by anything else than PlayerShoot !!! )
    /// </summary>
    /// <param name="rocketBody">Rocket prefab</param>
    /// <param name="playerFireOutputTransform">Transform to spawn the rocket from rocket</param>
    /// <param name="targetrotation">Rotation of the rocket when spawned</param>
    public void Call_ShootRocket(Vector3 targetposition, Quaternion targetrotation, PlayerStats.Team newteam)
    {
        playerCommand.Cmd_ShootRocket(targetposition, targetrotation, newteam);
    }

    /// <summary>
    /// Add explosion force to this player ( !!! Should not be called by anything else than locally exploding rockets !!! )
    /// </summary>
    /// <param name="explosionpos"></param>
    public void Call_AddExplosionForce(Vector3 explosionpos)
    {
        playerRigidBody.AddExplosionForce(10.0f, explosionpos, 2.0f, 0.1f, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Calculates damage according to position
    /// </summary>
    /// <param name="explosionpos"></param>
    public void Call_ExplosionDamage(Vector3 explosionpos, PlayerStats.Team rocketTeam)
    {
        //Damage calculs
        float distance = Vector3.Distance(playerTransform.position, explosionpos);  //Distance between player and explosion
        if (distance > 2.0f || playerStats.playerTeam == PlayerStats.Team.SPE || playerStats.playerTeam == rocketTeam)
        {
            return;
        }
        int damage = (int)((2.0f - distance) * 100 / 2.0f);

        //Damage application
        Call_DamagePlayer(damage);
    }
    #endregion

}
