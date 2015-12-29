using UnityEngine;
using UnityEngine.Networking;

public class NetworkOverlord : NetworkManager
{
    public GameObject playerGM;
    public GameObject playerUI;
    public GameObject playerRigidBody;
    public PlayerStats.Team playerTeam = PlayerStats.Team.SPE;
    public Vector3 playerSpawnPos = Vector3.zero;

    //*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject playerNew;
        switch (playerControllerId)
        {
            case 0:
                playerNew = (GameObject)Instantiate(playerGM, Vector3.zero, Quaternion.identity);
                NetworkServer.AddPlayerForConnection(conn, playerNew, playerControllerId);
                break;

            case 1:
                playerNew = (GameObject)Instantiate(playerUI, Vector3.zero, Quaternion.identity);
                NetworkServer.AddPlayerForConnection(conn, playerNew, playerControllerId);
                break;

            case 2:
                playerNew = (GameObject)Instantiate(playerRigidBody, playerSpawnPos, Quaternion.identity);
                playerNew.GetComponent<PlayerStats>().playerTeam = playerTeam;
                NetworkServer.AddPlayerForConnection(conn, playerNew, playerControllerId);
                break;

            default:
                Debug.Log("SHOULD NOT HAPPEND ! WTF DID YOU DO ?");
                break;
        }
    }
    //*/
}
