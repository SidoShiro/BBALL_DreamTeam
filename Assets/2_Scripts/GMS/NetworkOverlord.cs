using UnityEngine;
using UnityEngine.Networking;

public class NetworkOverlord : NetworkManager
{
    public GameObject playerGM;
    public GameObject playerRigidBody;
    public PlayerStats.Team playerTeam = PlayerStats.Team.BLU;
    public Vector3 playerSpawnPos = Vector3.up;

    /*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject playerNew = (GameObject)Instantiate(playerRigidBody, playerSpawnPos, Quaternion.identity);
        playerNew.GetComponent<PlayerStats>().playerTeam = playerTeam;
        NetworkServer.AddPlayerForConnection(conn, playerNew, playerControllerId);
    }
    //*/
}
