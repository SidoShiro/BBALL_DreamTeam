using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MatchConnector : MonoBehaviour
{
    public Text ConnectionAdress;   //The text containing ip adress (From DirectConnectPanel)

    private NetworkManager networkManager;  //Used to send server creation in the network

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        //Initialize components
        networkManager = GameObject.Find("NetworkGM").GetComponent<NetworkManager>();
    }

    /// <summary>
    /// Get current IP adress from the ConnectionAdress text and tries to connect to it
    /// </summary>
    public void ConnectToMatch()
    {
        networkManager.networkAddress = ConnectionAdress.text;
        networkManager.StartClient();
    }
}
