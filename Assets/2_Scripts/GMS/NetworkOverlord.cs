using UnityEngine;
using UnityEngine.Networking;

public class NetworkOverlord : NetworkManager
{
    public GameObject playerRigidBody;
    public GameObject playerUI;
    public PlayerStats.Team playerTeam;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerRigidBody, Vector3.zero, Quaternion.identity);
        player.GetComponent<PlayerStats>().playerTeam = playerTeam;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        GameObject newplayerUI = (GameObject)Instantiate(playerUI, Vector3.zero, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(newplayerUI, conn);
    }
}
