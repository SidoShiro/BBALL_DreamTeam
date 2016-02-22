using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to send player command to the server
/// /!\ THOSE COMMANDS SHOULD NEVER BE CALLED DIRECTLY /!\ 
/// >>>>> Use PlayerCall instead! <<<<<
/// </summary>
public class PlayerCommand : NetworkBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject playerSpawner;    //Player spawner prefab
    [SerializeField]
    private GameObject playerRigidBody;  //Player prefab

    [Header("Rocket")]
    [SerializeField]
    private GameObject rocketBody;   //Rocket prefab

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
    public void Cmd_ShootRocket(Vector3 targetposition, Quaternion targetrotation, PlayerStats.Team newteam)
    {
        GameObject rocket = (GameObject)Instantiate(rocketBody, targetposition, targetrotation);    //Spawns rocket at gunpoint with needed rotation
        rocket.GetComponent<RocketMove>().rocketTeam = newteam;                                     //Give rocket same layer as player
        rocket.GetComponent<RocketMove>().rocketRotation = targetrotation;                          //Set rocket starting rotation
        NetworkServer.Spawn(rocket);                                                                //Instantiate new rocket
    }
}
