using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class MatchCreator : MonoBehaviour
{
    NetworkMatch networkMatch;  //Used to send match creation in the network

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        //Initialize NetworkMatch component
        networkMatch = gameObject.AddComponent<NetworkMatch>();
    }

    /// <summary>
    /// Creates a custom match with custom parameters if found
    /// </summary>
    public void CreateMatch()
    {
        MatchRequestCustom MatchRequest = new MatchRequestCustom();
        MatchRequest.name = "Match " + System.Guid.NewGuid().ToString("N"); //This GUID is to prevent 2 matches from having the same name
        MatchRequest.size = 10;                                             //Max numbers of player in a match (Spectators included)
        MatchRequest.advertise = true;                                      //The match is visible to all on the network
        MatchRequest.password = "";                                         //No password  (TODO/FEATURE : Add passwords)

        MatchRequest.matchAttributesCustom = new Dictionary<string, string>();
        MatchRequest.matchAttributesCustom.Add(MatchProperty.DisplayName, "<DISPLAYNAME>"); //Name to display to the network        (e.g. "DreamTeam's server")
        MatchRequest.matchAttributesCustom.Add(MatchProperty.HostName, "<HOSTNAME>");       //Name of original creator of the match (e.g. "DarkSasuke92")
        MatchRequest.matchAttributesCustom.Add(MatchProperty.MapName, "<MAPNAME>");         //Map to join when joining the match    (e.g. "sand_box")

        networkMatch.CreateMatch(MatchRequest, OnMatchCreate);  //Creates the match and triggers OnMatchCreate()
    }

    /// <summary>
    /// Triggered after the creation of a Match
    /// </summary>
    /// <param name="matchResponse">Response from the Network to the match creation (Used here to test sucess)</param>
    public void OnMatchCreate(CreateMatchResponse matchResponse)
    {
        if (matchResponse.success)
            Debug.Log("Create match succeeded");
        else
            Debug.LogError("Create match failed");
    }
}
