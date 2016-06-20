using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkOverlord : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public void ConnectToGame()
    {
        Text AdressText = GameObject.Find("AdressText").GetComponent<Text>();
        if(AdressText != null)
        {
            Debug.Log("Trying to connect to : " + AdressText.text);
            networkAddress = AdressText.text;
            StartClient();
        }
        else
        {
            Debug.Log("Error in finding adress for Conection");
        }
        
    }
}
