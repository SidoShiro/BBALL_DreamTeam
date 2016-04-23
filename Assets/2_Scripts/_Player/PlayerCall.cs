using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to trigger calls on the player (Damage, kill, etc ...)
/// Those calls will be repercuted on the server via commands.
/// </summary>
public class PlayerCall : MonoBehaviour
{
    [Header("References(Player)")]
    #region References(Player)
    [SerializeField]
    private Transform playerCenter;
    [SerializeField]
    private NetworkIdentity playerIdentity;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCommand playerCommand;
    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private Rigidbody playerRigidBody;
    #endregion

    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private PlayerHUD playerHUD;
    #endregion

    [Header("Parameters")]
    [SerializeField]
    private float selfdmgfactor;

    /// <summary>
    /// Apllies damage to the player
    /// </summary>
    /// <param name="dmg">Damage dealt</param>
    public void Call_DamagePlayer(int dmg, string originIdentity, Team originTeam, DamageType damagetype)
    {
        if (playerStats.playerHealth - dmg <= 0)
        {
            Call_KillPlayer(originIdentity, originTeam, damagetype);
        }
        else
        {
            playerStats.playerHealth -= dmg;
        }
        Call_UpdateHealth();
    }

    /// <summary>
    /// Kills Player
    /// </summary>
    public void Call_KillPlayer(string killerName, Team killerTeam, DamageType damagetype)
    {
        playerCommand.Cmd_SendKill(killerName, playerIdentity.name, killerTeam, playerStats.playerTeam, damagetype);
        playerCommand.Cmd_KillPlayer(playerStats.playerTeam);
    }

    /// <summary>
    /// Kills Player
    /// </summary>
    public void Call_KillPlayer(string reason)
    {
        playerCommand.Cmd_SendKill(reason, playerIdentity.name, 0, playerStats.playerTeam, DamageType.Hazard);
        playerCommand.Cmd_KillPlayer(playerStats.playerTeam);
    }

    /// <summary>
    /// Kills Player
    /// </summary>
    public void Call_KillPlayer()
    {
        playerCommand.Cmd_KillPlayer(playerStats.playerTeam);
    }

    /// <summary>
    /// Freeze/unFreeze player
    /// </summary>
    public void Call_FreezePlayerUpdate()
    {
        GameObject sceneGM = GameObject.FindGameObjectWithTag("SceneGM");
        if (sceneGM != null)
        {
            bool newstate = sceneGM.GetComponent<SceneOverlord>().isFrozen;
            playerStats.isFrozen = newstate;
            playerStats.isReceivingInputs = !newstate;
        }
        else
        {
            Debug.Log("Could not find SceneGM/SceneOverlord in scene, make sure there is one in the scene");
        }
    }

    /// <summary>
    /// Call for a player respawn in specified team
    /// </summary>
    /// <param name="newteam"></param>
    public void Call_ChangePlayerTeam(Team newteam)
    {
        playerStats.playerTeam = newteam;
        Call_KillPlayer("");
    }

    /* Autocalls should never be called directly except if you really know what you are doing */
    #region AUTOCALLS

    /// <summary>
    /// Updates health display on HUD
    /// </summary>
    public void Call_UpdateHealth()
    {        
        playerHUD.UpdateHealth(playerStats.playerHealth);
    }

    /// <summary>
    /// Updates score display on HUD
    /// </summary>
    public void Call_UpdateScore()
    {
        //Deprecated
        if (GameObject.Find("ScoreGM") != null)
        {
            ScoreGM scoreGM = GameObject.Find("ScoreGM").GetComponent<ScoreGM>();
            int scoreBlu = scoreGM.score_blue;
            int scoreRed = scoreGM.score_red;
            playerHUD.UpdateScore(scoreBlu, scoreRed);
        }

        //New (Globalization of GMS)
        if (GameObject.Find("SceneGM") != null)
        {
            SceneOverlord sceneOverlord = GameObject.Find("SceneGM").GetComponent<SceneOverlord>();
            int scoreBLU = sceneOverlord.scoreBLU;
            int scoreRED = sceneOverlord.scoreRED;
            playerHUD.UpdateScore(scoreBLU, scoreRED);
        }
        else
        {
            Debug.Log("Did not find \"SceneGM\" when trying to update score");
        }
    }

    /// <summary>
    /// Call for this player to shoot a rocket ( !!! Should not be called by anything else than PlayerShoot !!! )
    /// </summary>
    /// <param name="rocketBody">Rocket prefab</param>
    /// <param name="playerFireOutputTransform">Transform to spawn the rocket from rocket</param>
    /// <param name="targetrotation">Rotation of the rocket when spawned</param>
    public void Call_ShootRocket(Vector3 targetposition, Quaternion targetrotation, Team newteam, NetworkIdentity ownerIdentity)
    {
        playerCommand.Cmd_ShootRocket(targetposition, targetrotation, newteam, ownerIdentity);
    }

    /// <summary>
    /// Add explosion force to this player ( !!! Should not be called by anything else than locally exploding rockets !!! )
    /// </summary>
    /// <param name="explosionpos"></param>
    public void Call_AddExplosionForce(Vector3 explosionpos)
    {
        playerMove.ForceAirborne();
        playerRigidBody.AddExplosionForce(8.0f, explosionpos, 2.0f, 0.1f, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Calculates damage according to position
    /// </summary>
    /// <param name="explosionpos"></param>
    public void Call_ExplosionDamage(Vector3 explosionpos, Team explosionTeam, NetworkIdentity ownerIdentity)
    {
        //Send explosion force
        Call_AddExplosionForce(explosionpos);

        //Calculates distance
        float distance = Vector3.Distance(playerCenter.position, explosionpos);
        if (playerStats.playerTeam == Team.SPE || distance > 2.0f || (explosionTeam == playerStats.playerTeam && ownerIdentity != playerIdentity))
        {
            return;
        }
        //Calculate damage
        float magnitude = (2.0f - distance) / 1.5f;
        int damage = (int)((2.0f - distance) * 100);
        //playerCommand.Cmd_SendHit(ownerIdentity, magnitude); //Used to debug hitmarker/hitsound

        //Damage application
        if (playerIdentity == ownerIdentity)
        {
            Call_DamagePlayer((int)(damage * selfdmgfactor), ownerIdentity.name, explosionTeam, DamageType.Explosion);
        }
        else
        {
            playerCommand.Cmd_SendHit(ownerIdentity, magnitude);
            Call_DamagePlayer(damage, ownerIdentity.name, explosionTeam, DamageType.Explosion);
        }
    }

    public void Call_ToggleHitMarker(float magnitude)
    {
        playerHUD.ToggleHitMarker(magnitude);
    }
    #endregion

}
