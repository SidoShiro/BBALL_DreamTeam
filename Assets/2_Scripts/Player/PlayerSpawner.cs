using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


/// <summary>
/// This script should be attached to the PlayerSpawner object
/// It will recreate a player for that connection after a given amount of time.
/// The team and respawn time can be changed accordingly
/// </summary>
public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject playerRigidBody;     //Player prefab to spawn
    [SerializeField]
    private Text playerRespawnTimeText;     //Text to display respawn time

    //Public
    [SyncVar]
    public PlayerStats.Team newteam;    //Team to respawn in (to set before respawn)
    public int respawntime = 2;         //Time before respawn (Spectator is always 0)

    //Local
    private float spawntime;
    private bool stop = false;

    void Start()
    {
        //Instant respawn if in SPE Team
        if (newteam == PlayerStats.Team.SPE)
        {
            Cmd_CreatePlayer();
        }
        else
        {
            spawntime = Time.time + respawntime;
        }
    }

    void Update()
    {
        //Timer
        float time = Time.time;

        //Timer checker
        if (time > spawntime && !stop)
        {
            Cmd_CreatePlayer();
            stop = true;
        }

        float rtime = spawntime - time;
        if (rtime < 0)
        {
            rtime = 0;
        }

        playerRespawnTimeText.text = rtime.ToString("0.00");   //Display format respawn timer and siplay it

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
        NetworkServer.DestroyPlayersForConnection(connectionToClient);                                              //Clean connection (prevents duplicate)
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerNew, playerControllerId);                //Instantiate new player
    }
}
