using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MatchConnector : MonoBehaviour
{
    public Text ConnectionAdress;   //The text containing ip adress (From DirectConnectPanel)

    private NetworkOverlord networkOverlord;  //Used to send server creation in the network

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        //Initialize components
        networkOverlord = GameObject.Find("NetworkGM").GetComponent<NetworkOverlord>();
    }

    /// <summary>
    /// Get current IP adress from the ConnectionAdress text and tries to connect to it
    /// </summary>
    public void ConnectToMatch()
    {
        networkOverlord.networkAddress = ConnectionAdress.text;
        networkOverlord.StartClient();
    }
}
