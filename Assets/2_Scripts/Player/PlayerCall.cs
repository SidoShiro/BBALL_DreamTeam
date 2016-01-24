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
    /// Creates new PlayerMenu
    /// </summary>
    public void Call_CreateUI(GameObject playerUI)
    {
        playerUI.GetComponent<PlayerMenu>().playerCall = this;
        previousUI = Instantiate(playerUI);
    }

    /// <summary>
    /// Call for a player respawn in specified team
    /// </summary>
    /// <param name="newteam"></param>
    public void Call_RespawnPlayer(PlayerStats.Team newteam)
    {
        Destroy(previousUI);
        playerCommand.Cmd_RespawnPlayer(newteam);
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
