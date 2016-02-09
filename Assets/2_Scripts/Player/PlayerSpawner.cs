using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject playerRigidBody;

    public PlayerStats.Team newteam = PlayerStats.Team.SPE;
    public int respawntime = 2;

    void Start()
    {
        StartCoroutine(WaitFor(respawntime));
    }

    IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Cmd_CreatePlayer();
    }

    [Command]
    private void Cmd_CreatePlayer()
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
        playerNew.GetComponent<PlayerStats>().playerTeam = newteam;                                                 //Set player team accordingly
        playerNew.name = "Bob";                                                                                     //TODO : Add player nickname
        NetworkServer.DestroyPlayersForConnection(connectionToClient);
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerNew, playerControllerId);                //Instantiate new player
    }
}
