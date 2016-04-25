using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

/// <summary>
/// This script is used for LAN and ONLINE match creation.
/// </summary>
public class MatchCreator : MonoBehaviour
{
    public Text NickName;           //The text containing player name   (From MenuButtonsPanel)
    public Text ServerName;         //The text containing server name   (From CreateGamePanel)
    public Dropdown MapName;        //The dropdown for map selection    (From CreateGamePanel)

    private NetworkOverlord networkOverlord;    //Used to send server creation in the network
    private NetworkMatch networkMatch;          //Used to send match creation in the network

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        //Initialize components
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        networkOverlord = GameObject.Find("NetworkGM").GetComponent<NetworkOverlord>();
    }

    /// <summary>
    /// Creates an LAN/ONLINE game according to the checked toggle
    /// </summary>
    public void CreateMatch()
    {
        CreateMatchRequest();
    }

    /// <summary>
    /// Creates a custom match with custom parameters if found
    /// </summary>
    private void CreateMatchRequest()
    {
        MatchRequestCustom MatchRequest = new MatchRequestCustom();
        MatchRequest.name = ServerName.text + "|" + MapName.options[0].text + "|" + NickName.text;
        MatchRequest.size = 10;                                             //Max numbers of player in a match (Spectators included)
        MatchRequest.advertise = true;                                      //The match is visible to all on the network
        MatchRequest.password = "";                                         //TODO : Add passwords

        MatchRequest.matchAttributesCustom = new Dictionary<string, string>();
        MatchRequest.matchAttributesCustom.Add(MatchProperty.HostName, NickName.text);          //Name of original creator of the match (e.g. "DarkSasuke92")
        MatchRequest.matchAttributesCustom.Add(MatchProperty.DisplayName, ServerName.text);     //Name to display to the network        (e.g. "DreamTeam's server")
        MatchRequest.matchAttributesCustom.Add(MatchProperty.MapName, MapName.options[0].text); //Map to join when joining the match    (e.g. "sand_box")
        
        networkMatch.CreateMatch(MatchRequest, OnMatchCreate);  //Creates the match and triggers OnMatchCreate()
    }

    /// <summary>
    /// Triggered after the creation of a Match
    /// </summary>
    /// <param name="matchResponse">Response from the Network to the match creation (Used here to test sucess)</param>
    public virtual void OnMatchCreate(CreateMatchResponse matchResponse)
    {
        if (matchResponse.success)
        {
            Debug.Log("OnMatchCreate > Success");
            SwitchSelectedMap();
            networkOverlord.StartHost(); //Host a game with current map in networkManager
        }
        else
        {
            Debug.LogError("OnMatchCreate > Failure, please verify internet connection");
            //SwitchSelectedMap();
            //networkOverlord.StartHost(); //Host a game with current map in networkManager
        }
    }

    private void SwitchSelectedMap()
    {
        Debug.Log("Joined : " + MapName.options[MapName.value].text);
        networkOverlord.onlineScene = MapName.options[MapName.value].text;
    }
}
