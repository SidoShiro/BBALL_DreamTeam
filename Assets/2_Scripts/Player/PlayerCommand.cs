using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommand : NetworkBehaviour
{
    [Header("Player")]
    public GameObject playerRigidBody;
    public GameObject playerUI;

    [Header("Rocket")]
    public GameObject rocketBody;
    public Camera playerCamera;
    public Transform playerFireOutputTransform;

    [Command]
    public void Cmd_RespawnPlayer(PlayerStats.Team newteam)
    {
        GameObject[] spawnTab;
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
        Transform spawnPoint = spawnTab[Random.Range(0, (spawnTab.Length - 1))].transform;
        GameObject playerNew = (GameObject)Instantiate(playerRigidBody, spawnPoint.position, spawnPoint.rotation);
        playerNew.GetComponent<PlayerStats>().playerTeam = newteam;
        playerNew.name = "PlayerRigidBody(Clone)";
        NetworkServer.DestroyPlayersForConnection(connectionToClient);
        NetworkServer.AddPlayerForConnection(connectionToClient, playerNew, playerControllerId);
    }

    [Command]
    public void Cmd_ShootRocket(Vector3 targetposition, Quaternion targetrotation, PlayerStats.Team newteam)
    {
        GameObject rocket = (GameObject)Instantiate(rocketBody, targetposition, targetrotation);    //Spawns rocket at gunpoint with needed rotation
        rocket.GetComponent<RocketMove>().rocketTeam = newteam;                                     //Give rocket same layer as player
        rocket.GetComponent<RocketMove>().rocketRotation = targetrotation;
        NetworkServer.Spawn(rocket);
    }
}
