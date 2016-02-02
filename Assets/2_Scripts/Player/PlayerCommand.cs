using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This script is used to send player command to the server
/// /!\ THOSE COMMANDS SHOULD NEVER BE CALLED DIRECTLY /!\ 
/// >>>>> Use PlayerCall instead! <<<<<
/// </summary>
public class PlayerCommand : NetworkBehaviour
{
    [Header("Player")]
    public GameObject playerRigidBody;  //Player Prefab to spawn
    public GameObject playerUI;         //Player UI to spawn

    [Header("Rocket")]
    public GameObject rocketBody;   //Rocket prefab to spawn

    /// <summary>
    /// Destroys this player and command the server to spawn a new one
    /// </summary>
    /// <param name="newteam">Player team to spawn the player in</param>
    [Command]
    public void Cmd_RespawnPlayer(PlayerStats.Team newteam)
    {
        GameObject[] spawnTab;  //Array of possible spawns
        switch (newteam)
        {
            case PlayerStats.Team.BLU:
                spawnTab = GameObject.FindGameObjectsWithTag("BLUSpawn");
                break;

            case PlayerStats.Team.RED:
                spawnTab = GameObject.FindGameObjectsWithTag("REDSpawn");
                break;

            default:
                spawnTab = GameObject.FindGameObjectsWithTag("SPESpawn");
                break;
        }
        Transform spawnPoint = spawnTab[Random.Range(0, (spawnTab.Length - 1))].transform;                          //Pick random spawn
        GameObject playerNew = (GameObject)Instantiate(playerRigidBody, spawnPoint.position, spawnPoint.rotation);  //Spawns new player
        playerNew.GetComponent<PlayerStats>().playerTeam = newteam;                                 //Set player team accordingly
        playerNew.name = "PlayerRigidBody(Clone)";                                                  //TODO : Add player nickname
        NetworkServer.DestroyPlayersForConnection(connectionToClient);                              //Destroy all previous players for this conection
        NetworkServer.AddPlayerForConnection(connectionToClient, playerNew, playerControllerId);    //Instantiate new player
    }

    /// <summary>
    /// 
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
