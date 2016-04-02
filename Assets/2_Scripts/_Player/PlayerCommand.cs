using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to send player command to the server
/// /!\ THOSE COMMANDS SHOULD NEVER BE CALLED DIRECTLY /!\ 
/// >>>>> Use PlayerCall instead! <<<<<
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerCommand : NetworkBehaviour
{
    [Header("References(Player)")]
    #region References(Player)
    [SerializeField]
    private NetworkIdentity playerIdentity;
    [SerializeField]
    private PlayerCall playerCall;
    #endregion

    [Header("Prefabs")]
    #region Instantiation(Prefabs)
    [SerializeField]
    private GameObject playerSpawner;   //Player spawner prefab
    [SerializeField]
    private GameObject playerRigidBody; //Player prefab
    [SerializeField]
    private GameObject rocketBody;      //Rocket prefab
    #endregion

    /// <summary>
    /// Kills the player, and creates a spawner for that player with given team
    /// </summary>
    /// <param name="newteam">Team the player will respawn in</param>
    [Command]
    public void Cmd_KillPlayer(PlayerStats.Team newteam)
    {
        GameObject spawner = Instantiate(playerSpawner);                                            //Creates spawner                
        spawner.GetComponent<PlayerSpawner>().newteam = newteam;                                    //Set spawner team
        NetworkServer.DestroyPlayersForConnection(connectionToClient);                              //Clean connection (to prevent duplicates)
        NetworkServer.ReplacePlayerForConnection(connectionToClient, spawner, playerControllerId);  //Instantiate spawner on network
    }

    /// <summary>
    /// Command the server to spawn a rocket at given position/rotation
    /// </summary>
    /// <param name="targetposition">Position to spawn the rocket at</param>
    /// <param name="targetrotation">Rotation to give the rocket</param>
    /// <param name="newteam">Team of the rocket</param>
    [Command]
    public void Cmd_ShootRocket(Vector3 targetposition, Quaternion targetrotation, PlayerStats.Team newteam, NetworkIdentity ownerIdentity)
    {
        //rocketBody.transform.rotation = targetrotation;
        GameObject rocket = (GameObject)Instantiate(rocketBody, targetposition, targetrotation);    //Spawns rocket at gunpoint with needed rotation
        rocket.GetComponent<RocketMove>().ownerIdentity = ownerIdentity;                            //Set the rocket owner accordingly
        rocket.GetComponent<RocketMove>().rocketTeam = newteam;                                     //Set the rocket team accordingly
        rocket.GetComponent<RocketMove>().rocketRotation = targetrotation;                          //Set rocket starting rotation
        NetworkServer.Spawn(rocket);                                                                //Instantiate new rocket on the server
    }

    /// <summary>
    /// Sends a hit info to the player corresponding to the identity
    /// </summary>
    /// <param name="ownerIdentity">Identity of the player to send the hit to</param>
    /// <param name="magnitude">Magnitude of the hit</param>
    [Command]
    public void Cmd_SendHit(NetworkIdentity ownerIdentity, float magnitude)
    {
        ownerIdentity.gameObject.GetComponent<PlayerCommand>().Rpc_GetHit(magnitude);
    }

    [Command]
    public void Cmd_SendKill(string killerIdentity, string victimIdentity, int killerTeam, int victimTeam)
    {
        GameObject.FindGameObjectWithTag("KillFeedPanel").GetComponent<KillFeedInput>().Rpc_ParseKill(killerIdentity, victimIdentity, killerTeam, victimTeam);
    }

    /// <summary>
    /// Receives a hit
    /// </summary>
    /// <param name="magnitude">Magnitude of the hit</param>
    [ClientRpc]
    public void Rpc_GetHit(float magnitude)
    {
        if (isLocalPlayer)
        {
            playerCall.Call_ToggleHitMarker(magnitude);
        }
    }

    /// <summary>
    /// Calls for an update of the score on this player
    /// </summary>
    [ClientRpc]
    public void Rpc_UpdateScore()
    {
        playerCall.Call_UpdateScore();
    }

    /// <summary>
    /// Kills player
    /// </summary>
    [ClientRpc]
    public void Rpc_KillPlayer(string reason)
    {
        playerCall.Call_KillPlayer(reason);
    }
}
