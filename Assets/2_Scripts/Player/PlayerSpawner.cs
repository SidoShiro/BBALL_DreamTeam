using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject playerRigidBody;
    [SerializeField]
    private Text playerRespawnTimeText;

    public PlayerStats.Team newteam = PlayerStats.Team.SPE;
    public int respawntime = 2;

    private float spawntime;
    private bool stop = false;

    void Start()
    {
        spawntime = Time.time + respawntime;
    }

    void Update()
    {
        float time = Time.time;

        if (time > spawntime && !stop)
        {
            Cmd_CreatePlayer();
            stop = true;
        }
            
        playerRespawnTimeText.text = (spawntime - time).ToString("0.00");

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
