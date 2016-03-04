﻿using UnityEngine;

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
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerCommand playerCommand;
    [SerializeField]
    private Rigidbody playerRigidBody;
    #endregion

    [Header("References(Interface)")]
    #region References(Interface)
    [SerializeField]
    private PlayerHUD playerHUD;
    #endregion

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
        Call_UpdateHealth();
    }

    /// <summary>
    /// Kills Player
    /// </summary>
    public void Call_KillPlayer()
    {
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
    /// Updates health display on HUD
    /// </summary>
    public void Call_UpdateHealth()
    {
        playerHUD.UpdateHealth(playerStats.playerHealth);
    }

    /// <summary>
    /// Updates ammo display on HUD
    /// </summary>
    public void Call_UpdateAmmo(int ammo)
    {
        playerHUD.UpdateAmmo(ammo);
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
        playerRigidBody.AddExplosionForce(10.0f, explosionpos, 2.0f, 0.0f, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Calculates damage according to position
    /// </summary>
    /// <param name="explosionpos"></param>
    public void Call_ExplosionDamage(Vector3 explosionpos, PlayerStats.Team explosionTeam)
    {
        //Damage calculs
        if (!Physics.Linecast(explosionpos, playerCenter.position, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
        {
            Call_AddExplosionForce(explosionpos);
            float distance = Vector3.Distance(playerCenter.position, explosionpos);  //Distance between player and explosion
            if (distance > 2.0f || playerStats.playerTeam == PlayerStats.Team.SPE || playerStats.playerTeam == explosionTeam)
            {
                return;
            }
            int damage = (int)((2.0f - distance) * 100 / 2.0f);

            //Damage application
            Call_DamagePlayer(damage);
        }
    }
    #endregion

}
