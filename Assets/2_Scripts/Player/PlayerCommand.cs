using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommand : NetworkBehaviour
{
    public GameObject playerRigidBody;
    public GameObject playerUI;

    private GameObject previousUI;

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Start()
    {
        if (isLocalPlayer)
        {
            CreateUI();
        }
    }

    /// <summary>
    /// Creates new PlayerUI and destroys previous one
    /// </summary>
    void CreateUI()
    {
        playerUI.GetComponent<PlayerMenu>().playerCommand = this;
        previousUI = Instantiate(playerUI);
    }

    /// <summary>
    /// Call for a player respawn in specified team
    /// </summary>
    /// <param name="newteam"></param>
    public void CallRespawnPlayer(PlayerStats.Team newteam)
    {
        Destroy(previousUI);
        CmdRespawnPlayer(newteam);
    }

    [Command]
    void CmdRespawnPlayer(PlayerStats.Team newteam)
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
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerNew, playerControllerId);
        NetworkServer.Destroy(gameObject);
    }

}
