using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// This script should be attached to the PlayerSpawner object
/// It will recreate a player for that connection after a given amount of time.
/// The team and respawn time can be changed accordingly
/// </summary>
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject playerRigidBody;     //Player prefab to spawn
    [SerializeField]
    private Text playerRespawnTimeText;     //Text to display respawn time

    //Public
    [SyncVar]
    public PlayerStats.Team newteam;      //Team to respawn in (to set before respawn)
    [SyncVar]
    public string newname = "_N_";
    [SerializeField]
    private int respawntime = 2;   //Time before respawn (Spectator is always 0)

    private float spawntime;
    private bool b_CanRespawn;

    void Start()
    {
        //Instant respawn if in SPE Team
        if (newteam == PlayerStats.Team.SPE)
        {
            Cmd_CreatePlayer();
        }
        else
        {
            StartCoroutine(WaitUntilRespawn());
        }
    }

    IEnumerator WaitUntilRespawn()
    {
        spawntime = Time.time + respawntime;
        IEnumerator animation = AnimateRespawnTime();
        StartCoroutine(animation);

        yield return new WaitForSeconds(respawntime);

        StopCoroutine(animation);
        Cmd_CreatePlayer();
    }

    IEnumerator AnimateRespawnTime()
    {
        while (Time.time < spawntime)
        {
            float rtime = spawntime - Time.time;
            rtime = rtime < 0 ? 0 : rtime;
            playerRespawnTimeText.text = rtime.ToString("0.00");   //Format respawn timer and diplay it
            yield return null;
        }
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
        playerNew.GetComponent<PlayerStats>().playerName = newname == "_N_" ? m_Custom.RandomGUID(10) : newname;    //TODO : Add player nickname
        NetworkServer.DestroyPlayersForConnection(connectionToClient);                                              //Clean connection (prevents duplicate)
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerNew, playerControllerId);                //Instantiate new player
    }
}
