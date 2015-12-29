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

    void CreateUI()
    {
        playerUI.GetComponent<PlayerMenu>().playerCommand = this;
        previousUI = Instantiate(playerUI);
    }

    public void CallRespawnPlayer(PlayerStats.Team newteam)
    {
        Destroy(previousUI);
        CmdRespawnPlayer(newteam);
    }

    [Command]
    void CmdRespawnPlayer(PlayerStats.Team newteam)
    {
        GameObject playerNew = (GameObject)Instantiate(playerRigidBody, Vector3.zero, Quaternion.identity);
        playerNew.GetComponent<PlayerStats>().playerTeam = newteam;
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerNew, playerControllerId);
        NetworkServer.Destroy(gameObject);
    }

}
