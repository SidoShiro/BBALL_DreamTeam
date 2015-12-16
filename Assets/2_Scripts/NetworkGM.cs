using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to connect to the master server and do shit if this fails
/// </summary>
public class NetworkGM : MonoBehaviour
{
    public Text NetworkInfoText;
    public Text RoomInfoText;

    /// <summary>
    /// Triggers once when the script loads
    /// </summary>
    void Awake()
    {
        //Connects to photon server
        PhotonNetwork.ConnectUsingSettings("0");
    }

    /// <summary>
    /// Triggers every frame
    /// </summary>
    void Update()
    {
        if (PhotonNetwork.connected)
        {
            NetworkInfoText.text = PhotonNetwork.connectionStateDetailed.ToString();    //Write infos about connection state
            RoomInfoText.text = "Rooms: " + PhotonNetwork.GetRoomList().Length;         //Write the numbers of available rooms
        }
    }

    /// <summary>
    /// Triggers whe nconnected to the master server
    /// </summary>
    void OnConnectedToMaster()
    {
        //Joins the main lobby on connection to master server
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
}
