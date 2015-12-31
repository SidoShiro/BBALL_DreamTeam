using UnityEngine;
using UnityEngine.Networking;

public class PlayerOverlord : NetworkBehaviour
{
    public GameObject playerRigidBody;

    void Start()
    {
        Cmd_CreateFirstPlayer();
    }

    [Command]
    private void Cmd_CreateFirstPlayer()
    {
        GameObject[] spawnTab = GameObject.FindGameObjectsWithTag("SPESpawn");

        Transform spawnPoint = spawnTab[Random.Range(0, (spawnTab.Length - 1))].transform;
        GameObject playerNew = (GameObject)Instantiate(playerRigidBody, spawnPoint.position, spawnPoint.rotation);
        playerNew.GetComponent<PlayerStats>().playerTeam = PlayerStats.Team.SPE;
        playerNew.name = "PlayerRigidBody(Clone)";
        NetworkServer.DestroyPlayersForConnection(connectionToClient);
        NetworkServer.AddPlayerForConnection(connectionToClient, playerNew, playerControllerId);
    }
}
